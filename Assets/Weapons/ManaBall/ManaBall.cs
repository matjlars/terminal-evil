using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBall : MonoBehaviour
{
	[SerializeField] GameObject ball = null;
	
	Player player;
	Camera cam;
	Vector3 ballVelocity;
	float nextCastTime;
	const float castDistance = 3f;
	const float ballSmoothTime = 5f;
	const float cooldown = .1f;
	const float damage = 20f;
	const float manaGain = 40f;
	bool canCast = true;
	Vector3 ballDestination;

	public bool IsCasting{
		get{return Time.time < nextCastTime;}
	}

	void Start(){
		player = GetComponentInParent<Player>();
		cam = player.Camera;
	}

	void Update(){
		// start cast
		if(player.Controls.Fire0 && Time.time >= nextCastTime && canCast){
			nextCastTime = Time.time + cooldown;
			canCast = false;
			ballDestination = cam.transform.position + (cam.transform.forward * castDistance);
		}

		// let go of the button to allow casting again
		if(!player.Controls.Fire0){
			canCast = true;
		}

		// move ball
		Vector3 dest = transform.position;
		if(IsCasting) dest = ballDestination;
		ball.transform.position = Vector3.SmoothDamp(ball.transform.position, dest, ref ballVelocity, ballSmoothTime * Time.deltaTime);
	}

	public void ReportHit(GameObject go){
		if(IsCasting){
			DamageTarget dmgTarget = go.GetComponent<DamageTarget>();
			if(dmgTarget){
				dmgTarget.Hit(new Damage(damage, player.gameObject));
				player.Mana.Gain(manaGain);
			}
		}
	}
}