using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("ª˙πÿ/ ‰»Î/ButtonController")]
public class ButtonController:MonoBehaviour {

	[SerializeField] UnityEvent<bool> SetState;

	[SerializeField] float threshold = 0.5f;

	private void FixedUpdate() {



		if(transform.localPosition.y<threshold) {
			SetState?.Invoke(true);
			on=true;
		} else {
			SetState?.Invoke(false);
			on=false;
		}
	}

	public bool on;

}
