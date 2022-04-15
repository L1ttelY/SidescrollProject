using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreaker:MonoBehaviour {

	[SerializeField] float breakTime;
	[SerializeField] float timeFading;
	[SerializeField] int layerBroken;
	Sprite spriteNormal;
	[SerializeField] Sprite spriteBroken;

	Noise2D noise = new Noise2D(10);
	public float breakProgression;
	[SerializeField] AnimationCurve breakMagnitudeCurve;
	[SerializeField] float baseBreakMagnitude = 0.1f;
	[SerializeField] AudioClip soundBreaking;
	[SerializeField] AudioClip soundBroke;

	int layerNormal;

	private void Start() {
		collider=GetComponent<Collider2D>();
		spriteRenderer=GetComponentInChildren<SpriteRenderer>();
		layerNormal=gameObject.layer;
		spriteNormal=spriteRenderer.sprite;
		if(spriteBroken==null) spriteBroken=spriteNormal;
	}

	bool breakingPrevious;
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

		bool breaking = breakProgression>0;
		if(broken) breaking=false;
		if(breaking&&!breakingPrevious){
			AudioManager.instance.PlayClip(soundBreaking,transform.position);
		}
		breakingPrevious=breaking;

	}

	float timeBroken;
	void UpdateVisual() {
		Vector3 visualPosition = Vector2.zero;
		if(broken) {
			timeBroken+=Time.deltaTime;
			if(timeBroken<timeFading) spriteRenderer.color=new Color(1,1,1,1-timeBroken/timeFading);
			else spriteRenderer.color=Color.clear;
			visualPosition=Vector3.zero;
			spriteRenderer.sprite=spriteBroken;
		} else {
			spriteRenderer.color=Color.white;
			timeBroken=0;
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

		AudioManager.instance.PlayClip(soundBroke,transform.position);
	}

}
