using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound:MonoBehaviour {

	[SerializeField] AudioClip soundWalk;
	[SerializeField] AudioClip soundDash;
	[SerializeField] AudioClip soundBounceWall;
	[SerializeField] AudioClip soundBounceMirror;
	[SerializeField] AudioClip soundFly;
	[SerializeField] AudioClip soundLand;
	[SerializeField] AudioClip soundDamage;

	HorizontalMovementActionHandler move;
	DashActionHandler dash;
	ObjectGroundedTester grounded;
	AudioSource player;
	FlyActionHandler fly;

	void Start() {
		move=GetComponent<HorizontalMovementActionHandler>();
		dash=GetComponent<DashActionHandler>();
		grounded=GetComponent<ObjectGroundedTester>();
		player=GetComponent<AudioSource>();
		fly=GetComponent<FlyActionHandler>();
		PlayerDamagable.PlayerDamage+=PlayerDamagable_PlayerDamage;
	}
	private void OnDestroy() {
		PlayerDamagable.PlayerDamage-=PlayerDamagable_PlayerDamage;
	}

	private void PlayerDamagable_PlayerDamage() {
		PlayOneShot(soundDamage);
	}

	bool dashingPrevious;
	bool groundedPrevious;
	void FixedUpdate() {
		
		if(!dashingPrevious&&dash.dashing) PlayOneShot(soundDash);
		dashingPrevious=dash.dashing;
		Debug.Log(dash.bounceToken);
		if(dash.bounceToken==1){
			if(dash.lastBounceMirror) PlayOneShot(soundBounceMirror);
			else PlayOneShot(soundBounceWall);
		}

		if(!groundedPrevious&&grounded.grounded&&!dash.dashing) PlayOneShot(soundLand);
		groundedPrevious=grounded.grounded;

		if(grounded.grounded&&Mathf.Abs(move.relativeSpeed)>1) PlayRecur(soundWalk);
		if(!grounded&&fly.doFly) PlayRecur(soundFly);

	}

	void PlayOneShot(AudioClip clip){
		AudioManager.instance.PlayClip(clip,transform.position);
	}
	void PlayRecur(AudioClip clip){
		if(!player.isPlaying) player.PlayOneShot(clip);
	}

}