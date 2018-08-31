using UnityEngine;

[AddComponentMenu("Scripts/Utility/Activators/On Trigger Activate")]
//ativa acoes quando starta
public class StartActivator : ActivatorBase {

  void Start(){
    ActivateTargets();
    }
}
