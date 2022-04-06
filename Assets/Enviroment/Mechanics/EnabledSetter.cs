using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("����/���/EnabledSetter")]
public class EnabledSetter : MonoBehaviour{

	[SerializeField] bool isActivatedWhenOn=true;

	public void	SetState(bool state){
		gameObject.SetActive(state==isActivatedWhenOn);
	}

}
