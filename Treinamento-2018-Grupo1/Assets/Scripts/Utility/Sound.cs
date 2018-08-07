using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound {

	public string nome;
	public float vol;//volume original
	public AudioClip clip;
	public AudioSource source;
	public bool loop;
}
