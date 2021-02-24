using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] Transform cameraArm = null;
	[SerializeField] PlayerControls controls = null;
	[SerializeField] PlayerWeapons weapons = null;
	[SerializeField] DamageTarget dmgTarget = null;
	[SerializeField] PlayerHealSpell healSpell = null;
	
	Vector2 rot;
	NumberPool health = new NumberPool(100f);
	NumberPool mana = new NumberPool(100f);
	NumberPool stamina = new NumberPool(100f);

	public const float speed = 18f;
	public const float gravity = -30f;
	public const float jumpSpeed = 20f;
	public const float DashStaminaCost = 40f;
	const float mouseSensitivity = .5f;
	const float staminaRegen = 50f;

	public NumberPool Health{
		get{return health;}
	}
	public NumberPool Mana{
		get{return mana;}
	}
	public NumberPool Stamina{
		get{return stamina;}
	}

	public PlayerControls Controls{
		get{return controls;}
	}

	public void Die(){
		Debug.Log("You died");
		GameManager.Restart();
	}

	void Start(){
		dmgTarget.OnHit += OnHit;
	}

	void Update() {
		look();
		regenStamina();
		equipWeapons();
		heal();
	}

	void look(){
		rot += controls.Look * mouseSensitivity;
		rot.x = Mathf.Repeat(rot.x, 360f);
		rot.y = Mathf.Clamp(rot.y, -80f, 80f);
		transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
		cameraArm.localRotation = Quaternion.AngleAxis(-rot.y, Vector3.right);
	}

	void regenStamina(){
		stamina.Gain(staminaRegen * Time.deltaTime);
	}

	void equipWeapons(){
		int idx = controls.Equip;
		if(idx >= 0) weapons.Equip(idx);
	}

	void heal(){
		if(controls.Heal){
			healSpell.Cast();
		}
	}

	void OnHit(Damage damage){
		health.Lose(damage.Amount);
		if(health.Empty) Die();
	}
}