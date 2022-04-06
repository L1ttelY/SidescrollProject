using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGroundedTester:MonoBehaviour {

	[SerializeField] float crotchHeightNormalized;

	new Collider2D collider;
	private void Start() {
		standingOn=new GameObject[10];
		collider=GetComponent<Collider2D>();
		filterGround=Utility.GetFilterByLayerName("Solid");
		filterWater=Utility.GetFilterByLayerName("Water");
		filterWater.useTriggers=true;
	}

	ContactFilter2D filterGround;
	ContactFilter2D filterWater;
	private void FixedUpdate() {

		UpdateIfGrounded();
		UpdateIfCeilinged();
		UpdateIfSubmerged();

	}

	void UpdateIfGrounded() {
		//检测是否落地
		Bounds bound = collider.bounds;

		Vector2 size = new Vector2(bound.size.x-0.0001f,0.01f);
		Vector2 position = new Vector2(bound.center.x,bound.min.y);
		int cnt = Physics2D.BoxCast(position,size,0,Vector2.down,filterGround,Utility.raycastBuffer,0.01f);
		standingOnSize=0;

		grounded=false;
		for(int i = 0;i<cnt;i++) {
			ref RaycastHit2D hit = ref Utility.raycastBuffer[i];
			if(hit.normal.y!=0) {
				grounded=true;
				standingOn[standingOnSize]=hit.collider.gameObject;
				standingOnSize++;
			}
		}

		if(grounded) timeNotGrounded=0;
		else timeNotGrounded+=Time.deltaTime;

	}



	void UpdateIfCeilinged() {
		//检测是否头顶天花板
		Bounds bound = collider.bounds;

		Vector2 size = new Vector2(bound.size.x-0.0001f,0.01f);
		Vector2 position = new Vector2(bound.center.x,bound.max.y);
		int cnt = Physics2D.BoxCast(position,size,0,Vector2.up,filterGround,Utility.raycastBuffer,0.01f);

		ceilinged=false;
		for(int i = 0;i<cnt;i++) {
			ref RaycastHit2D hit = ref Utility.raycastBuffer[i];
			if(hit.normal.y==0) continue;
			if(!hit.collider.usedByEffector) ceilinged=true;
		}

	}

	void UpdateIfSubmerged() {
		submerged=collider.IsTouchingLayers(filterWater.layerMask);

		if(!submerged) floating=false;
		else {
			Bounds bound = collider.bounds;
			float distance = bound.size.y*(1-crotchHeightNormalized);

			Vector2 size = new Vector2(bound.size.x,0.01f);
			Vector2 position = new Vector2(bound.center.x,bound.max.y);
			int cnt = Physics2D.BoxCast(position,size,0,Vector2.down,filterWater,Utility.raycastBuffer,distance);

			floating=cnt==0;

		}
	}

	[field: SerializeField]
	public bool grounded { get; private set; }
	[field: SerializeField]
	public float timeNotGrounded { get; private set; }
	[field: SerializeField]
	public bool ceilinged { get; private set; }
	[field: SerializeField]
	public bool submerged { get; private set; }
	[field: SerializeField]
	public bool floating { get; private set; }
	[field: SerializeField]
	public GameObject[] standingOn { get; private set; }
	public int standingOnSize;

}
