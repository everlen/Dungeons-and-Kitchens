using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Objeto_interagivel : MonoBehaviour{
    public Vector3 posiçao_uso;
    bool Player_posicionado = false;

    private void Awake()
    {
        posiçao_uso = this.transform.GetChild(0).transform.position;

    }


    public GameObject Usar_obj_interagivel(GameObject comida)
    {
        GameObject obj_retorno = null;   
        
//Segurando algum ingrediente ou prato
            if(comida != null)
            {               
                if (Tratar_por_categoria(comida))
                {
                    comida.GetComponent<S_Objeto_pegavel>().Posicionar_em_obj_interagivel(transform.gameObject);
                }
                else
                {
//Retorna o mesmo objeto equipado
                    obj_retorno = comida;
                }
            }  
//Maos vazias
            else
            {
                if (GetComponent<S_Bancada>() != null)
                {
                    obj_retorno = GetComponent<S_Bancada>().Usar_bancada(comida);
                }
                else if (GetComponent<S_Caldeirao>() != null)
                {
                    if (!GetComponent<S_Caldeirao>().Cozinhando)
                    {
                        GetComponent<S_Caldeirao>().Iniciar_cozinhar();
                    }
                    else
                    {
                        obj_retorno = GetComponent<S_Caldeirao>().Pegar_ingrediente();
                    }
                }
                else if (GetComponent<S_Chapa>() != null)
                {
                    if (!GetComponent<S_Chapa>().fritando)
                    {
                        GetComponent<S_Chapa>().Iniciar_fritura();
                    }
                    else if (GetComponent<S_Chapa>().Virar())
                    {
                        obj_retorno = GetComponent<S_Chapa>().Pegar_ingrediente();
                    }
                }
                else if (GetComponent<S_Tabua_corte>() != null)
                {
                    if (GetComponent<S_Tabua_corte>().finalizado)
                    {
                        obj_retorno = GetComponent<S_Tabua_corte>().Pegar_ingrediente();
                    }
                    else
                    {
                        Debug.Log("Finalize algum corte de ingrediente");
                    }
                }
                else if (GetComponent<S_Lixo>() != null)
                {
                    //uso_permitido = false;
                }
            }

        return obj_retorno;
    }

    public bool Tratar_por_categoria(GameObject comida)
    {
        bool uso_permitido = true;

        if (comida.GetComponent<Model_Prato_finalizado>() != null)
        {
            if (GetComponent<S_Bancada_entrega>() != null)
            {
                if (!GetComponent<S_Bancada_entrega>().Realizar_entrega(comida))
                    uso_permitido = false;
            }
            else if (GetComponent<S_Bancada>() != null)
            {
                if (GetComponent<S_Bancada>().Usar_bancada(comida) == comida)
                    uso_permitido = false;
            }
            else if (GetComponent<S_Caldeirao>() != null)
            {
                uso_permitido = false;
            }
            else if (GetComponent<S_Chapa>() != null)
            {
                uso_permitido = false;
            }
            else if (GetComponent<S_Tabua_corte>() != null)
            {
                uso_permitido = false;
            }
            else if (GetComponent<S_Lixo>() != null)
            {
                GetComponent<S_Lixo>().Jogar_fora(comida);
                uso_permitido = false;
            }

        }
        else if (comida.GetComponent<Model_Ingrediente>() != null)
        {
            //Processos culinarios
            if (GetComponent<S_Bancada_entrega>() != null)
            {
                uso_permitido = false;
            }
            else if (GetComponent<S_Caldeirao>() != null)
            {
                if (!GetComponent<S_Caldeirao>().Cozinhar(comida))
                    uso_permitido = false;
            }
            else if (GetComponent<S_Chapa>() != null)
            {
                if (!GetComponent<S_Chapa>().Fritar(comida))
                    uso_permitido = false;
            }
            else if (GetComponent<S_Tabua_corte>() != null)
            {
                if (!GetComponent<S_Tabua_corte>().Cortar(comida))
                    uso_permitido = false;
            }
            else if (GetComponent<S_Lixo>() != null)
            {
                GetComponent<S_Lixo>().Jogar_fora(comida);
                uso_permitido = false;
            }
            else if (GetComponent<S_Bancada>() != null)
            {
                if (GetComponent<S_Bancada>().Usar_bancada(comida) == comida)
                    uso_permitido = false;
            }

        }
        else
        {
            //Pratos e itens que não sejam comida nao podem ser posicionados nos itens interagiveis que realizam processos culinarios


        }

        Debug.Log("O usu permitido foi = "+ uso_permitido);

        return uso_permitido;
    }

    public bool getPlayer_posicionado()
    {
        if (Player_posicionado)
            return true;
        else
            return false;
    }

    public void setPlayer_posicionado(bool posição_player)
    {
        Player_posicionado = posição_player;
        Debug.Log(Player_posicionado);
    }
}
