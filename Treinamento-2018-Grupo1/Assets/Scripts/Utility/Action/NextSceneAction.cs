using UnityEngine;

// Esta ação carrega a próxima cena definida em por um objeto SceneScript.
[AddComponentMenu("Scripts/Utility/Actions/Next Scene")]
public class NextSceneAction : ActionBase {

    public override void Activate() {
        if (SceneScript.currentSceneScript != null)
        
            SceneScript.currentSceneScript.NextScene();
        else
            Debug.LogError("(NextSceneAction) Scene script not found on current scene!");

    }
}
