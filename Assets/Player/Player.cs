using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] Transform cameraArm = null;
	[SerializeField] PlayerControls controls = null;
	public const float speed = 12f;
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

	void Start(){
		hp = maxHP;
		mana = maxMana;
		stamina = maxStamina;
	}

	void Update() {
		look();
		regenStamina();
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
}