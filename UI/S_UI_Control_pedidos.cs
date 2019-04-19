using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_Control_pedidos : MonoBehaviour {
    public GameObject UI_Pedido;

    public GameObject Adicionar_pedido(Info_pedido info_pedido)
    {
        return GetComponentInChildren<S_UI_Horizontal_organize_pedido>().Adicionar(UI_Pedido, info_pedido);
    }

    public void Remover_pedido(GameObject UI_Pedido)
    {
        GetComponentInChildren<S_UI_Horizontal_organize_pedido>().Remover(UI_Pedido);
    }

}
