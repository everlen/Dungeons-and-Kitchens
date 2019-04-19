using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Gerenciador_ingrediente : MonoBehaviour {
    private List<Model_Prato_finalizado> list_all_pratos, list_pratos_possiveis; //Compara se é igual ao de algum prato disponivel
    int checkPrato; //Variavel para contar sempre que um ingrediente combina com o prato - se forem todos o prato foi feito

    /// Variável Auxiliar para que a var estática possa ser editada no inspector
    /// </summary>

    private void Start()
    {
        list_all_pratos = GetComponent<S_Pratos_existentes>().list_pratos_existentes;
        Resetar_list_pratos_possiveis();
    }

	public GameObject Ingrediente_adicionado(List<GameObject> list_ingredientes_atuais, GameObject ingrediente_adicionado)
    {
        List<GameObject> list_ingredientes_aux = new List<GameObject>();

        foreach (GameObject ingrediente_lista_atual in list_ingredientes_atuais)
            list_ingredientes_aux.Add(ingrediente_lista_atual);

        list_ingredientes_aux.Add(ingrediente_adicionado);

        if (ingrediente_adicionado.GetComponent<Model_Ingrediente>().list_acompanhamentos != null)
            foreach(GameObject acompanhamento in ingrediente_adicionado.GetComponent<Model_Ingrediente>().list_acompanhamentos)
            {
                list_ingredientes_aux.Add(acompanhamento);
                //Debug.Log("Nome acompanhamento " + acompanhamento.GetComponent<Model_Ingrediente>().nome_ingrediente);
            }

        GameObject result_prato = checarPrato(list_ingredientes_aux);
        if(list_pratos_possiveis.Count == 0)
        {
            Resetar_list_pratos_possiveis();
            result_prato = ingrediente_adicionado;

            Debug.Log("Nenhum prato disponivel com estes ingredientes");
        }

        //Debug.Log("Quant ingredientes atuais " + list_ingredientes_atuais.Count);

        return result_prato;
    }

    public GameObject Prato_adicionado(List<GameObject> list_ingredientes_atuais, List<GameObject> list_ingredientes_prato)
    {
        List<GameObject> list_ingredientes_aux = new List<GameObject>();

        foreach (GameObject ingrediente_lista_atual in list_ingredientes_atuais)
            list_ingredientes_aux.Add(ingrediente_lista_atual);

        foreach (GameObject ingrediente_prato in list_ingredientes_prato)
            list_ingredientes_aux.Add(ingrediente_prato);

        GameObject result_prato = checarPrato(list_ingredientes_aux);
        if (list_pratos_possiveis.Count == 0)
        {
            Resetar_list_pratos_possiveis();
            result_prato = list_ingredientes_prato[0];

            Debug.Log("Nenhum prato disponivel com estes ingredientes");
        }

        //Debug.Log("Quant ingredientes atuais " + list_ingredientes_atuais.Count);

        return result_prato;
    }

    public GameObject checarPrato(List<GameObject> list_ingredientes_atuais)
    {
        GameObject retorno = null;
        Model_Prato_finalizado Prato;
        Resetar_list_pratos_possiveis();

        for (int i = 0; i < list_pratos_possiveis.Count; i++)
        {

            Prato = list_pratos_possiveis[i];

            //Debug.Log("List pratos possiveis " + list_pratos_possiveis.Count);
            //Debug.Log("O prato esta null? " + Prato);
            //Debug.Log("list Ingredientes atuais " + list_ingredientes_atuais.Count);

            int ingredientes_iguais = Prato.Checar_ingredientes_iguais(list_ingredientes_atuais);

            //Debug.Log("Ingredientes iguais = " + ingredientes_iguais);

            if (ingredientes_iguais == Prato.getIngredientes_necessarios().Count && ingredientes_iguais == list_ingredientes_atuais.Count)
            {
                retorno = Prato.Obj_prato_finalizado;
                retorno.GetComponent<Model_Prato_finalizado>().Definir_dados(Prato);
                //Debug.Log("O prato primeiro retorno " + retorno.GetComponent<Model_Prato_finalizado>().Nome);
                //Debug.Log("Prato foi finalizado");
                break;
            }
            else if (ingredientes_iguais < list_ingredientes_atuais.Count)
            {

                //Debug.Log("Não achou nada em comum com o prato " + Prato.Nome);

                list_pratos_possiveis.RemoveAt(i);
                i--;
            }
            else
            {
               //Debug.Log("Achou algo em comummas ainda esta incompleto");
            }
        }

        //Debug.Log("Qaunt pratos possiveis " + list_pratos_possiveis.Count);
       // foreach (Model_Prato_finalizado prato_lista in list_pratos_possiveis)
            //Debug.Log(prato_lista.Nome);

        return retorno;
    }

    private void Resetar_list_pratos_possiveis()
    {
        list_pratos_possiveis = new List<Model_Prato_finalizado>();
        foreach (Model_Prato_finalizado prato in list_all_pratos)
        {
            list_pratos_possiveis.Add(prato);
        }
    }
}
