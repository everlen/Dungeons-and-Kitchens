using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Chapa : MonoBehaviour
{

    GameObject ingrediente_pai;
    List<GameObject> Ingredientes_acompanhamento;
    int tempo_max, tempo_virar, tempo_queimando, quant_fritar, id_ingrediente_pai;
    bool queimado = false;
    public bool fritando = false;

    private void Awake()
    {
        id_ingrediente_pai = 0;
        tempo_max = 0;
        Ingredientes_acompanhamento = new List<GameObject>();
    }

    public bool Fritar(GameObject ingrediente)
    {

        Debug.Log("Fritar");

        bool retorno = false;

        if (ingrediente.GetComponent<Model_Ingrediente>() != null)
        {
            if (ingrediente.GetComponent<Model_Ingrediente>().acompanhamento_fritar)
            {
                //  Acompanhamento para o ingrediente pai
                Ingredientes_acompanhamento.Add(ingrediente);
                retorno = true;
                Debug.Log(ingrediente.GetComponent<Model_Ingrediente>().nome_ingrediente + " Este é o ingrediente acompanhamento");
            }
            else if (id_ingrediente_pai == 0 && ingrediente.GetComponent<Model_Ingrediente>().tempo_max_fritar > 0)
            {
                // Este é o ingrediente pai

                tempo_max = ingrediente.GetComponent<Model_Ingrediente>().tempo_max_fritar;
                tempo_virar = ingrediente.GetComponent<Model_Ingrediente>().tempo_virar_fritar;
                tempo_queimando = ingrediente.GetComponent<Model_Ingrediente>().tempo_queimando_fritar;
                quant_fritar = ingrediente.GetComponent<Model_Ingrediente>().quant_virar_fritar;
                id_ingrediente_pai = ingrediente.GetComponent<Model_Ingrediente>().id_ingrediente;
                ingrediente_pai = ingrediente;

                retorno = true;

                Debug.Log(ingrediente_pai.GetComponent<Model_Ingrediente>().nome_ingrediente + " Este é o ingrediente pai");
            }
            else
            {
                // Este ingrediente não pode ser combinado
                if (id_ingrediente_pai != 0)
                    Debug.Log("O ingrediente " + ingrediente.GetComponent<Model_Ingrediente>().nome_ingrediente + " não pode ser combinado aos demais ingredientes");
                else
                    Debug.Log("Este ingrediente nao pode ser frito");
            }

        }
        return retorno;
    }

    public void Iniciar_fritura()
    {
        if (tempo_max > 0)
        {
            GetComponentInChildren<S_Ativar>().Ativar_canvas(true);
            GetComponent<S_Timer_chapa>().Contabilizar(tempo_max, tempo_virar, tempo_queimando, quant_fritar, GetComponentInChildren<S_Ativar>().obj);
            fritando = true;
        }
        else
        {
            if (ingrediente_pai == null)
                Debug.Log("Sem ingredientes para fritar");
            else
                Debug.Log("Ingredientes invalidos para iniciar a fritura");
        }
    }

    public bool Virar()
    {
        return GetComponent<S_Timer_chapa>().Virar_ingrediente();
    }

    public GameObject Pegar_ingrediente()
    {
        GameObject ingrediente_finalizado = ingrediente_pai;

        GetComponentInChildren<S_Ativar>().Ativar_canvas(false);

        if (!queimado)
        {
            ingrediente_pai.GetComponent<Model_Ingrediente>().Frito();
            foreach (GameObject ingrediente_acompanhamento in Ingredientes_acompanhamento)
            {
                ingrediente_acompanhamento.GetComponent<Model_Ingrediente>().Definir_pai_acompanhamento(id_ingrediente_pai);
                ingrediente_acompanhamento.transform.SetParent(ingrediente_pai.transform);
                ingrediente_pai.GetComponent<Model_Ingrediente>().Adicionar_acompanhamento(ingrediente_acompanhamento);
            }


        }
        else
        {

        }

        float tempo_decorrido = GetComponent<S_Timer_chapa>().Finalizar_timer();
        int nota = 0;
        ingrediente_pai.GetComponent<Model_Ingrediente>().Nota = nota;
        resetar_chapa();

        return ingrediente_finalizado;
    }

    public void Queimou()
    {
        ingrediente_pai.GetComponent<Model_Ingrediente>().Queimado();
        queimado = true;
    }

    private void resetar_chapa()
    {
        ingrediente_pai = null;
        fritando = false;
        Ingredientes_acompanhamento = new List<GameObject>();
        tempo_max = 0;
        id_ingrediente_pai = 0;
    }

    public void Button_click()
    {
        Virar();
    }
}