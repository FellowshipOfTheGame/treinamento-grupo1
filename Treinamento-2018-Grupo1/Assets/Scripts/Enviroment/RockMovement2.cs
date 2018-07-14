using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMovement2 : MonoBehaviour {

	public int ativo;//movimentando ou parado
	public float desaceleracao;
	public float tempoMaximoAr;
	private float tempoRestanteNoAr;
	private float vel;//velocidade atual

	private int direcao = 0;//0=parado ponto minimo;1=subindo;2=parado ponto max;3=descendo
	public Transform pontoFinal;	//pontos  fim do percurso
	private Vector3 posFinal;//position final
	private Vector3 posInicio;//position inicial
	private Transform trans;//transform da rocha
	void Start () {
		//recebendo os pontos iniciais e finais
		trans = trans = GetComponent<Transform>();
		posInicio = trans.position;
		posFinal = pontoFinal.GetComponent<Transform>().position;
		tempoRestanteNoAr = tempoMaximoAr;
	}
	
	public void impulsionar(){
		vel = Mathf.Sqrt(2*desaceleracao*Vector2.Distance(trans.position,posFinal) );
		direcao = 1;
	}


	void FixedUpdate(){

		if(direcao == 1){
			vel -= desaceleracao;
			trans.position = Vector2.MoveTowards(trans.position, posFinal,ativo* vel);
			if((Vector2.Distance(trans.position,posFinal) <0.3)){//caso chegue no ponto final
				direcao = 2;
				vel = 0;
			}
		}else if(direcao == 3){
			trans.position = Vector2.MoveTowards(trans.position, posInicio,ativo* vel);
			vel += desaceleracao;
			if((Vector2.Distance(trans.position,posInicio) <0.1)){//caso chegue no ponto inicial
				direcao = 0;
				vel = 0;
			}
		}


		//timer de quanto tempo fica parado no ar
		if(direcao == 2){
			tempoRestanteNoAr--;
			if(tempoRestanteNoAr < 0){
				tempoRestanteNoAr = tempoMaximoAr;
				direcao = 3;
				vel = 0;
			}
		}
	}
}
