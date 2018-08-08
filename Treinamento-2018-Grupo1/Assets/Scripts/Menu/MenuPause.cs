using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour {

	public static bool pausado = false;
	public GameObject player;
	public static int pos = 0;//posicao atual

	public GameObject menuPausaUI;
	private GameObject[] botoes;
	public GameObject button0;
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;
	public Color corHighlighted;//cor caso item selecionado
	public Color corNormal;//cor caso item nao selecionado

	void Start(){
		botoes = new GameObject[4];
		botoes[0] = button0;
		botoes[1] = button1;
		botoes[2] = button2;
		botoes[3] = button3;
	}

	public void Update(){
		//entrando/saindo do menu
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(pausado)
				sairMenu();
			else
				entrarMenu();
		}
		else if(pausado){
			//selecionando opções dentro do menu
			if(Input.GetKeyDown(KeyCode.W)){
				int novaPos;
				if(pos ==0)
					novaPos = 3;
				else 
					novaPos = (pos-1) % 4;
				mudarPos(novaPos);
			}else if(Input.GetKeyDown(KeyCode.S)){
				mudarPos( (pos+1) % 4);

			//atindo opção selecionada
			}else if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)){
				if(pos == 3)
					Exit();
				else if(pos == 2)
					Options();
				else if(pos == 1)
					Restart();
				else
					sairMenu();
			}
		}

	}

	public void entrarMenu(){
		pausado = true;
		pos = 0;
		Time.timeScale = 0f;
		menuPausaUI.SetActive(true);
		mudarPos(0);
	}
	public void sairMenu(){
		pausado = false;
		Time.timeScale = 1f;
		menuPausaUI.SetActive(false);
		mudarPos(0);
	}

	//entrar no menu de opcoes
	public void Options(){
		sairMenu();
		FindObjectsOfType<MenuOptions>()[0].GetComponent<MenuOptions>().entrarMenu();	
	}

	public void Restart(){
		sairMenu();
		player.GetComponent<Player>().morreu();
	}
	//sair para tel principal
	public void Exit(){
		//ainda não há cena para entrar
		//SceneManager.LoadScene("...");
	}

	public void mudarPos(int novaPos){
		ColorBlock cb = botoes[pos].GetComponent<Button>().colors;
		cb.normalColor = corNormal;
		botoes[pos].GetComponent<Button>().colors = cb;

		pos = novaPos;

		cb = botoes[pos].GetComponent<Button>().colors;
		cb.normalColor = corHighlighted;
		botoes[pos].GetComponent<Button>().colors = cb;
	}
}
