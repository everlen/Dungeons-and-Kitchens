using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Posiçao_uso : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) { 
    
        if (other.gameObject.tag == "Player") { 
            GetComponentInParent<S_Objeto_interagivel>().setPlayer_posicionado(true);
            Debug.Log("Entrou");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") { 
            GetComponentInParent<S_Objeto_interagivel>().setPlayer_posicionado(false);
            Debug.Log("Saiu");
        }
  
    }
   
}
