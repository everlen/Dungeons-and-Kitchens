using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	[SerializeField] private int crystalCoin = 30;
	[SerializeField] private int goldCoin = 250;
	[SerializeField] private int potion = 5;

	[SerializeField] private Text Num_Crystal;
	[SerializeField] private Text Num_Gold;
	[SerializeField] private Text Num_Potion;
	//Animator
	[SerializeField] private Animator btnCozinheiro;
	private bool btnCozinheiroOn = true;
	[SerializeField] private Animator btnInventario;
	private bool btnInventarioOn = false;
	[SerializeField] private Animator btnLoja;
	[SerializeField] private bool btnLojaOn = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		AtualizaCoins();
	}

	void AtualizaCoins(){
		Num_Crystal.text = crystalCoin.ToString();
		Num_Gold.text = goldCoin.ToString();
		Num_Potion.text = potion.ToString();
	}

	public void ClickInventario(){
		btnInventarioOn = true;
		btnCozinheiroOn = false;
		btnLojaOn = false;
		btnCozinheiro.SetBool("On", btnCozinheiroOn);
		btnInventario.SetBool("On", btnInventarioOn);
		btnLoja.SetBool("On", btnLojaOn);
	}

	public void ClickCozinheiro(){
		btnInventarioOn = false;
		btnCozinheiroOn = true;
		btnLojaOn = false;
		btnCozinheiro.SetBool("On", btnCozinheiroOn);
		btnInventario.SetBool("On", btnInventarioOn);
		btnLoja.SetBool("On", btnLojaOn);
	}

	public void ClickLoja(){
		btnInventarioOn = false;
		btnCozinheiroOn = false;
		btnLojaOn = true;
		btnCozinheiro.SetBool("On", btnCozinheiroOn);
		btnInventario.SetBool("On", btnInventarioOn);
		btnLoja.SetBool("On", btnLojaOn);
	}
}
