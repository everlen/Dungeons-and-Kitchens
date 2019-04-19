using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Tabua_corte : MonoBehaviour {

    GameObject Comida;
    int quant_cortar, quant_cortes_realizados;
    public bool finalizado;
    Movimentaçao player_movimentaçao;

    private void Awake()
    {
        player_movimentaçao = GameObject.FindGameObjectWithTag("Player").GetComponent<Movimentaçao>();
    }

    public bool Cortar(GameObject comida)
    {
        bool retorno = false;

        if (comida.GetComponent<Model_Ingrediente>() != null)
        {
            quant_cortar = comida.GetComponent<Model_Ingrediente>().quant_cortar;
            quant_cortes_realizados = 0;
            if (quant_cortar > 0)
            {
                Debug.Log("Quant_cortar = " + quant_cortar);
                Comida = comida;
                finalizado = false;
                GetComponentInChildren<S_Ativar>().Ativar_canvas(true);          
                retorno = true;
                Debug.Log("Botão cortar criado");
            }
            else
            {
                Debug.Log("Este ingrediente não pode ser cortado");
            }

        }
        return retorno;  
    }

    void Finalizado()
    {
        GetComponentInChildren<S_Ativar>().Ativar_canvas(false);
        Comida.GetComponent<Model_Ingrediente>().Cortado();
        finalizado = true;

    }

    public void Button_click()
    {
        Debug.Log("Botão acionado");

        player_movimentaçao.setDestino(GetComponentInParent<S_Objeto_interagivel>().posiçao_uso);

        if (GetComponent<S_Objeto_interagivel>().getPlayer_posicionado())
        {
            player_movimentaçao.setNao_andar(true);

            quant_cortes_realizados++;
            if (quant_cortes_realizados == quant_cortar)
            {
                player_movimentaçao.setNao_andar(false);
                Finalizado();
            }
        }
        else
        {
            Debug.Log("Bancada muito distante");
        }
    }

    public GameObject Pegar_ingrediente()
    {
        GameObject comida = Comida;
        Comida = null;
        finalizado = false;
        return comida; 

    }
}
