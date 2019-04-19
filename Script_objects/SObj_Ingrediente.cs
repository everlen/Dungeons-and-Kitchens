using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Novo Ingrediente", menuName = "Comida/Ingrediente")]
public class SObj_Ingrediente : ScriptableObject {

    public string Nome;
    public int Id, Tempo_ideal_cozinhar, Tempo_max_cozinhar, tempo_virar_fritar, tempo_queimando_fritar, tempo_max_fritar, quant_virar_fritar, Quant_cortar;
    public Sprite Img_ingrediente;
    public bool Fritar_acompanhado, Cozinhar_acompanhado;
}
