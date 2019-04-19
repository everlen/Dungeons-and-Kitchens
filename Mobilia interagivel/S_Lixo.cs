using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Lixo : MonoBehaviour {

    public void Jogar_fora(GameObject comida)
    {
        Destroy(comida);
    }

}
