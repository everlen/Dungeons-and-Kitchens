using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {
    NavMeshAgent agent;
    public LayerMask mask;
    public RaycastHit hit;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>(); //aqui pega o component NavMeshAgent	
	}
	
	// Update is called once per frame
	void Update () {

        //se apertar o touch
        if (Input.touchCount > 0)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
   
        // se apertar o botao esquerdo
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,100, mask))
            {
                agent.destination = hit.point; //pra onde o personagem vai andar quando clicar com o mouse
            }
        }
	}
}
