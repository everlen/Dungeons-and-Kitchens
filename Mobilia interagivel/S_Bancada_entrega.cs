using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bancada_entrega : MonoBehaviour {

    public GameObject prato;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public bool Realizar_entrega(GameObject Prato_feito)
    {
        bool retorno;

        bool pedido_existente = GameObject.FindGameObjectWithTag("Gerenciador").GetComponent<S_Gerenciador_pedido>().Verificar_existencia_prato_pendente(Prato_feito);

        if (pedido_existente)
        {
            Debug.Log("Pedido_entregue");
            Destroy(Prato_feito);
            retorno = true;
        }
        else
        {
            Debug.Log("Pedido_nao_entregue");
            retorno = false;
        }

        return retorno;
    }
}
