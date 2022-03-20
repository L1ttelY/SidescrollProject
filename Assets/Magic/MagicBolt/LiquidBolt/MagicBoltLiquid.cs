using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBoltLiquid:MagicBoltBase {

	static Dictionary<GameObject,GameObject> effects = new Dictionary<GameObject,GameObject>();

	[SerializeField] GameObject effectPrefab;
	[SerializeField] float minimumTime = 0.2f;

	float timer;
	bool ended;

	private void Update() {
		timer+=Time.deltaTime;
		if(timer>minimumTime&&ended) EndCast();
	}

	protected override void OnImpact(Vector2 normal) {

		GameObject effectGameObject = Instantiate(effectPrefab,transform.position,Quaternion.identity);

		if(!effects.ContainsKey(shooter)) {
			effects.Add(shooter,effectGameObject);
		} else {
			Destroy(effects[shooter]);
			effects[shooter]=effectGameObject;
		}

		LiquidEffectController effect = effectGameObject.GetComponent<LiquidEffectController>();

		effect.SetNormal(normal);

		base.OnImpact(normal);
	}

	public override void EndCast() {
		if(timer<minimumTime) {
			ended=true;
		} else {
			OnImpact(Vector2.zero);
		}
		base.EndCast();
	}

}
