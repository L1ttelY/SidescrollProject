using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreaker:MonoBehaviour {

	[SerializeField] float breakTime;
	[SerializeField] int layerBroken;
	Sprite spriteNormal;
	[SerializeField] Sprite spriteBroken;

	int layerNormal;

	private void Start() {
		collider=GetComponent<Collider2D>();
		spriteRenderer=GetComponent<SpriteRenderer>();
		layerNormal=gameObject.layer;
		spriteNormal=spriteRenderer.sprite;
	}

	private void Update() {
		if(broken) {
			timeAfterBreak+=Time.deltaTime;

			if(timeAfterBreak>breakTime) {

				broken=false;
				gameObject.layer=layerNormal;
				collider.isTrigger=false;
				spriteRenderer.sprite=spriteNormal;

			}

		}
	}

	new Collider2D collider;
	SpriteRenderer spriteRenderer;

	float timeAfterBreak;
	public bool broken{ get; private set; }
	public void Break() {
		if(broken) return;

		timeAfterBreak=0;
		broken=true;

		gameObject.layer=layerBroken;
		collider.isTrigger=true;
		spriteRenderer.sprite=spriteBroken;

	}

}
