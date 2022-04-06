using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPositionSetter:MonoBehaviour {

	[SerializeField] Vector2 offset;

	public void SetPlayerRespawn(){
		PlayerDamagable.instance.respawnPoint=(Vector2)transform.position+offset;
	}

}
