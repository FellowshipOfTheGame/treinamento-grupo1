using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {

	public string name;
	public float vol; // Volume original
	public AudioClip clip;
	public AudioSource source;
	public bool loop;
}
