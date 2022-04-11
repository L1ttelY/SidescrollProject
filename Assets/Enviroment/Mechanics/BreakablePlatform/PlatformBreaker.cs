using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreaker:MonoBehaviour {

	[SerializeField] float breakTime;
	[SerializeField] int layerBroken;
	Sprite spriteNormal;
	[SerializeField] Sprite spriteBroken;

	Noise2D noise = new Noise2D(10);
	public float breakProgression;
	[SerializeField] AnimationCurve breakMagnitudeCurve;
	[SerializeField] float baseBreakMagnitude = 0.1f;

	int layerNormal;

	private void Start() {
		collider=GetComponent<Collider2D>();
		spriteRenderer=GetComponentInChildren<SpriteRenderer>();
		layerNormal=gameObject.layer;
		spriteNormal=spriteRenderer.sprite;
		if(spriteBroken==null) spriteBroken=spriteNormal;
	}

	private void Update() {
		UpdateVisual();
		if(broken) {
			timeAfterBreak+=Time.deltaTime;

			if(timeAfterBreak>breakTime) {

				broken=false;
				gameObject.layer=layerNormal;
				collider.isTrigger=false;

			}

		}
	}

	void UpdateVisual() {
		Vector3 visualPosition = Vector2.zero;
		if(broken) {
			visualPosition=Vector3.zero;
			spriteRenderer.sprite=spriteBroken;
		} else {
			spriteRenderer.sprite=spriteNormal;
			float breakMagnitude = breakMagnitudeCurve.Evaluate(breakProgression);
			if(breakProgression<0) breakMagnitude=0;
			visualPosition=noise.Sample(breakMagnitude*baseBreakMagnitude);
		}
		spriteRenderer.transform.position=transform.position+visualPosition;
	}

	new Collider2D collider;
	SpriteRenderer spriteRenderer;

	float timeAfterBreak;
	public bool broken { get; private set; }
	public void Break() {
		if(broken) return;

		timeAfterBreak=0;
		broken=true;

		gameObject.layer=layerBroken;
		collider.isTrigger=true;
		spriteRenderer.sprite=spriteBroken;

	}

}
