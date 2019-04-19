using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Ativar : MonoBehaviour
{
    public GameObject obj;

    private void Start()
    {
        //obj.gameObject.SetActive(false);
    }

    public void Ativar_canvas(bool ativado)
    {
        obj.gameObject.SetActive(ativado);
    }
}
