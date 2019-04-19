using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_B_Cortar : MonoBehaviour
{
    GameObject Obj_pai;
    public void setObj_pai(GameObject obj_pai)
    {
        Obj_pai = obj_pai;
        UnityEngine.UI.Button btn = GetComponent<UnityEngine.UI.Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(obj_pai.GetComponent<S_Tabua_corte>().Button_click);
        transform.position = Camera.main.WorldToScreenPoint(Obj_pai.transform.position + new Vector3(0,1,0));
    }
}
