using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Prato_finalizado : MonoBehaviour{

    public SObj_Prato_finalizado sobj_prato_finalizado;
    int Id;
    public string Nome;
    float Preco;
    List<Model_Ingrediente> list_ingredientes_necessarios;
    List<int> Processo_necessario_ingredientes;
    //public GameObject Obj_prato_finalizado;
    public int Tempo_min_entrega;
    public int Tempo_max_entrega;
    public GameObject Obj_prato_finalizado;
    int Nota;
    public Sprite Img_ingrediente;

    public List<GameObject> Ingredientes_integrantes;

    public void Definir_dados(SObj_Prato_finalizado dados_prato)
    {
        Id = dados_prato.Id;
        Nome = dados_prato.Nome;
        Obj_prato_finalizado = dados_prato.Obj_prato_finalizado;
        Preco = dados_prato.Preco;
        Processo_necessario_ingredientes = dados_prato.Processo_necessario_ingredientes;
        Tempo_min_entrega = dados_prato.Tempo_min_entrega;
        Tempo_max_entrega = dados_prato.Tempo_max_entrega;
        Img_ingrediente = dados_prato.Img_prato;

        list_ingredientes_necessarios = new List<Model_Ingrediente>();
        int i = 0;
        foreach (SObj_Ingrediente ingrediente in dados_prato.Ingredientes_necessarios)
        {
            Model_Ingrediente Ingrediente_finalizado = gameObject.AddComponent<Model_Ingrediente>();
            Ingrediente_finalizado.Definir_dados(ingrediente);
            Ingrediente_finalizado.Definir_total_processo_culinario(dados_prato.Processo_necessario_ingredientes[i]);
            list_ingredientes_necessarios.Add(Ingrediente_finalizado);
            i++;
        }
    }

    public void Definir_dados(Model_Prato_finalizado dados_prato)
    {
        Id = dados_prato.Id;
        Nome = dados_prato.Nome;
        Obj_prato_finalizado = dados_prato.Obj_prato_finalizado;
        Preco = dados_prato.Preco;
        Processo_necessario_ingredientes = dados_prato.Processo_necessario_ingredientes;
        Tempo_min_entrega = dados_prato.Tempo_min_entrega;
        Tempo_max_entrega = dados_prato.Tempo_max_entrega;
        Img_ingrediente = dados_prato.Img_ingrediente;

        list_ingredientes_necessarios = new List<Model_Ingrediente>();
        int i = 0;

        //Debug.Log("Quant ingredientes necessarios " + dados_prato.list_ingredientes_necessarios.Count);

        foreach (Model_Ingrediente ingrediente in dados_prato.list_ingredientes_necessarios)
        {
            Model_Ingrediente Ingrediente_finalizado = ingrediente;
            Ingrediente_finalizado.Definir_dados(ingrediente);
            Ingrediente_finalizado.Definir_total_processo_culinario(dados_prato.Processo_necessario_ingredientes[i]);
            list_ingredientes_necessarios.Add(Ingrediente_finalizado);
            i++;
        }

    }

    // Recebe a lista de ingredientes na bancada e contabiliza quantos ingredientes s~çao iguais, caso todos sejam entao o retorno é -1 
    public int Checar_ingredientes_iguais(List<GameObject> list_ingredientes_bancada)
    {
        int retorno = 0;
        bool acho;
        List<int> id_pai_acompanhamento = null;

//Verificação dos acompanhamentos necessarios
        for (int i = 0; i< Processo_necessario_ingredientes.Count; i++)
        {
            //Debug.Log("Processo culinario = " + Processo_necessario_ingredientes[i]);
            if (Processo_necessario_ingredientes[i] < 0)
            {

                if (id_pai_acompanhamento == null)
                    id_pai_acompanhamento = new List<int>();

                id_pai_acompanhamento.Add(Processo_necessario_ingredientes[i]);
            }
               
        }

        foreach (GameObject ingrediente in list_ingredientes_bancada)
        {
            //Debug.Log("Ingrediente da bancada " + ingrediente.GetComponent<Model_Ingrediente>().nome_ingrediente);

            acho = false;
            int i = 0;
            foreach (Model_Ingrediente ingrediente_prato in list_ingredientes_necessarios)
            {
                //Debug.Log("id ingrediente " +ingrediente.GetComponent<Model_Ingrediente>().id_ingrediente);
                //Debug.Log("id Prato " + ingrediente_prato.id_ingrediente);

                if (ingrediente.GetComponent<Model_Ingrediente>().id_ingrediente == ingrediente_prato.id_ingrediente)
                {
                   // Debug.Log("Total processo culinario da " + ingrediente.GetComponent<Model_Ingrediente>().nome_ingrediente + " " + ingrediente.GetComponent<Model_Ingrediente>().retornar_total_processo_culinario() + " / " + ingrediente_prato.retornar_total_processo_culinario());
                    int processo_prato = ingrediente_prato.retornar_total_processo_culinario();

                    if (ingrediente.GetComponent<Model_Ingrediente>().retornar_total_processo_culinario() == processo_prato)
                    {
                        if(processo_prato < 0)
                        {
                            for(int c = 0; c < id_pai_acompanhamento.Count; c++)
                            {
                                if (processo_prato == id_pai_acompanhamento[c])
                                {
                                    id_pai_acompanhamento.RemoveAt(c);
                                    break;
                                }
                            }
                                    
                        }

                        acho = true;
                        break;
                    }

                }
                i++;
            }

            if (acho){              
                retorno++;
            }

        }

        if (id_pai_acompanhamento != null && id_pai_acompanhamento.Count > 0)
            retorno = retorno - id_pai_acompanhamento.Count;

        return retorno;
    }

    public void Definir_ingredientes(List<GameObject> list_ingredientes)
    {
        Ingredientes_integrantes = new List<GameObject>();
        foreach (GameObject ing in list_ingredientes)
        {
            Ingredientes_integrantes.Add(ing);
        }
    }

    public void setNota(int nota)
    {
        Nota = (Nota + nota)/2;
    }
    public int getNota()
    {
        return Nota;
    }

    public List<Model_Ingrediente> getIngredientes_necessarios()
    {
        return list_ingredientes_necessarios;
    }
}
