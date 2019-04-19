using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bancada : MonoBehaviour {

    List<GameObject> list_ingredientes;
    GameObject Prato_atual;
    S_Gerenciador_ingrediente gerenciador_ingrediente;
    S_UI_Horizontal_organize_bancada s_horizontal;

    private void Start()
    {
        gerenciador_ingrediente = GameObject.FindGameObjectWithTag("Gerenciador").GetComponent<S_Gerenciador_ingrediente>();
        s_horizontal = GetComponentInChildren<S_UI_Horizontal_organize_bancada>();
        list_ingredientes = new List<GameObject>();
        Resetar();
    }

    public GameObject Usar_bancada(GameObject obj_equipado_player)
    {
        GameObject retorno = null;

        //Arrumar gambiarra
        if (obj_equipado_player != null)
        {
            if (obj_equipado_player.GetComponent<Model_Ingrediente>() != null && obj_equipado_player.GetComponent<Model_Prato_finalizado>() == null)
            {
                //Debug.Log("O prato entrou mesmo assim como ingrediente");
                if (Combinar_ingredientes(obj_equipado_player))
                {
                    retorno = new GameObject();
                }
                else
                    retorno = obj_equipado_player;
            }
            else if (obj_equipado_player.GetComponent<Model_Prato_finalizado>() != null)
            {
                if (Posicionar_prato_finalizado(obj_equipado_player))
                    retorno = new GameObject();
                else
                    retorno = obj_equipado_player;
            }
        }
        else
        {
            Debug.Log("OBJ equipado = null");
        }

        Debug.Log("Prato atual = " + Prato_atual);
        return retorno;
    }


    private bool Combinar_ingredientes(GameObject ingrediente)
    {
        bool retorno = true;

        GameObject result_prato = gerenciador_ingrediente.Ingrediente_adicionado(list_ingredientes, ingrediente);

        //Debug.Log("Result combinar ingrediente = " + result_prato);

        if (result_prato != null && result_prato != ingrediente)
        {
            Adicionar_ingrediente(ingrediente);

            if (ingrediente.GetComponent<Model_Ingrediente>().list_acompanhamentos != null)
                foreach (GameObject acompanhamento in ingrediente.GetComponent<Model_Ingrediente>().list_acompanhamentos)
                    list_ingredientes.Add(acompanhamento);

            Instanciar_prato_finalizado(result_prato);
        }
        else if (result_prato == ingrediente)
        {
//Não posicionar ingrediente na bancada
            retorno = false;
        }
        else
        {
            //Ainda nao esta finalizado
            Adicionar_ingrediente(ingrediente);
            if (ingrediente.GetComponent<Model_Ingrediente>().list_acompanhamentos != null)
                foreach (GameObject acompanhamento in ingrediente.GetComponent<Model_Ingrediente>().list_acompanhamentos)
                    list_ingredientes.Add(acompanhamento);

        }
        return retorno;
    }



    public void Pegar_item_bancada(string nome_ing, bool is_ingrediente)
    {
        // Implementar mecanica de escolha de item bancada
        GameObject obj_pego = null;

        //Debug.Log("Patro finalizado = " + Prato_atual);
        //Debug.Log("O prato é ingrediente? " + is_ingrediente);
        if (is_ingrediente)
        {

            if (Prato_atual != null)
            {
                Destroy(Prato_atual);
                Remover_prato();
            }
            foreach (GameObject ingrediente_bancada in list_ingredientes)
            {
                ingrediente_bancada.SetActive(true);
                if(ingrediente_bancada.GetComponent<Model_Ingrediente>().nome_ingrediente == nome_ing)
                    obj_pego = ingrediente_bancada;
            }
                
            if(obj_pego != null)
                Remover_ingrediente(obj_pego);

            List<GameObject> list_aux_acompanhamento = obj_pego.GetComponent<Model_Ingrediente>().list_acompanhamentos;
            if (list_aux_acompanhamento != null)
            {
                foreach(GameObject acompanhamento in list_aux_acompanhamento)
                {
                    list_ingredientes.Remove(acompanhamento);
                }
            }

            GameObject result_prato = gerenciador_ingrediente.checarPrato(list_ingredientes);
            if (result_prato != null)
            {
                Instanciar_prato_finalizado(result_prato);
            }

        }
        else
        {
            obj_pego = Prato_atual;
            Resetar();

        }

        Debug.Log("Obj pego = "+ obj_pego);

        GameObject.FindGameObjectWithTag("Player").GetComponent<S_Adm_itens_equip>().Interagir_item_pegavel(obj_pego);
    }


    public bool Posicionar_prato_finalizado(GameObject prato_finalizado)
    {
        bool retorno = true;

        //Debug.Log("Dados bancada na hora do posicionamento " + list_ingredientes.Count + " / Prato atual? " + Prato_atual);

        if (list_ingredientes.Count == 0 && Prato_atual == null)
        {
            //Debug.Log("Quant_list prato_finalizado " + prato_finalizado.GetComponent<Model_Prato_finalizado>().Ingredientes_integrantes.Count);

            Atualizar_ingredientes(prato_finalizado.GetComponent<Model_Prato_finalizado>().Ingredientes_integrantes);
            Prato_atual = prato_finalizado;
            Adicionar_ui_prato();
            GetComponentInChildren<S_Mover_obj_equipado>().Posicionar_obj(Prato_atual);


            //Debug.Log("Prato finalizado foi posicionado " + Prato_atual.GetComponent<Model_Prato_finalizado>().Nome);
        }
        else if(list_ingredientes.Count > 0 && Prato_atual == null)
        { 

            GameObject result_prato = gerenciador_ingrediente.Prato_adicionado(list_ingredientes, prato_finalizado.GetComponent<Model_Prato_finalizado>().Ingredientes_integrantes);
            Destroy(prato_finalizado);

            if (result_prato != null && result_prato != prato_finalizado.GetComponent<Model_Prato_finalizado>().Ingredientes_integrantes[0])
            {
                Atualizar_ingredientes(prato_finalizado.GetComponent<Model_Prato_finalizado>().Ingredientes_integrantes);
                Instanciar_prato_finalizado(result_prato);
            }
            else if (result_prato == null)
            {
                Atualizar_ingredientes(prato_finalizado.GetComponent<Model_Prato_finalizado>().Ingredientes_integrantes);
            }
            else
            {
                Debug.Log("Não foi posivel posicionar pois ha combinente na bancada que nao combinam com o prato");
                retorno = false;
            }

           
        }
        else
        {
            Debug.Log("Não foi posivel posicionar pois ha um prato finalizado na bancada");
        }

        return retorno;
    }

    private void Atualizar_ingredientes(List<GameObject> list_ingredientes_prato)
    {
        foreach(GameObject ing_prato in list_ingredientes_prato)
        {
            //Debug.Log("Ing_prato = " + ing_prato);
            if(ing_prato.GetComponent<Model_Ingrediente>().acompanhamento_cozinhar || ing_prato.GetComponent<Model_Ingrediente>().acompanhamento_fritar)
            {
                list_ingredientes.Add(ing_prato);
            }
            else
            {
                Adicionar_ingrediente(ing_prato);
            }
            
        }

        Debug.Log("Quantidade de ingredientes na bancada " + list_ingredientes.Count);
    }

    private void Resetar()
    {

        Remover_prato();
        for (int i = list_ingredientes.Count - 1; i >= 0; i--)
        {
            Remover_ingrediente(list_ingredientes[i]);
        }

    }

    private void Instanciar_prato_finalizado(GameObject Model_prato)
    {
        foreach (GameObject ingrediente_bancada in list_ingredientes)
        {
            ingrediente_bancada.SetActive(false);

        }
        if (Prato_atual != null)
        {
            Destroy(Prato_atual);
            Remover_prato();
        }
            

        Prato_atual = Instantiate(Model_prato, GetComponentInChildren<S_Mover_obj_equipado>().gameObject.transform.position, this.transform.rotation);
        GetComponentInChildren<S_Mover_obj_equipado>().Posicionar_obj(Prato_atual);
        Prato_atual.GetComponent<S_Objeto_pegavel>().pai_obj_interagivel = this.gameObject;
        Prato_atual.GetComponent<Model_Prato_finalizado>().Definir_dados(Model_prato.GetComponent<Model_Prato_finalizado>());
        Prato_atual.GetComponent<Model_Prato_finalizado>().Definir_ingredientes(list_ingredientes);
        Adicionar_ui_prato();

        //Debug.Log("Quant_list prato_finalizado " + list_ingredientes.Count);
        //Debug.Log("O prato instanciado foi " + Prato_atual.GetComponent<Model_Prato_finalizado>().Nome);
        Debug.Log("Quant ingrediente integrante no prato " + Prato_atual.GetComponent<Model_Prato_finalizado>().Ingredientes_integrantes.Count);
    }

    private void Adicionar_ingrediente(GameObject Ingrediente)
    {
        list_ingredientes.Add(Ingrediente);
        s_horizontal.Adicionar_ingrediente(Ingrediente);
    }

    private void Remover_ingrediente(GameObject Ingrediente)
    {
        s_horizontal.Remover_ingrediente(Ingrediente);
        list_ingredientes.Remove(Ingrediente);
        
    }

    private void Adicionar_ui_prato()
    {
        s_horizontal.Adicionar_prato(Prato_atual);
    }

    private void Remover_prato()
    {
        Prato_atual = null;

        s_horizontal.Remover_prato();
    }
}
