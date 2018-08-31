using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Scripts/Menu/Pause")]
public class PauseMenu : MonoBehaviour {

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

        if (GameController.gameController == null)
            return;

        //entrando/saindo do menu
        if (Input.GetKeyDown(KeyCode.Escape)){
			if(GameController.gameController.currentState == GameController.GameState.Paused)
				sairMenu();
			else
				entrarMenu();
		}
		else if(GameController.gameController.currentState == GameController.GameState.Paused) {
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
        GameController.gameController.currentState = GameController.GameState.Paused;
		pos = 0;
		Time.timeScale = 0f;
		menuPausaUI.SetActive(true);
		mudarPos(0);
	}
	public void sairMenu(){
        GameController.gameController.currentState = GameController.GameState.Play;
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
        GameController.gameController.RestartLevel();
	}
	//sair para tel principal
	public void Exit(){
		GameController.gameController.QuitToMainMenu();
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
