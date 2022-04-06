using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController:MonoBehaviour {

	void Start() {
		GetComponentReferences();
		CameraController.GetCameraPoi+=CameraController_GetCameraPoi;
	}


	private void OnDestroy() {
		CameraController.GetCameraPoi-=CameraController_GetCameraPoi;
	}

	private void CameraController_GetCameraPoi(object sender,CameraController.CameraPoiList e) {
		e.Add(new KeyValuePair<Vector2,float>(transform.position,1));
	}

	void Update() {
		jump.doJump=Input.GetKey(KeyCode.Space);
		fall.doFall=Input.GetKey(KeyCode.S);

		horizontalMovement.moveDirection=0;
		if(Input.GetKey(KeyCode.A)) horizontalMovement.moveDirection--;
		if(Input.GetKey(KeyCode.D)) horizontalMovement.moveDirection++;

		swim.swimDirection=0;
		if(Input.GetKey(KeyCode.S)) swim.swimDirection--;
		if(Input.GetKey(KeyCode.Space)) swim.swimDirection++;

		if(Input.GetMouseButtonDown(0)) {
			Vector2 castDirection = (CameraController.mousePosition-(Vector2)transform.position).normalized;
			magicCaster.CastMagic(transform.position,castDirection);
		}
		if(Input.GetMouseButtonUp(0)) magicCaster.EndCast();

		if(Input.GetKeyDown(KeyCode.Q)) magicCaster.magicBoltIndex--;
		if(Input.GetKeyDown(KeyCode.E)) magicCaster.magicBoltIndex++;

	}

	HorizontalMovementActionHandler horizontalMovement;
	FallActionHandler fall;
	SwimActionHandler swim;
	JumpActionHandler jump;
	MagicCaster magicCaster;
	void GetComponentReferences() {
		horizontalMovement=GetComponent<HorizontalMovementActionHandler>();
		jump=GetComponent<JumpActionHandler>();
		fall=GetComponent<FallActionHandler>();
		swim=GetComponent<SwimActionHandler>();
		magicCaster=GetComponent<MagicCaster>();
	}



}