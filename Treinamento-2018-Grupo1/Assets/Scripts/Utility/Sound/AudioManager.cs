using System;
using UnityEngine;
using UnityEngine.Audio;

[AddComponentMenu("Scripts/Utility/Audio Manager")]
public class AudioManager : MonoBehaviour {

	public Sound[] sfxs;
	public Sound[] musics;
	public static AudioManager instance;
	public float volumeMusic = 1f;

	void Awake(){
		//evitando que audioManager seja destruido ou duplicado
		if(instance == null)
			instance = this;
		else{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);

		foreach(Sound s in sfxs){
			if(s.source == null)s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume =  s.vol;
		}

		foreach(Sound s in musics){
			if(s.source == null)s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume =  s.vol * volumeMusic;
		}
	}

	public void mudarVolumeMusic(){
		foreach(Sound s in musics){
			s.source.volume =  s.vol * volumeMusic;
		}
	}



	public void play(string name, bool sfx){
		Sound s = null;
		if(sfx) s =Array.Find(sfxs, sound =>sound.name == name);
		else s =Array.Find(musics, sound =>sound.name == name);

		if(s !=null)s.source.Play();
			else Debug.Log(name + " nao encontrado");
	}
}
