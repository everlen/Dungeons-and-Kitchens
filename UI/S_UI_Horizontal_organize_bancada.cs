using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_UI_Horizontal_organize_bancada : MonoBehaviour
{
    public GameObject UI_Ingrediente, UI_Horizontal_organize;
    GameObject prato_instantiate;
    List<GameObject> list_ui_ingredientes;
    bool exibindo;

    public void Adicionar_ingrediente(GameObject Ingrediente)
    {
        if (!exibindo)
        {
            list_ui_ingredientes = new List<GameObject>();
            exibindo = true;
            GetComponentInChildren<S_Ativar>().Ativar_canvas(true);
        }

        Debug.Log("Ingrediente adicionado ao UI");
        GameObject ingrediente_instantiate = Instantiate(UI_Ingrediente);
        ingrediente_instantiate.GetComponent<S_UI_Ingrediente>().Definir_dados(Ingrediente.GetComponent<Model_Ingrediente>().nome_ingrediente, Ingrediente.GetComponent<Model_Ingrediente>().Img_ingrediente, true);
        ingrediente_instantiate.transform.SetParent(UI_Horizontal_organize.transform);
        ingrediente_instantiate.transform.position = UI_Horizontal_organize.transform.position;
        ingrediente_instantiate.SetActive(true);

        list_ui_ingredientes.Add(ingrediente_instantiate);

    }


    public void Adicionar_prato(GameObject Prato)
    {

        Debug.Log("Prato adicionado ao UI");
        prato_instantiate = Instantiate(UI_Ingrediente);
        prato_instantiate.GetComponent<S_UI_Ingrediente>().Definir_dados(Prato.GetComponent<Model_Prato_finalizado>().Nome, Prato.GetComponent<Model_Prato_finalizado>().Img_ingrediente, false);
        prato_instantiate.transform.SetParent(UI_Horizontal_organize.transform);
        prato_instantiate.transform.position = UI_Horizontal_organize.transform.position;
        prato_instantiate.SetActive(true);

    }


    public void Remover_ingrediente(GameObject ingrediente)
    {
        GameObject aux = null;
        foreach(GameObject ui_ingrediente in list_ui_ingredientes)
        {
            if(ui_ingrediente.GetComponent<S_UI_Ingrediente>().getNome() == ingrediente.GetComponent<Model_Ingrediente>().nome_ingrediente)
            {
                aux = ui_ingrediente;
                break;
            }
        }
        if(aux != null)
            list_ui_ingredientes.Remove(aux);

        Destroy(aux);

        if(list_ui_ingredientes != null && list_ui_ingredientes.Count == 0)
        {
            exibindo = false;
            GetComponentInChildren<S_Ativar>().Ativar_canvas(false);
        }
    }


    public void Remover_prato()
    {
        Destroy(prato_instantiate);
        prato_instantiate = null;

        if (list_ui_ingredientes != null && list_ui_ingredientes.Count == 0)
        {
            exibindo = false;
            GetComponentInChildren<S_Ativar>().Ativar_canvas(false);
        }
    }


    public void Pegar_item(GameObject UI, bool is_ingrediente)
    {
        Debug.Log("É ingrediente? "+ is_ingrediente);
        if(!is_ingrediente)
        {
            GetComponentInParent<S_Bancada>().Pegar_item_bancada(UI.GetComponent<S_UI_Ingrediente>().getNome(), is_ingrediente);
        }
        else
        {
            GetComponentInParent<S_Bancada>().Pegar_item_bancada(UI.GetComponent<S_UI_Ingrediente>().getNome(), is_ingrediente);
        }

    }
}
