using System.Collections.Generic;
using UnityEngine;

//Controla a funcionalidade de peso para botao
[AddComponentMenu("Scripts/Enviroment/Box Movement")]
public class BoxMovement : MonoBehaviour {

	public float peso;
    public GameObject botao;//botao o qual a caixa esta sobre
    private List<GameObject> boxs;//caixas imediatamente em cima 

    void Start(){
		boxs = new List<GameObject>();
	}
			

    void OnCollisionEnter2D(Collision2D other){
        //entrando em contato com botao
        if(other.gameObject.GetComponent<ButtonActivator>() != null && transform.position.y > other.gameObject.transform.position.y){
            entrarEmContato(other.gameObject);
            other.gameObject.GetComponent<ButtonActivator>().colocandoBox(gameObject);
            other.gameObject.GetComponent<ButtonActivator>().verificar();
        }
        //entrando em contato com box 
        else if(other.gameObject.GetComponent<BoxMovement>() != null  && transform.position.y < other.gameObject.transform.position.y){
            if(other.gameObject.GetComponent<BoxMovement>().botao == null){
                boxs.Add(other.gameObject);
                if( botao != null){
                    entrarEmContato(botao);
                    botao.gameObject.GetComponent<ButtonActivator>().verificar();
                }
            }
        }
        //entrando em contato com player
        else if(other.gameObject.tag == "Player" && botao != null){
            botao.gameObject.GetComponent<ButtonActivator>().setPlayerEmCima(1);
            botao.gameObject.GetComponent<ButtonActivator>().verificar();
        }
        //"colando" box na plataforma
        if(other.gameObject.tag == "Plataform")
            transform.SetParent (other.transform);

    }

    void OnCollisionExit2D(Collision2D other){
        //sai do contato com botao
        if(botao !=null && other.gameObject == botao){
            other.gameObject.GetComponent<ButtonActivator>().retirandoBox(gameObject);
            sairDoContato();
        }
        //saindo do contato com outra caixa
        else if(other.gameObject.transform.position.y > transform.position.y && other.gameObject.GetComponent<BoxMovement>() != null ){
            boxs.Remove(other.gameObject);
            if(botao != null){
                other.gameObject.GetComponent<BoxMovement>().sairDoContato();
            }
        }        
        //saindo do contato com player
        else if(other.gameObject.tag == "Player" && botao != null){
            botao.gameObject.GetComponent<ButtonActivator>().setPlayerEmCima(0);
        }

        if(other.gameObject.tag == "Plataform"){
            transform.SetParent (null);
        }
    }

    //soma dos pesos de todas as caixas
    public float somarPesos(){
        if(boxs.Count == 0 )return peso;
        float novoPeso = peso;
        for(int i =0;i<boxs.Count;i++){
            novoPeso += boxs[i].GetComponent<BoxMovement>().somarPesos();
        }
        return novoPeso;
    }

    //resetando valor de botao de todas as caixas
    public void sairDoContato(){
        botao = null;
        for(int i =0;i<boxs.Count;i++){
            boxs[i].GetComponent<BoxMovement>().sairDoContato();
        }
    }

    //colocando valor do botao em todas as caixas
    public void entrarEmContato(GameObject other){
        botao = other;
        for(int i =0;i<boxs.Count;i++){
            boxs[i].GetComponent<BoxMovement>().entrarEmContato(other);
        }
    }
}

