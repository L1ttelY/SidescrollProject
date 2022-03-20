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
		CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		collider.size=new Vector2(collider.size.x*width,collider.size.y);
		spriteRenderer.size=new Vector2(spriteRenderer.size.x*width,spriteRenderer.size.y);

		transform.rotation=((Angle)(-normal)+(Angle)90).quaternion;

	}

}
