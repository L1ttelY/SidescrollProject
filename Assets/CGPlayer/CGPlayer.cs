using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGPlayer:MonoBehaviour {

	public static bool isPlaying;
	Image image;

	int imageIndex;
	[SerializeField] Sprite[] sprites;
	[SerializeField] bool canEnd=true;
	void Start() {
		isPlaying=true;
		image=GetComponent<Image>(); 
		image.sprite=sprites[0];
	}

	private void Update() {
		if(Input.anyKeyDown) {
			imageIndex++;
			if(imageIndex>=sprites.Length) {
				if(canEnd) {
					isPlaying=false;
					image.color=Color.clear;
				}
			} else image.sprite=sprites[imageIndex];
		}
	}

}
