using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Caldeirao : MonoBehaviour {

    bool queimou;

    List<GameObject> List_ingredientes_acompanhamento;
    GameObject Ingrediente_pai;
    int tempo_ideal, tempo_max, nota;
    public bool Cozinhando = false;

    private void Awake()
    {
        resetar();
    }

    public bool Cozinhar(GameObject ingrediente_atual)
    {

        Debug.Log("Cozinhando");
        queimou = false;
        bool retorno = false;

        if (ingrediente_atual.GetComponent<Model_Ingrediente>() != null)
        {

                if (ingrediente_atual.GetComponent<Model_Ingrediente>().acompanhamento_cozinhar)
                {
                    List_ingredientes_acompanhamento.Add(ingrediente_atual);

                    Debug.Log(ingrediente_atual.GetComponent<Model_Ingrediente>().nome_ingrediente + " Este é o ingrediente acompanhamento");

                    retorno = true;
                }
                else if(ingrediente_atual.GetComponent<Model_Ingrediente>().tempo_max_cozinhar > 0)
                {
                    tempo_ideal = ingrediente_atual.GetComponent<Model_Ingrediente>().tempo_cozinhar;
                    tempo_max = ingrediente_atual.GetComponent<Model_Ingrediente>().tempo_max_cozinhar;
                    Ingrediente_pai = ingrediente_atual;


                    Debug.Log(ingrediente_atual.GetComponent<Model_Ingrediente>().nome_ingrediente + " Este é o ingrediente pai");

                    retorno = true;
                }
                else
                {
                    if (Ingrediente_pai != null)
                        Debug.Log("O ingrediente " + ingrediente_atual.GetComponent<Model_Ingrediente>().nome_ingrediente + " não pode ser combinado aos demais ingredientes");
                    else
                        Debug.Log("Este ingrediente nao pode ser cozido");
                }

        }
        return retorno;
    }


    public bool Iniciar_cozinhar()
    {
        bool retorno = false;

        if (Ingrediente_pai != null)
        {
            GetComponentInChildren<S_Ativar>().Ativar_canvas(true);
            GetComponent<S_Timer_caldeirao>().Contabilizar(tempo_max, tempo_ideal, GetComponentInChildren<S_Ativar>().obj);
            Cozinhando = true;
        }
        else
        {
            if (List_ingredientes_acompanhamento.Count > 0)
                Debug.Log("Nenhum ingrediente principal para fritar");
            else
                Debug.Log("Nenhum ingrediente foi adicionado");
        }

        return retorno;
    }


    public GameObject Pegar_ingrediente()
    {
        //gameObject.GetComponent<AudioSource>().Stop();
        GameObject retorno;

        float tempo_removido = GetComponent<S_Timer_caldeirao>().Finalizar_timer();
        if (!queimou)
        {
            Ingrediente_pai.GetComponent<Model_Ingrediente>().Cozido();
            foreach (GameObject ingrediente_acompanhamento in List_ingredientes_acompanhamento)
            {
                ingrediente_acompanhamento.GetComponent<Model_Ingrediente>().Definir_pai_acompanhamento(Ingrediente_pai.GetComponent<Model_Ingrediente>().id_ingrediente);
                ingrediente_acompanhamento.transform.SetParent(Ingrediente_pai.transform);
                Ingrediente_pai.GetComponent<Model_Ingrediente>().Adicionar_acompanhamento(ingrediente_acompanhamento);
            }
        }
            

        //Fazer calculo de nota
        nota = 0;
        Ingrediente_pai.GetComponent<Model_Ingrediente>().Nota = nota;

        retorno = Ingrediente_pai;
        resetar();

        Debug.Log("Pegou ingrediente em: " + tempo_removido);


        return retorno;
    }

    public void Queimou()
    {
        Ingrediente_pai.GetComponent<Model_Ingrediente>().Queimado();
        queimou = true;
    }

    private void resetar()
    {
        Cozinhando = false;
        tempo_ideal = 0;
        tempo_max = 0;
        nota = 0;
        Ingrediente_pai = null;
        List_ingredientes_acompanhamento = new List<GameObject>();
        GetComponentInChildren<S_Ativar>().Ativar_canvas(false);
    }

    public void Button_click()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<S_Adm_itens_equip>().Interagir_item_pegavel(Pegar_ingrediente());
    }
}
