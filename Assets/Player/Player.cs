using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] Transform cameraArm = null;
	[SerializeField] PlayerControls controls = null;
	[SerializeField] PlayerWeapons weapons = null;
	[SerializeField] DamageTarget dmgTarget = null;
	public const float speed = 18f;
	public const float gravity = -30f;
	public const float jumpSpeed = 20f;
	public const float DashStaminaCost = 40f;
	Vector2 rot;
	float hp;
	float mana;
	float stamina;
	const float maxHP = 100f;
	const float maxMana = 100f;
	const float maxStamina = 100f;
	const float staminaRegen = 50f;
	const float mouseSensitivity = .5f;

	public float HPRatio{
		get{return hp / maxHP;}
	}
	public float ManaRatio{
		get{return mana / maxMana;}
	}
	public float StaminaRatio{
		get{return stamina / maxStamina;}
	}
	public float Stamina{
		get{return stamina;}
	}

	public PlayerControls Controls{
		get{return controls;}
	}

	/// <summary>
	/// Attempt to use up some stamina and return whether or not it was used.
	/// It won't use up any if there isn't enough.
	/// </summary>
	/// <param name="amount"></param>
	/// <returns></returns>
	public bool UseStamina(float amount){
		if(stamina >= amount){
			stamina -= amount;
			return true;
		}
		return false;
	}

	/// <summary>
	/// Attempts to use up some mana and return whether or not it was used.
	/// It won't use up any if there isn't enough.
	/// </summary>
	/// <param name="amount">The amount of mana to try to use.</param>
	/// <returns>true means that much mana was used up, so you should do the effects.</returns>
	public bool UseMana(float amount){
		if(mana >= amount){
			mana -= amount;
			return true;
		}
		return false;
	}
	public void GainMana(float amount){
		mana += amount;
		if(mana > maxMana) mana = maxMana;
	}

	public void Die(){
		Debug.Log("You died");
		GameManager.Restart();
	}

	void Start(){
		hp = maxHP;
		mana = maxMana;
		stamina = maxStamina;
		dmgTarget.OnHit += OnHit;
	}

	void Update() {
		look();
		regenStamina();
		equipWeapons();
	}

	void look(){
		rot += controls.Look * mouseSensitivity;
		rot.x = Mathf.Repeat(rot.x, 360f);
		rot.y = Mathf.Clamp(rot.y, -80f, 80f);
		transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
		cameraArm.localRotation = Quaternion.AngleAxis(-rot.y, Vector3.right);
	}

	void regenStamina(){
		stamina += staminaRegen * Time.deltaTime;
		if(stamina > maxStamina) stamina = maxStamina;
	}

	void equipWeapons(){
		int idx = controls.Equip;
		if(idx >= 0) weapons.Equip(idx);
	}

	void OnHit(Damage damage){
		hp -= damage.Amount;
		if(hp <= 0f) Die();
	}
}