using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
	[SerializeField] InputActionAsset controls = null;

	InputAction move;
	InputAction look;
	InputAction jump;
	InputAction dash;

	void OnEnable(){
		if(move == null){
			move = controls.FindAction("Move");
			look = controls.FindAction("Look");
			jump = controls.FindAction("Jump");
			dash = controls.FindAction("Dash");
		}

		move.Enable();
		look.Enable();
		jump.Enable();
		dash.Enable();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void OnDisable(){
		move.Disable();
		look.Disable();
		jump.Disable();
		dash.Disable();
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

	public Vector3 WorldMove{
		get{
			Vector2 m = move.ReadValue<Vector2>();
			Vector3 forward = transform.forward * m.y;
			Vector3 right = transform.right * m.x;
			return (forward + right).normalized;
		}
	}
}