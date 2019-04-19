using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Objeto_pegavel : MonoBehaviour {
    bool Disponivel, selecionado;
    public GameObject pai_obj_interagivel;

    private void Awake()
    {
        Disponivel = true;
        selecionado = false;
        pai_obj_interagivel = null;

    }

    public bool Pegar_objeto(Transform transform_uso)
    {
        bool retorno = false;

        if (Disponivel)
        {
            Disponivel = false;
            transform.position = transform_uso.position;
            transform.SetParent(transform_uso);
            retorno = true;
        }
        

        return retorno;
    }

    public void Soltar_objeto(Transform local_soltar)
    {
        Disponivel = true;
        transform.SetParent(null);
        pai_obj_interagivel = null;
        transform.position = local_soltar.position;
        transform.rotation = local_soltar.rotation;

    }

    public void Posicionar_em_obj_interagivel(GameObject objeto_interagivel)
    {
        Disponivel = true;
        pai_obj_interagivel = objeto_interagivel;
        pai_obj_interagivel.GetComponentInChildren<S_Mover_obj_equipado>().Posicionar_obj(transform.gameObject);
        
    }

    public bool getSelecionado()
    {
        if (selecionado)
        {

            return true;
        }
        else
            return false;
    }
    public void setSelecionado(bool s)
    {
        selecionado = s;
    }

    public bool getDisponivel()
    {
        return Disponivel;
    }
}
