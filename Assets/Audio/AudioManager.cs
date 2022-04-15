using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager:MonoBehaviour {

	public static AudioManager instance { get; private set; }
	private void Start() {
		instance=this;
	}

	[SerializeField] GameObject prefab;

	public void PlayClip(AudioClip clip,Vector2 position) {
		GameObject go = Instantiate(prefab,position,Quaternion.identity);
		go.GetComponent<AudioSource>().PlayOneShot(clip);
	}

}