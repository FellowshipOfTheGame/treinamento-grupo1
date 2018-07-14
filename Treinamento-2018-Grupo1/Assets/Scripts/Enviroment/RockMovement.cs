using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/Enviroment/Rock Movement")]
public class RockMovement : MonoBehaviour {

	public int ativo;//movimentando ou parado
	public float vel;//velocidade
	private int direcao = 1;

	public Transform pontoFinal;	//pontos  fim do percurso

	private Vector3 posFinal;//position final

	private Vector3 posInicio;//position inicial
	private Transform trans;//transform da rocha
	void Start () {
		//recebendo os pontos iniciais e finais
		trans = GetComponent<Transform>();
		posInicio = trans.position;
		posFinal = pontoFinal.GetComponent<Transform>().position;
	}
	

	void FixedUpdate() {

		if(direcao == 1){
			trans.position = Vector2.MoveTowards(trans.position, posFinal,ativo* vel);
			if((Vector2.Distance(trans.position,posFinal) <0.1))//caso chegue no ponto final
				direcao = 0;
		}else{
			trans.position = Vector2.MoveTowards(trans.position, posInicio,ativo* vel);
			if((Vector2.Distance(trans.position,posInicio) <0.1))//caso chegue no ponto inicial
				direcao = 1;
		}


	}

	//muda estado atual de ativo
	public void mudarEst(){
		if(ativo == 1)ativo = 0;
		else if(ativo == 0)ativo = 1;
	}
}
