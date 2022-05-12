using UnityEngine;

// Muda o tamanho de um objeto em uma build Web GL.
[AddComponentMenu("Scripts/Utility/Change Size In Web GL")]
public class ChangeSizeInWebGL : MonoBehaviour {

	public RectTransform target;

	public Vector2 size;

	void Start () {
		# if UNITY_WEBGL
			target.sizeDelta = size;
		# endif
	}
}