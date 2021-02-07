using UnityEngine;

/// <summary>
/// A simple state machine that uses Child GameObjects as each state.
/// The name of the state is the name of the GameObject.
/// Use the State setter to switch states.
/// </summary>
public class StateMachine : MonoBehaviour
{
	GameObject state;

	/// <summary>
	/// Returns true if the given state exists
	/// </summary>
	/// <param name="name">The name of the state that you are wondering if it exists.</param>
	/// <returns>true if it exists. false otherwise.</returns>
	public bool HasState(string name){
		Transform t = transform.Find(name);
		return t != null;
	}

	/// <summary>
	/// Returns the name of the current state, or null if none.
	/// Set this to change the current state. Pass the name of the child GameObject you want to be the current state.
	/// </summary>
	public string State{
		get{
			if(state == null) return null;
			return state.name;
		}

		set{
			if(state != null) state.SetActive(false);

			Transform newStateTransform = transform.Find(value);
			if(newStateTransform != null){
				state = newStateTransform.gameObject;
				state.SetActive(true);
			}else{
				state = null;
				#if UNITY_EDITOR
				Debug.LogError("StateMachine "+name+" does not have child GameObject called "+value);
				#endif
			}
		}
	}

	/// <summary>
	/// Returns the current State GameObject or null if there isn't one.
	/// </summary>
	public GameObject StateObject{
		get{return state;}
	}

	/// <summary>
	/// Attempts to find the current state by looking at which child GameObject is active.
	/// Will deactivate all subsequent GameObjects that are active.
	/// This is called on Start.
	/// </summary>
	public void Refresh(){
		state = null;
		// try to find an active one as the current state.
		// if you find one, deactivate the rest:
		for(int i = 0; i < transform.childCount; i++){
			Transform child = transform.GetChild(i);
			if(child.gameObject.activeSelf){
				if(state == null){
					// remember this active GameObject as the current state
					state = child.gameObject;
				}else{
					// we already found a state that should be the current state.
					// so deactivate this one, so there is only one.
					child.gameObject.SetActive(false);
				}
			}
		}
	}

	void Start(){
		Refresh();
	}
}