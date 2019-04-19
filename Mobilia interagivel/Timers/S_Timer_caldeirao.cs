using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Timer_caldeirao : MonoBehaviour {

    float tempo = 0, tempo_max, tempo_ideal;
    public float r_inicial, g_inicial, b_inicial;
    public float r_ideal, g_ideal, b_ideal;
    public float r_final, g_final, b_final;
    Color cor_material;
    GameObject Img_cozinhar;
    bool parar = true;

    private void Awake()
    {
        cor_material = new Color(r_inicial, g_inicial, b_inicial);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!parar)
        {
            tempo = tempo + Time.deltaTime;
            Vector3 rgb;

            if (tempo < tempo_ideal)
            {
                float lerp = tempo / tempo_ideal;

                rgb = Vector3.Lerp(new Vector3(r_inicial, g_inicial, b_inicial), new Vector3(r_ideal, g_ideal, b_ideal), lerp);

                cor_material.r = rgb.x;
                cor_material.g = rgb.y;
                cor_material.b = rgb.z;
            }
            else if (tempo < tempo_max)
            {
                float lerp = (tempo - tempo_ideal) / (tempo_max - tempo_ideal);

                rgb = Vector3.Lerp(new Vector3(r_ideal, g_ideal, b_ideal), new Vector3(r_final, g_final, b_final), lerp);

                cor_material.r = rgb.x;
                cor_material.g = rgb.y;
                cor_material.b = rgb.z;
            }
            else
            {
                parar = true;
                GetComponent<S_Caldeirao>().Queimou();
            }

            Img_cozinhar.GetComponent<Image>().color = cor_material;
        }

    }

    public void Contabilizar(float Tempo_max, float Tempo_ideal, GameObject img_cozinhar)
    {
        tempo = 0;
        tempo_max = Tempo_max;
        tempo_ideal = Tempo_ideal;
        Img_cozinhar = img_cozinhar;
        Img_cozinhar.GetComponent<Image>().color = cor_material;
        parar = false;
    }

    public float Finalizar_timer()
    {
        parar = true;
        return tempo;
    }

}
