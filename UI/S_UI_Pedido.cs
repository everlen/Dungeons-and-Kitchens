using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_Pedido : MonoBehaviour {

    public int Tempo_entrega = 20;
    float time_percorrido = 0;
    public Text Text_comida;
    public Slider barra;
    public Image Img;


    public void Definir_dados(string nome, int tempo_entrega, Sprite sprite)
    {
        Text_comida.text = nome;
        Tempo_entrega = tempo_entrega;
        Img = GetComponentInChildren<Image>(); 
        Img.sprite = sprite;
        barra = GetComponentInChildren<Slider>();

    }

	void Update () {

        time_percorrido += Time.deltaTime;

        if(time_percorrido >= Tempo_entrega)
        {
            GameObject.FindGameObjectWithTag("Gerenciador").GetComponent<S_Gerenciador_pedido>().Remover_pedido(this.gameObject);
        }
        else
        {
            barra.value = time_percorrido / Tempo_entrega;
        }
    }

}
