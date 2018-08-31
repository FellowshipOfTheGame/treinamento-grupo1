using UnityEngine;

[AddComponentMenu("Scripts/Utility/Activators/Start Activate")]
//ativa acoes quando starta
public class StartActivator : ActivatorBase {

  void Start(){
    ActivateTargets();
    }
}
