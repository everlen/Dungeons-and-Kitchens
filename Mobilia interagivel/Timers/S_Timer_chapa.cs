using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Timer_chapa : MonoBehaviour {

    float tempo = 0, tempo_max, tempo_ideal, tempo_queimando;
    int quant_virar;
    Color cor_material_cru, cor_material_bom, cor_material_queimando;
    bool parar = true;
    GameObject Img_virar;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!parar)
        {
            tempo = tempo + Time.deltaTime;

            if (tempo < tempo_ideal)
            {

            }
            else if (tempo < tempo_queimando)
            {
                if(Img_virar.GetComponent<Image>().color != cor_material_bom)
                {
                    Debug.Log("Bom");
                    Img_virar.GetComponent<Image>().color = cor_material_bom;
                }
            }
            else if(tempo < tempo_max)
            {
                if(Img_virar.GetComponent<Image>().color != cor_material_queimando)
                {
                    Debug.Log("Queimando");
                    Img_virar.GetComponent<Image>().color = cor_material_queimando;
                }

            }
            else
            {
                parar = true;
                GetComponent<S_Chapa>().Queimou();
            }

            
        }

    }

    public void Contabilizar(float Tempo_max, float Tempo_ideal_virar, float Tempo_queimando, int Quant_virar, GameObject img_virar)
    {
        tempo = 0;
        tempo_max = Tempo_max;
        tempo_ideal = Tempo_ideal_virar;
        tempo_queimando = Tempo_queimando;
        quant_virar = Quant_virar;
        Img_virar = img_virar;

        cor_material_cru = new Color(0.8f, 0.08f, 0.04f);
        cor_material_bom = new Color(0.63f, 0.37f, 0.29f);
        cor_material_queimando = new Color(0.13f, 0.13f, 0.13f);

        Img_virar.GetComponent<Image>().color = cor_material_cru;

        parar = false;
    }

    public float Finalizar_timer()
    {
        parar = true;
        return tempo;
    }

    public bool Virar_ingrediente()
    {
        bool retorno = false;

        tempo = 0;
        quant_virar--;
        tempo = 0;
        Img_virar.GetComponent<Image>().color = cor_material_cru;

        if (quant_virar == 1)
        {
            //Debug.Log("Remover ingrediente");
        }
        else if (quant_virar == 0)
        {
            Img_virar.GetComponent<Image>().color = cor_material_bom;
            retorno = true;
        }

        return retorno;
    }

}
