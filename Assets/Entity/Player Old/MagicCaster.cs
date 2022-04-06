using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagicCaster:MonoBehaviour {

	[SerializeField] bool inheritVelocity;
	[SerializeField] GameObject[] magicBoltPrefabs;

	int _magicBoltIndex;
	public int magicBoltIndex {
		get => _magicBoltIndex;
		set {
			_magicBoltIndex=value;
			if(_magicBoltIndex<0) _magicBoltIndex+=magicBoltPrefabs.Length;
			_magicBoltIndex%=magicBoltPrefabs.Length;
			magicBoltPrefab=magicBoltPrefabs[_magicBoltIndex];
		}

	}
	GameObject magicBoltPrefab;
	MagicBoltBase lastCasted;
	new Rigidbody2D rigidbody;

	private void Start() {
		rigidbody=GetComponent<Rigidbody2D>();
		magicBoltPrefab=magicBoltPrefabs[0];
	}

	public void CastMagic(Vector2 position,Vector2 direction) {
		lastCasted=Instantiate(magicBoltPrefab,position,((Angle)direction).quaternion).GetComponent<MagicBoltBase>();
		lastCasted.Init(inheritVelocity ? rigidbody.velocity : Vector2.zero,gameObject);
	}

	public void EndCast() {
		if(lastCasted) {
			lastCasted.EndCast();
		}
	}

}
