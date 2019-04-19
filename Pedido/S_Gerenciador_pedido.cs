using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Gerenciador_pedido : MonoBehaviour {

    float tempo_proximo_pedido;
    public GameObject UI;
    private List<SObj_Prato_finalizado> list_all_pratos;
    List<GameObject> list_UI_pedidos;
    int count_pedidos;

    void Start()
    {    
        tempo_proximo_pedido = 3;
        count_pedidos = 0;
        list_UI_pedidos = new List<GameObject>();
        list_all_pratos = GetComponent<S_Pratos_existentes>().list_obj_pratos_existentes;
    }

    void Update()
    {

        tempo_proximo_pedido -= Time.deltaTime;
        if (tempo_proximo_pedido <= 0)
        {
            Gerar_pedido();
            tempo_proximo_pedido = Random.Range(10f, 20f);
        }

    }

    void Gerar_pedido()
    {
//Sorteio do pedido
        int id_prato = Random.Range(0, list_all_pratos.Count);
        SObj_Prato_finalizado prato_pedido = list_all_pratos[id_prato];
       
//Sorteio tempo para entregar
        int tempo_entrega = Random.Range(prato_pedido.Tempo_min_entrega, prato_pedido.Tempo_max_entrega);

        Info_pedido pedido = new Info_pedido(prato_pedido, tempo_entrega);
        //Debug.Log("Prato pedido " + prato_pedido + "  " + pedido.Nome);
        list_UI_pedidos.Add(UI.GetComponentInChildren<S_UI_Control_pedidos>().Adicionar_pedido(pedido));
        //Debug.Log(list_UI_pedidos.Count);

        count_pedidos++;
    } 

    public bool Verificar_existencia_prato_pendente(GameObject Prato_finalizado)
    {
        bool retorno = false;

        foreach(GameObject UI_pedido in list_UI_pedidos)
        {

//Alterar para aceitar especificações
            //Debug.Log(UI_pedido.GetComponent<S_UI_Pedido>().Text_comida.text);
            //Debug.Log(Prato_finalizado.GetComponent<Model_Prato_finalizado>().Nome);

            if (UI_pedido.GetComponent<S_UI_Pedido>().Text_comida.text == Prato_finalizado.GetComponent<Model_Prato_finalizado>().Nome)
            {
                retorno = true;

                Entregar_pedido(UI_pedido);
                break;
            }
        }

        if (retorno)
        {
            


        }

        return retorno;
    }

    public void Entregar_pedido(GameObject UI_pedido)
    {
        list_UI_pedidos.Remove(UI_pedido);
        UI.GetComponentInChildren<S_UI_Control_pedidos>().Remover_pedido(UI_pedido);
        count_pedidos--;
    }

    public void Remover_pedido(GameObject UI_pedido)
    {
        list_UI_pedidos.Remove(UI_pedido);
        UI.GetComponentInChildren<S_UI_Control_pedidos>().Remover_pedido(UI_pedido);
        count_pedidos--;
    }
}
