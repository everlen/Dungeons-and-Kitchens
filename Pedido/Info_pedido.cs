using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info_pedido {


    public string Nome;
    public Sprite Img_prato;
    public int Tempo_entrega;
    //public int tempo_entregue;

    public Info_pedido(SObj_Prato_finalizado Prato, int tempo_entrega)
    {
        Nome = Prato.Nome;
        Img_prato = Prato.Img_prato;
        Tempo_entrega = tempo_entrega;
    }


}
