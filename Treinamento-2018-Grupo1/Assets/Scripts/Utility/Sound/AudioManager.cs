using UnityEngine;

[AddComponentMenu("Scripts/Utility/Audio Manager")]
public class AudioManager : MonoBehaviour {

	public Sound[] sfxs;
	public Sound[] musics;
	public static AudioManager instance;
	public float volumeMusic = 1f;
    public GameObject sourceTemplate;
    public string startingMusic;

    void Awake() {

		//evitando que audioManager seja destruido ou duplicado
		if(instance == null)
			instance = this;
		else{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		foreach(Sound s in sfxs){

            if (s.source == null) {
                GameObject src = Instantiate(sourceTemplate, transform.position, transform.rotation);
                src.transform.SetParent(transform);
                src.transform.name = s.clip.name;

                s.source = src.GetComponent<AudioSource>();
            }

			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume =  s.vol;

		}

		foreach(Sound s in musics){

			if(s.source == null)
                s.source = gameObject.AddComponent<AudioSource>();

			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume =  s.vol * volumeMusic;

		}

        PlaySound(startingMusic, false);
    }

	public void ChangeMusicVolume(){

		foreach(Sound s in musics){
			s.source.volume =  s.vol * volumeMusic;
		}

	}
    
	public void PlaySound(string name, bool sfx){

		Sound s = null;

		if(sfx)
            for(int i = 0; i < sfxs.Length; i++) {
                if (name == sfxs[i].name)
                    s = sfxs[i];
            }
		else
            for (int i = 0; i < musics.Length; i++) {
                if (name == musics[i].name)
                    s = musics[i];
            }

        if (s != null) {
            s.source.Play();
        } else
            Debug.Log("(Audio Manager) "+ name + " not found!");

	}
}
