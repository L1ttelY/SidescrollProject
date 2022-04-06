using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezableBase:MonoBehaviour {
	protected virtual void Start() {
		FreezeEvent+=Freeze;
		UnfreezeEvent+=Unfreeze;
	}
	protected virtual void OnDestroy() {
		FreezeEvent-=Freeze;
		UnfreezeEvent-=Unfreeze;
	}
	public static event System.EventHandler<Vector2> FreezeEvent;
	public static event System.EventHandler<Vector2> UnfreezeEvent;
	public static void OnFreeze(object sender,Vector2 e) => FreezeEvent?.Invoke(sender,e);
	public static void OnUnfreeze(object sender,Vector2 e) => UnfreezeEvent?.Invoke(sender,e);


	protected virtual void Freeze(object sender,Vector2 e) { }
	protected virtual void Unfreeze(object sender,Vector2 e) { }
	protected virtual bool IfReactEvent(object sender,Vector2 position) { return false; }
}

public class LiquidFreezable:FreezableBase {

	[SerializeField] int layerMolten = 4;
	[SerializeField] int layerFrozen = 6;

	[SerializeField] Sprite spriteMolten;
	[SerializeField] Sprite spriteFrozen;

	new Collider2D collider;
	SpriteRenderer spriteRenderer;

	protected override void Start() {
		base.Start();
		collider=GetComponent<Collider2D>();
		spriteRenderer=GetComponent<SpriteRenderer>();
	}

	protected override void Freeze(object sender,Vector2 e) {
		if(!IfReactEvent(sender,e)) return;
		gameObject.layer=layerFrozen;
		collider.usedByEffector=false;
		collider.isTrigger=false;
		spriteRenderer.sprite=spriteFrozen;
	}

	protected override void Unfreeze(object sender,Vector2 e) {
		if(!IfReactEvent(sender,e)) return;
		gameObject.layer=layerMolten;
		collider.usedByEffector=true;
		collider.isTrigger=true;
		spriteRenderer.sprite=spriteMolten;
	}

	protected override bool IfReactEvent(object sender,Vector2 position) {
		return collider.OverlapPoint(position);
	}

}
