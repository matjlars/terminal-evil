using UnityEngine;

public class PlayerAirState : MonoBehaviour
{
	Player player;
	CharacterController character;
	StateMachine sm;
	const float acceleration = 100f;

	void Start(){
		player = GetComponentInParent<Player>();
		character = GetComponentInParent<CharacterController>();
		sm = GetComponentInParent<StateMachine>();
	}

	void FixedUpdate(){
		Vector3 velocity = character.velocity;
		Vector3 desiredVelocity = player.Controls.WorldMove * Player.speed;

		float y = velocity.y;
		velocity = Vector3.MoveTowards(velocity, desiredVelocity, acceleration * Time.deltaTime);
		velocity.y = y + (Player.gravity * Time.deltaTime);

		character.Move(velocity * Time.deltaTime);

		if(character.isGrounded) sm.State = "Grounded";
		if(player.Controls.Dash && player.Stamina.Use(Player.DashStaminaCost)) sm.State = "Dash";
	}
}