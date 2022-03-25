using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidEffectController:MagicEffectControllerBase {

	[SerializeField] float time;

	private void Update() {
		time-=Time.deltaTime;
		if(time<0) Destroy(gameObject);
	}

	public void SetNormal(Vector2 normal) {
		int width = normal==Vector2.zero ? 1 : 2;

		transform.rotation=((Angle)(-normal)).quaternion;
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		spriteRenderer.size=new Vector2(spriteRenderer.size.x*width,spriteRenderer.size.y);


	}

}
