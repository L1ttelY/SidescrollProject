using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageField:MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision) {
		DamagableBase other = collision.GetComponent<DamagableBase>();
		if(!other) return;
		other.Damage();
	}

}
