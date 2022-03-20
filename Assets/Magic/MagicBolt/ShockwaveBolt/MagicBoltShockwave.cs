using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBoltShockwave:MagicBoltBase {

	[SerializeField] GameObject shockwavePrefab;

	protected override void OnImpact(Vector2 normal) {

		Instantiate(shockwavePrefab,transform.position,Quaternion.identity);
		base.OnImpact(normal);
	}

}
