using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonActivator : ActivatorBase {

	public float pesoNecessario;//peso necessario para ativar botao
	[HideInInspector]
	public float pesoAtual = 0;
	public float pesoPlayer;//peso que player irá realizar caso esteja em cima

	public float velPlayer;//velocidade minima do player para apertar o botao
	private int PlayerEmCima = 0;//verifica se player esta em cima do botao(ou uma caixa)
	private List<GameObject> boxs;//caixas imediatamente em cima do botao


	public bool ativo = true;


	void Start(){
		boxs = new List<GameObject>();
	}
						
	public void colocandoBox(GameObject other){
		boxs.Add(other);
	}

	public void retirandoBox(GameObject other){
		boxs.Remove(other);
	}

	//setando se player esta ou nao em cima da caixa
	public void setPlayerEmCima(int valor){
		PlayerEmCima = valor;
	}
   void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().velY < - velPlayer){
            setPlayerEmCima(1);
            verificar();
        }
    }

	void OnCollisionExit2D(Collision2D other){
		if(other.gameObject.tag == "Player" ){
            setPlayerEmCima(0);
        }
    }

	//verifica se tem as condiçoes necessarias para ativar
	public void verificar(){
		//ativa as açoes
		pesoAtual = PlayerEmCima * pesoPlayer;
		for(int i =0;i<boxs.Count;i++){
      	  pesoAtual += boxs[i].GetComponent<BoxMovement>().somarPesos();
		}

		if( pesoAtual >= pesoNecessario && ativo){
			ActivateTargets();
			ativo = false;
		}
	}

	
}
