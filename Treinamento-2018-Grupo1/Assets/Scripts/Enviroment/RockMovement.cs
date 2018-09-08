using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/Enviroment/Rock Movement")]
public class RockMovement : MonoBehaviour {

	public int ativo;//movimentando ou parado
	public float velIr;//velocidade para ir

	public float tempoMaximoAr;

	public float velVoltar;//velocidade para voltar
	private int direcao = 1;

	public Transform pontoFinal;	//pontos  fim do percurso

	private Vector3 posFinal;//position final

	private Vector3 posInicio;//position inicial
	private Transform trans;//transform da rocha

	public List<ActionBase> acoesEntrandoPosFinal;

	public List<ActionBase> acoesEntrandoPosInicial;

	public List<ActionBase> acoesTerminaTimer;

	void Start () {
		//recebendo os pontos iniciais e finais
		trans = GetComponent<Transform>();
		posInicio = trans.position;
		posFinal = pontoFinal.GetComponent<Transform>().position;
	}
	

	void FixedUpdate() {

		if(direcao == 1){
			trans.position = Vector2.MoveTowards(trans.position, posFinal,ativo* velIr);
			if((Vector2.Distance(trans.position,posFinal) <0.3)){//caso chegue no ponto final
				direcao = 2;
				StartCoroutine(esperarCooldown());
				chegouPosFinal();
			}
		}else if(direcao == 0){
			trans.position = Vector2.MoveTowards(trans.position, posInicio,ativo* velVoltar);
			if((Vector2.Distance(trans.position,posInicio) <0.1)){//caso chegue no ponto inicial
				direcao = 1;
				chegouPosInicial();
			}
		}


	}

	IEnumerator esperarCooldown(){
		direcao = 2;
		//espera cooldown
 		yield return new WaitForSeconds(tempoMaximoAr);
		terminaTimer();
		direcao =0;
	}

	//muda estado atual de ativo
	public void mudarEst(){
		if(ativo == 1)ativo = 0;
		else if(ativo == 0)ativo = 1;
	}

	protected virtual void chegouPosFinal(){
		//ativo = 0;

		for(int i=0;i< acoesEntrandoPosFinal.Count;i++)
			acoesEntrandoPosFinal[i].Activate();
	}

	protected virtual void chegouPosInicial(){
		//ativo = 0;

		for(int i=0;i< acoesEntrandoPosInicial.Count;i++)
			acoesEntrandoPosInicial[i].Activate();
	}

	protected virtual void terminaTimer(){
		//ativo = 0;

		for(int i=0;i< acoesTerminaTimer.Count;i++)
			acoesTerminaTimer[i].Activate();
	}
}
