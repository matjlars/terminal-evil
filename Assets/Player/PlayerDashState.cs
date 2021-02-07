using UnityEngine;

public class PlayerDashState : MonoBehaviour
{
	Player player;
	CharacterController character;
	StateMachine sm;
	const float speed = 70f;
	const float duration = .2f;
	float expireTime;
	Vector3 velocity;

	void Awake() {
		player = GetComponentInParent<Player>();
		character = GetComponentInParent<CharacterController>();
		sm = GetComponentInParent<StateMachine>();
	}

	void OnEnable(){
		expireTime = Time.time + duration;
		velocity = player.Controls.WorldMove * speed;
	}

	void FixedUpdate(){
		// move character
		character.Move(velocity * Time.deltaTime);

		// expire after a bit
		if(Time.time >= expireTime){
			if(character.isGrounded){
				sm.State = "Grounded";
			}else{
				sm.State = "Air";
			}
		}
	}
}