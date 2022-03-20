using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagable:DamagableBase {

	public Vector2 respawnPoint;
	public static PlayerDamagable instance;

	private void Start() {
		instance=this;
		respawnPoint=transform.position;
	}

	public override bool Damage() {
		transform.position=respawnPoint;
		return true;
	}

}
