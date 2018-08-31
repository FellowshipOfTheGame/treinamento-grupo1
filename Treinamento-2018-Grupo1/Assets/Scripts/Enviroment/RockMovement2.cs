using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/Enviroment/Rock Movement 2")]
public class RockMovement2 : MonoBehaviour {

	public int ativo;//movimentando ou parado
	public float desaceleracao;
	public float tempoMaximoAr;

	public bool desativarCasoCheguePosFinal = false;
	private float vel;//velocidade atual

	private int direcao = 0;//0=parado ponto minimo;1=subindo;2=parado ponto max;3=descendo
	public Transform pontoFinal;	//pontos  fim do percurso
	private Vector3 posFinal;//position final
	private Vector3 posInicio;//position inicial

	[HideInInspector]
	public Vector3 novaPosFinal = Vector3.zero;
	private Transform trans;//transform da rocha

	public List<ActionBase> acoesEntrandoPosFinal;

	public List<ActionBase> acoesEntrandoPosInicial;

	public List<ActionBase> acoesSaindoPosFinal;
	void Start () {
		//recebendo os pontos iniciais e finais
		trans = GetComponent<Transform>();
		posInicio = trans.position;
		posFinal = pontoFinal.GetComponent<Transform>().position;
	}
	
	public void impulsionar(){
		ativo = 1;
		vel = Mathf.Sqrt(2*desaceleracao*Vector2.Distance(trans.position,posFinal) );
		posInicio = trans.position;
		direcao = 1;
	}


	void FixedUpdate(){

		if(direcao == 1){
			vel -= desaceleracao;
			trans.position = Vector2.MoveTowards(trans.position, posFinal,ativo* vel);
			if((Vector2.Distance(trans.position,posFinal) <0.5)){//caso chegue no ponto final
				direcao = 2;
				vel = 0;
				chegouPosFinal();
				StartCoroutine(esperarCooldown());
			}
		}else if(direcao == 3){
			trans.position = Vector2.MoveTowards(trans.position, posInicio,ativo* vel);
			vel += desaceleracao;
			if((Vector2.Distance(trans.position,posInicio) <0.2)){//caso chegue no ponto inicial
				direcao = 0;
				vel = 0;
				ativo = 0;
				chegouPosInicial();
			}
		}
	}


	IEnumerator esperarCooldown(){
		//espera cooldown
 		yield return new WaitForSeconds(tempoMaximoAr);
		direcao = 3;
		vel = 0;
		saindoPosFinal();
	}


	void chegouPosFinal(){
		//ativo = 0;

		for(int i=0;i< acoesEntrandoPosFinal.Count;i++)
			acoesEntrandoPosFinal[i].Activate();
	}

	void chegouPosInicial(){
		//ativo = 0;
		if(novaPosFinal != Vector3.zero){//definindo novo ponto final
			posFinal = novaPosFinal;
			novaPosFinal = Vector3.zero;
		}
		for(int i=0;i< acoesEntrandoPosInicial.Count;i++)
			acoesEntrandoPosInicial[i].Activate();
	}

	void saindoPosFinal(){
		
		for(int i=0;i< acoesSaindoPosFinal.Count;i++)
			acoesSaindoPosFinal[i].Activate();
		if( desativarCasoCheguePosFinal)ativo = 0;
	}
}
