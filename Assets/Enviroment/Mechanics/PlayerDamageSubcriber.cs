using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("����/����/PlayerDamageSubcriber")]
public class PlayerDamageSubcriber:MonoBehaviour {
	void Start() {
		PlayerDamagable.PlayerDamage+=PlayerDamagable_PlayerDamage;
	}

	[SerializeField] UnityEvent onDamage;
	private void PlayerDamagable_PlayerDamage() {
		onDamage?.Invoke();
	}

	void OnDestroy() {
		PlayerDamagable.PlayerDamage-=PlayerDamagable_PlayerDamage;
	}
}
