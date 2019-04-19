using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Mover_obj_equipado : MonoBehaviour {

    bool ocupado = false;
    public bool Mover_objeto_equipado = false;
    public float velocidade = 8;
	
	void Update () {
        if (Mover_objeto_equipado)
        {
            if (ocupado)
            {
                transform.Rotate(Vector3.up, velocidade * Time.deltaTime);          
            }
        }
	}
    public void setOcupado(bool oc)
    {
        ocupado = oc;
    }

    public void Posicionar_obj(GameObject obj)
    {
        obj.transform.parent = transform;
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        ocupado = true;
    }
}
