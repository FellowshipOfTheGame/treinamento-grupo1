using UnityEngine;

// Esta ação carrega a próxima cena definida em por um objeto SceneScript.
[AddComponentMenu("Scripts/Utility/Actions/Next Scene")]
public class NextSceneAction : ActionBase {

    // Delay até carregar a próxima cena.
    public float delay = 1;
    private float currentTime;
    private bool loading = false;

    private void Update() {

        if(currentTime != 0) {

            currentTime += Time.deltaTime;

            if (currentTime >= delay && !loading) {

                loading = true;

                if (SceneScript.currentSceneScript != null)

                    SceneScript.currentSceneScript.NextScene();
                else
                    Debug.LogError("(NextSceneAction) Scene script not found on current scene!");
            }
        }
    }

    public override void Activate() {

        GameController.instance.transition = true;
        currentTime += Time.deltaTime;

    }
}
