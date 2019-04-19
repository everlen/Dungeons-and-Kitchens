using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentaçao : MonoBehaviour {
    bool movimentando, nao_andar;
    Vector3 destino;
    Transform obj_destino;

	void Start () {
        obj_destino = null;
        movimentando = false;
    }
	
	void FixedUpdate () {
        if (Input.touchCount > 0)
        {
            if(nao_andar == false)
            {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                Definir_destino(ray);
            }
            else
            {
                Debug.Log("Não foi acionado o metodo andar");
            }
            
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if(nao_andar == false)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Definir_destino(ray);
            }
            else
            {
                Debug.Log("Não foi acionado o metodo andar");
            }
        }

        if (movimentando)
            Mover_player();

    }

    void Definir_destino(Ray ray)
    {
         obj_destino = null;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {

            //Verificando se o hit foi em algum obj especifico
            //Mover_pegavel();
            if (hit.transform.GetComponent<S_Objeto_pegavel>() != null)
            {

                obj_destino = hit.transform;
                if (obj_destino.GetComponent<S_Objeto_pegavel>().pai_obj_interagivel == null)
                    destino = obj_destino.GetComponent<S_Objeto_pegavel>().transform.position;
                else
                {
                    destino = obj_destino.GetComponent<S_Objeto_pegavel>().pai_obj_interagivel.GetComponent<S_Objeto_interagivel>().posiçao_uso;

                }

            }
            //Mover_interagivel();
            else if (hit.transform.GetComponent<S_Objeto_interagivel>() != null)
            {
                obj_destino = hit.transform;
                destino = obj_destino.GetComponent<S_Objeto_interagivel>().posiçao_uso;

            }
            else
            {
                obj_destino = null;
                destino = hit.point;
            }

            movimentando = true;
        }
       
    }

    void Mover_player()
    {
        int speed = 5;
        transform.LookAt(destino);
        transform.position = Vector3.MoveTowards(transform.position, destino, speed * Time.deltaTime);
        if (transform.position == destino)
        {
            movimentando = false;
            if (obj_destino != null)
            {
                GetComponent<S_Adm_itens_equip>().Interagir_obj_destino(obj_destino.gameObject);
            }
        }
            
    }

    public void setDestino(Vector3 novo_destino)
    {
        destino = novo_destino;
    }
    public void setNao_andar(bool a)
    {
            nao_andar = a;
    }

}
