using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour {

	public static bool pausado = false;
	public scrollBarOptions[] scrolls;
	public GameObject player;
	public GameObject menuOptionsUI;
	public static int pos = 0;//posicao atual
	public GameObject audioManager;
	public Color corHighlighted;//cor caso item selecionado
	public Color corNormal;//cor caso item nao selecionado


	public void Update(){
		
		if(pausado){
			//Saindo do menu
			if(Input.GetKeyDown(KeyCode.Escape))
				sairMenu();
			//arrumando posicao do highlighted
			else if(Input.GetKeyDown(KeyCode.W)){
				int novaPos;
				if(pos ==0)
					novaPos = 2;
				else 
					novaPos = (pos-1) % 3;
				mudarPos(novaPos);
			}else if(Input.GetKeyDown(KeyCode.S))
				mudarPos( (pos+1) % 3);
			else if(Input.GetKeyDown(KeyCode.A)){
				scrolls[pos].scroll.GetComponent<Scrollbar>().value -= 0.15f;
				if(scrolls[pos].scroll.GetComponent<Scrollbar>().value < 0f)scrolls[pos].scroll.GetComponent<Scrollbar>().value = 0f;

			}
			else if(Input.GetKeyDown(KeyCode.S)){
				scrolls[pos].scroll.GetComponent<Scrollbar>().value += 0.15f;
				if(scrolls[pos].scroll.GetComponent<Scrollbar>().value > 1f)scrolls[pos].scroll.GetComponent<Scrollbar>().value = 1f;
			}

			//evitando que efx e music sejam maior que som geral
			if(scrolls[0].scroll.GetComponent<Scrollbar>().value < scrolls[1].scroll.GetComponent<Scrollbar>().value)
				scrolls[1].scroll.GetComponent<Scrollbar>().value = scrolls[0].scroll.GetComponent<Scrollbar>().value;
			if(scrolls[0].scroll.GetComponent<Scrollbar>().value < scrolls[2].scroll.GetComponent<Scrollbar>().value)
				scrolls[2].scroll.GetComponent<Scrollbar>().value = scrolls[0].scroll.GetComponent<Scrollbar>().value;
			
		}

	}

	public void entrarMenu(){
		pausado = true;
		pos = 0;
		Time.timeScale = 0f;
		menuOptionsUI.SetActive(true);
		mudarPos(0);
	}
	public void sairMenu(){
		pausado = false;
		Time.timeScale = 1f;
		menuOptionsUI.SetActive(false);
		mudarPos(0);
		FindObjectsOfType<MenuPause>()[0].GetComponent<MenuPause>().entrarMenu();
	}

	public void mudarVolumeSfx(){
		float aux = scrolls[2].scroll.GetComponent<Scrollbar>().value;
		if(aux > scrolls[0].scroll.GetComponent<Scrollbar>().value)aux = scrolls[0].scroll.GetComponent<Scrollbar>().value;
		audioManager.GetComponent<AudioManager>().volumesfx = aux;
		audioManager.GetComponent<AudioManager>().mudarVolumeSfx();
	}

	public void mudarVolumeMusic(){
		float aux = scrolls[1].scroll.GetComponent<Scrollbar>().value;
		if(aux > scrolls[0].scroll.GetComponent<Scrollbar>().value)aux = scrolls[0].scroll.GetComponent<Scrollbar>().value;
		audioManager.GetComponent<AudioManager>().volumeMusic = aux;
		audioManager.GetComponent<AudioManager>().mudarVolumeMusic();
	}
	public void mudarVolumeTotal(){
		mudarVolumeMusic();
		mudarVolumeSfx();
	}

	public void mudarPos(int novaPos){
		ColorBlock cb = scrolls[pos].button.GetComponent<Button>().colors;
		cb.normalColor = corNormal;
		scrolls[pos].button.GetComponent<Button>().colors = cb;
		pos = novaPos;

		cb = scrolls[pos].button.GetComponent<Button>().colors;
		cb.normalColor = corHighlighted;
		scrolls[pos].button.GetComponent<Button>().colors = cb;
	}

}
