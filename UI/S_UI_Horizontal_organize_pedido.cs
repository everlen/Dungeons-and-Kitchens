
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_UI_Horizontal_organize_pedido : MonoBehaviour
{
    List<GameObject> list_ui_pedidos;
    public GameObject Adicionar (GameObject UI_Pedido, Info_pedido info_pedido)
    {
        if (list_ui_pedidos == null)
        {
            list_ui_pedidos = new List<GameObject>();
        }

        UI_Pedido.GetComponent<S_UI_Pedido>().Definir_dados(info_pedido.Nome, info_pedido.Tempo_entrega, info_pedido.Img_prato);
        GameObject pedido_instantiate = Instantiate(UI_Pedido);
        pedido_instantiate.transform.SetParent(transform);
        pedido_instantiate.transform.position = transform.position;
        pedido_instantiate.SetActive(true);

        list_ui_pedidos.Add(pedido_instantiate);
        return pedido_instantiate;
    }
    public void Remover(GameObject UI_Pedido)
    {
        list_ui_pedidos.Remove(UI_Pedido);
        Destroy(UI_Pedido);
    }
}
