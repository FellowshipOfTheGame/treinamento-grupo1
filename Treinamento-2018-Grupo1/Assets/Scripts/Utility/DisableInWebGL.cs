using UnityEngine;

// Desabilita um objeto em uma build Web GL.
[AddComponentMenu("Scripts/Utility/Disable In Web GL")]
public class DisableInWebGL : MonoBehaviour {

	public GameObject target;

	void Start () {
		# if UNITY_WEBGL
			target.SetActive(false);
		# endif
	}
}