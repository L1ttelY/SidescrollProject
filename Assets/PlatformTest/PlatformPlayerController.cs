using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerController:MonoBehaviour {

	FlyActionHandler fly;
	HorizontalMovementActionHandler move;
	DashActionHandler dash;

	private void Start() {
		move=GetComponent<HorizontalMovementActionHandler>();
		fly=GetComponent<FlyActionHandler>();
		dash=GetComponent<DashActionHandler>();
		CameraController.GetCameraPoi+=CameraController_GetCameraPoi;
	}

	private void OnDestroy() {
		CameraController.GetCameraPoi-=CameraController_GetCameraPoi;
	}

	private void CameraController_GetCameraPoi(object sender,CameraController.CameraPoiList e) {
		e.Add(new KeyValuePair<Vector2,float>(transform.position,1));
	}

	private void FixedUpdate() {

		move.moveDirection=0;
		if(Input.GetKey(KeyCode.A)) move.moveDirection--;
		if(Input.GetKey(KeyCode.D)) move.moveDirection++;

		Vector2 direction = Vector2.zero;
		if(Input.GetKey(KeyCode.A)) direction.x--;
		if(Input.GetKey(KeyCode.D)) direction.x++;
		if(Input.GetKey(KeyCode.S)) direction.y--;
		if(Input.GetKey(KeyCode.Space)||Input.GetKey(KeyCode.W)) direction.y++;
		dash.targetDirection=direction;
		dash.doDash=direction!=Vector2.zero&&(Input.GetKey(KeyCode.LeftControl)||Input.GetKey(KeyCode.K));

		fly.doFly=Input.GetKey(KeyCode.Space);

	}

}
