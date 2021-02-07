using UnityEngine;

public class PlayerAirState : MonoBehaviour
{
	Player player;
	CharacterController character;
	StateMachine sm;

	void Start(){
		player = GetComponentInParent<Player>();
		character = GetComponentInParent<CharacterController>();
		sm = GetComponentInParent<StateMachine>();
	}

	void FixedUpdate(){
		Vector3 velocity = character.velocity;
		velocity.y += Player.gravity * Time.deltaTime;
		character.Move(velocity * Time.deltaTime);

		if(character.isGrounded) sm.State = "Grounded";
		if(player.Controls.Dash && player.UseStamina(Player.DashStaminaCost)) sm.State = "Dash";
	}
}