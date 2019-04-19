using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Ingrediente : MonoBehaviour{

    public SObj_Ingrediente sobj_ingrediente;
    public List<int> processo_culinario;
    /*  
     *  -id = filho do obj referente ao ID; 
     *  0 = intacta;
    *   1 = cortada;
    *   2 = cozida;
    *   4 = frita;
    *   8 = sobras;
    */
    public int id_ingrediente, tempo_cozinhar, tempo_max_cozinhar, tempo_virar_fritar, tempo_queimando_fritar, tempo_max_fritar, quant_virar_fritar, quant_cortar;
    public string nome_ingrediente;
    public int Nota = 0;
    public bool acompanhamento_fritar, acompanhamento_cozinhar;
    public List<GameObject> list_acompanhamentos;
    public Sprite Img_ingrediente;

    // Remover depois
    private void Start()
    {
        if(sobj_ingrediente != null)
            Definir_dados(sobj_ingrediente);
    }


    public void Definir_dados(SObj_Ingrediente ingrediente)
    {
        id_ingrediente = ingrediente.Id;
        nome_ingrediente = ingrediente.Nome;
        tempo_cozinhar = ingrediente.Tempo_ideal_cozinhar;
        tempo_max_cozinhar = ingrediente.Tempo_max_cozinhar;
        tempo_virar_fritar = ingrediente.tempo_virar_fritar;
        tempo_queimando_fritar = ingrediente.tempo_queimando_fritar;
        tempo_max_fritar = ingrediente.tempo_max_fritar;
        quant_virar_fritar = ingrediente.quant_virar_fritar;
        quant_cortar = ingrediente.Quant_cortar;
        acompanhamento_fritar = ingrediente.Fritar_acompanhado;
        acompanhamento_cozinhar = ingrediente.Cozinhar_acompanhado;
        Img_ingrediente = ingrediente.Img_ingrediente;

        processo_culinario = new List<int>();
        processo_culinario.Add(0);
    }

    public void Definir_dados(Model_Ingrediente ingrediente)
    {

        id_ingrediente = ingrediente.id_ingrediente;
        nome_ingrediente = ingrediente.nome_ingrediente;
        tempo_cozinhar = ingrediente.tempo_cozinhar;
        tempo_max_cozinhar = ingrediente.tempo_max_cozinhar;
        tempo_virar_fritar = ingrediente.tempo_virar_fritar;
        tempo_queimando_fritar = ingrediente.tempo_queimando_fritar;
        tempo_max_fritar = ingrediente.tempo_max_fritar;
        quant_virar_fritar = ingrediente.quant_virar_fritar;
        quant_cortar = ingrediente.quant_cortar;
        acompanhamento_fritar = ingrediente.acompanhamento_fritar;
        acompanhamento_cozinhar = ingrediente.acompanhamento_cozinhar;
        Img_ingrediente = ingrediente.Img_ingrediente;

        processo_culinario = new List<int>();
        processo_culinario.Add(0);
    }

    public int retornar_total_processo_culinario()
    {
        int retorno = 0;

        foreach(int i in processo_culinario)
        {
            retorno += i;
        }

        return retorno;
    }



    public void Cortado()
    {
        processo_culinario.Add(1);
        quant_cortar = 0;
        Debug.Log("Cortado");
    }

    public void Cozido()
    {
        processo_culinario.Add(2);
        tempo_max_cozinhar = 0;
        Debug.Log("Cozido");
    }

    public void Frito()
    {
        processo_culinario.Add(4);
        tempo_max_fritar = 0;
        Debug.Log("Frito");
    }

    public void Queimado()
    {
        processo_culinario.Add(8);
        Debug.Log("Queimou");
    }

    public void Definir_pai_acompanhamento(int id_ingrediente_pai)
    {
        processo_culinario.Add(-id_ingrediente_pai);
        Debug.Log("Acopanhamento do ingrediente id = " + id_ingrediente_pai);
    }



    public void Adicionar_acompanhamento(GameObject acompanhamento)
    {
        if (list_acompanhamentos == null)
            list_acompanhamentos = new List<GameObject>();
        list_acompanhamentos.Add(acompanhamento);
    }

    public void Definir_total_processo_culinario(int i)
    {
        processo_culinario.Add(i);
    }

    public void setNota(int nota)
    {
        Nota = (Nota + nota) / 2;
    }
    public int getNota()
    {
        return Nota;
    }
}
