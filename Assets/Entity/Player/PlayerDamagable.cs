using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Void();

public class PlayerDamagable:DamagableBase {

	public Vector2 respawnPoint;
	public static PlayerDamagable instance;
	Rigidbody2D rigidbody;

	public static event Void PlayerDamage;

	private void Start() {
		instance=this;
		rigidbody=GetComponent<Rigidbody2D>();
		respawnPoint=transform.position;
	}

	public override bool Damage() {
		PlayerDamage?.Invoke();
		transform.position=respawnPoint;
		rigidbody.velocity=Vector2.zero;
		return true;
	}

}
