using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_UI_Ingrediente : MonoBehaviour
{
    Image Img;
    GameObject camera;
    bool is_ingrediente;
    string nome;
    //public Text text;

    public void Definir_dados(string nome, Sprite sprite, bool Is_ingrediente)
    {
        //text.text = nome;
        this.nome = nome;
        Img = GetComponent<Image>();
        Img.sprite = sprite;
        is_ingrediente = Is_ingrediente;
        //Debug.Log("UI_Ingrediente é ingrediente? " + is_ingrediente);
    }

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.down);
    }

    public void Button_click()
    {
        //Debug.Log("UI_Ingrediente é ingrediente? " + is_ingrediente);
        GetComponentInParent<S_UI_Horizontal_organize_bancada>().Pegar_item(this.gameObject, is_ingrediente);
    }

    public string getNome()
    {
        return nome;
    }
}
