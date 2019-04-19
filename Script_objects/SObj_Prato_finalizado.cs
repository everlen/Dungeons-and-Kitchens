using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Novo Prato_finalizado", menuName = "Comida/Prato_finalizado")]
public class SObj_Prato_finalizado : ScriptableObject {
/*    
 *    -id = filho do obj referente ao ID; 
      0 = intacta;
      1 = cortada;
      2 = cozida;
      4 = frita;
      8 = sobras;
*/
    public int Id;
    public string Nome;
    public float Preco;
    public Sprite Img_prato;
    public List<SObj_Ingrediente> Ingredientes_necessarios;
    public List<int> Processo_necessario_ingredientes;
    public int Tempo_min_entrega;
    public int Tempo_max_entrega;
    public GameObject Obj_prato_finalizado;
}
