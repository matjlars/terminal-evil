using UnityEngine;

public class PlayerGroundedState : MonoBehaviour
{
	CharacterController character;
	StateMachine sm;
	Player player;

	void Start(){
		player = GetComponentInParent<Player>();
		character = GetComponentInParent<CharacterController>();
		sm = GetComponentInParent<StateMachine>();
	}

	void FixedUpdate(){
		Vector3 velocity = player.Controls.WorldMove * Player.speed;

		velocity.y = character.velocity.y + (Player.gravity * Time.deltaTime);
		
		if(character.isGrounded && player.Controls.Jump){
			velocity.y = Player.jumpSpeed;
		}

		character.Move(velocity * Time.deltaTime);

		if(!character.isGrounded) sm.State = "Air";
		if(player.Controls.Dash && player.Stamina.Use(Player.DashStaminaCost)) sm.State = "Dash";
	}
}