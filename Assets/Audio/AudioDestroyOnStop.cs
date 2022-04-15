using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroyOnStop:MonoBehaviour {
	AudioSource player;	
	void Start() {
		player=GetComponent<AudioSource>();
	}
	void Update() {
		if(!player.isPlaying) Destroy(gameObject);
	}
}
