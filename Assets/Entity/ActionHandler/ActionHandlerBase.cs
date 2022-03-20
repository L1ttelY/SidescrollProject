using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandlerBase:MonoBehaviour {
	protected virtual void Start() {
		GetComponentReferences();
	}

	protected ObjectGroundedTester groundedTester;
	protected new Rigidbody2D rigidbody;
	protected virtual void GetComponentReferences() {
		groundedTester=GetComponent<ObjectGroundedTester>();
		rigidbody=GetComponent<Rigidbody2D>();
	}

}
