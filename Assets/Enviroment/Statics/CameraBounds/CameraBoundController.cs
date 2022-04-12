using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundController:MonoBehaviour {

	[field: SerializeField] public BoxCollider2D outerBound { get; private set; }
	[field: SerializeField] public BoxCollider2D innerBound { get; private set; }

	public static List<CameraBoundController> instances = new List<CameraBoundController>();
	private void Start() {
		instances.Add(this);
	}
	private void OnDestroy() {
		instances.Remove(this);
	}

}