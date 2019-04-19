using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Pratos_existentes : MonoBehaviour {

    public List<SObj_Prato_finalizado> list_obj_pratos_existentes;
    public List<Model_Prato_finalizado> list_pratos_existentes;

    private void Awake()
    {
        foreach(SObj_Prato_finalizado prato_finalizado in list_obj_pratos_existentes)
        {
            Model_Prato_finalizado Prato_finalizado = gameObject.AddComponent<Model_Prato_finalizado>();
            Prato_finalizado.Definir_dados(prato_finalizado);
            list_pratos_existentes.Add(Prato_finalizado);
        }

    }
}
