using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBoltShockwave:MagicBoltBase {

	[SerializeField] GameObject shockwavePrefab;

	bool exploded;

	protected override void OnImpact(Vector2 normal) {
		if(exploded) return;
		exploded=true;
		Instantiate(shockwavePrefab,transform.position,Quaternion.identity);
		base.OnImpact(normal);
	}

}
