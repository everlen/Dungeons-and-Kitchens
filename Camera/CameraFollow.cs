using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
	public float zMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float zSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector3 maxXAndZ;		// The maximum x and y coordinates the camera can have.
	public Vector3 minXAndZ;		// The minimum x and y coordinates the camera can have.

    [SerializeField]
	private Transform player;		// Reference to the player's transform.

    public static CameraFollow cameraFollow;
    
	void Awake ()
	{
		//Ele busca pela tag, para ser mais universal
		// Setting up the reference.
		//player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start(){
        cameraFollow = this;

        //transform.Rotate (5, 0, 0);
	}


	bool CheckXMargin()
	{
		//Retorna verdadeiro se a distância entre a câmera eo jogador no eixo x for maior que a margem x.
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}


	bool CheckZMargin()
	{
		//Retorna verdadeiro se a distância entre a câmera eo jogador no eixo Z for maior que a margem Z.
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.z - player.position.z) > zMargin;
	}


	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetZ = transform.position.z;

		// Se o jogador se moveu para além da margem x ...
		// If the player has moved beyond the x margin...
		if(CheckXMargin())
			// ... a coordenada x do alvo deve ser um Lerp(interpolacao) entre a posição x atual da câmera e a posição x atual do jogador.
			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

		// Se o jogador se moveu para além da margem Z ...
		// If the player has moved beyond the Z margin...
		if(CheckZMargin())
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetZ = Mathf.Lerp(transform.position.z, player.position.z, zSmooth * Time.deltaTime);
        
		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(targetX, minXAndZ.x, maxXAndZ.x);
		targetZ = Mathf.Clamp(targetZ, minXAndZ.z, maxXAndZ.z);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX +1.5f,transform.position.y , targetZ);

	}
}
