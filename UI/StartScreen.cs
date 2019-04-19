using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

	private Animator animacaoBotao;
	[SerializeField] private List<GameObject> botoes;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ClicouIniciar(){
		animacaoBotao = botoes[0].GetComponent<Animator>();
		animacaoBotao.SetTrigger("Clicou");
		Invoke("ChamaCenaInicial", 0.3f);
	}
	void ChamaCenaInicial(){
		SceneManager.LoadScene("Scene_Everlen", LoadSceneMode.Single);
	}
	public void ClicouOpcoes(){
		animacaoBotao = botoes[1].GetComponent<Animator>();
		animacaoBotao.SetTrigger("Clicou");
	}

	public void ClicouSobre(){
		animacaoBotao = botoes[2].GetComponent<Animator>();
		animacaoBotao.SetTrigger("Clicou");
	}

	public void ClicouSair(){		
		animacaoBotao = botoes[3].GetComponent<Animator>();
		animacaoBotao.SetTrigger("Clicou");
	}
}
