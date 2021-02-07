using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
	[SerializeField] InputActionAsset controls = null;

	InputAction move;
	InputAction look;
	InputAction jump;
	InputAction dash;
	InputAction fire0;
	InputAction fire1;
	InputAction equip0;
	InputAction equip1;
	InputAction equip2;
	InputAction equip3;

	void OnEnable(){
		if(move == null){
			move = controls.FindAction("Move");
			look = controls.FindAction("Look");
			jump = controls.FindAction("Jump");
			dash = controls.FindAction("Dash");
			fire0 = controls.FindAction("Fire0");
			fire1 = controls.FindAction("Fire1");
			equip0 = controls.FindAction("Equip0");
			equip1 = controls.FindAction("Equip1");
			equip2 = controls.FindAction("Equip2");
			equip3 = controls.FindAction("Equip3");
		}

		move.Enable();
		look.Enable();
		jump.Enable();
		dash.Enable();
		fire0.Enable();
		fire1.Enable();
		equip0.Enable();
		equip1.Enable();
		equip2.Enable();
		equip3.Enable();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void OnDisable(){
		move.Disable();
		look.Disable();
		jump.Disable();
		dash.Disable();
		fire0.Disable();
		fire1.Disable();
		equip0.Disable();
		equip1.Disable();
		equip2.Disable();
		equip3.Disable();
		Cursor.lockState = CursorLockMode.None;
	}

	public Vector2 Move{
		get{return move.ReadValue<Vector2>();}
	}
	public Vector2 Look{
		get{return look.ReadValue<Vector2>();}
	}
	public bool Jump{
		get{return jump.ReadValue<float>() > 0.5f;}
	}
	public bool Dash{
		get{return dash.ReadValue<float>() > 0.5f;}
	}
	public bool Fire0{
		get{return fire0.ReadValue<float>() > 0.5f;}
	}
	public bool Fire1{
		get{return fire1.ReadValue<float>() > 0.5f;}
	}

	/// <summary>
	/// Returns an equip index if the player just pressed one.
	/// -1 means the player isn't trying to equip anything.
	/// </summary>
	public int Equip{
		get{
			if(equip0.ReadValue<float>() > 0.5f) return 0;
			if(equip1.ReadValue<float>() > 0.5f) return 1;
			if(equip2.ReadValue<float>() > 0.5f) return 2;
			if(equip3.ReadValue<float>() > 0.5f) return 3;
			return -1;
		}
	}

	public Vector3 WorldMove{
		get{
			Vector2 m = move.ReadValue<Vector2>();
			Vector3 forward = transform.forward * m.y;
			Vector3 right = transform.right * m.x;
			return (forward + right).normalized;
		}
	}
}