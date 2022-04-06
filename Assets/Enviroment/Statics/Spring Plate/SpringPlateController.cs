using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlateController:MonoBehaviour {

	private void Start() {
		GetComponentReferences();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		EntityBase entity = collision.GetComponent<EntityBase>();
		if(entity) StartPush();
	}

	private void Update() {

		if(pushing) {
			timePushed+=Time.deltaTime;
			if(timePushed>pushTime) {
				pushing=false;
			}
		}

		float currentY = minY;
		if(pushing) {
			float t = pushCurve.Evaluate(timePushed/pushTime);
			currentY=Mathf.Lerp(minY,maxY,t);
		}

		spriteRenderer.size=new Vector2(spriteRenderer.size.x,currentY);
		pusher.SetPosition(currentY);

	}

	[SerializeField] float minY;
	[SerializeField] float maxY;
	[SerializeField] AnimationCurve pushCurve;
	[SerializeField] float pushTime;

	void GetComponentReferences() {
		pusher=GetComponentInChildren<SpringPlatePusher>();
		spriteRenderer=GetComponent<SpriteRenderer>();
	}
	SpringPlatePusher pusher;
	SpriteRenderer spriteRenderer;

	bool pushing;
	float timePushed;
	void StartPush() {
		if(pushing) return;
		pushing=true;
		timePushed=0;
	}

}
