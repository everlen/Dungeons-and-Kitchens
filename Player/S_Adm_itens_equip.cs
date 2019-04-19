using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Adm_itens_equip : MonoBehaviour {

    GameObject obj_equipado;
    List<GameObject> list_colisoes;
    Transform posiçao_uso;

    private void Awake()
    {
        list_colisoes = new List<GameObject>();
    }

    private void Start()
    {
        posiçao_uso = GetComponentInChildren<Model_posiçao_uso>().getPosiçao_uso();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        list_colisoes.Add(other.transform.gameObject);
    }


    private void OnTriggerExit(Collider other)
    {
        list_colisoes.Remove(other.transform.gameObject);
    }




    public void Interagir_obj_destino(GameObject obj_destino)
    {

        if (list_colisoes.Contains(obj_destino))
        {
            if (obj_destino.GetComponent<S_Objeto_interagivel>() != null)
            {
                GameObject obj_retorno_interativo = obj_destino.GetComponent<S_Objeto_interagivel>().Usar_obj_interagivel(obj_equipado);
                Interagir_item_pegavel(obj_retorno_interativo);
            }
            else if (obj_destino.GetComponent<S_Objeto_pegavel>() != null)
            {
// Verifica se a interação do player sera com o objeto pegavel ou o objeto interativo pai do pegavel
                if (obj_destino.GetComponent<S_Objeto_pegavel>().pai_obj_interagivel == null)
                {
                    Interagir_item_pegavel(obj_destino);
                }
                else
                {
                    GameObject obj_retorno_interativo = obj_destino.GetComponent<S_Objeto_pegavel>().pai_obj_interagivel.GetComponent<S_Objeto_interagivel>().Usar_obj_interagivel(obj_equipado);
                    Interagir_item_pegavel(obj_retorno_interativo);
                }
                
            }
        }     
        
    }

    public void Interagir_item_pegavel(GameObject item_pegavel)
    {
        //Debug.Log("OBJ Equipado: " + obj_equipado);
        if(item_pegavel != null)
        {
            if (obj_equipado == null)
            {
                if (item_pegavel.GetComponent<S_Objeto_pegavel>().Pegar_objeto(posiçao_uso))
                    setItem_equipado(item_pegavel);

            }
            else
            {
                if(item_pegavel == obj_equipado)
                {

                }
                else if (item_pegavel.GetComponent<S_Objeto_pegavel>().getDisponivel())
                {
                    if (item_pegavel.GetComponent<S_Objeto_pegavel>().Pegar_objeto(posiçao_uso))
                    {
                        obj_equipado.GetComponent<S_Objeto_pegavel>().Soltar_objeto(item_pegavel.transform);
                        setItem_equipado(item_pegavel);
                    }

                }
            }
        }
        else
        {
            setItem_equipado(null);
        }

    }

    public void setItem_equipado(GameObject item)
    {
        obj_equipado = item;
    }

    public GameObject getObj_equipado()
    {
        return obj_equipado;
    }
}
