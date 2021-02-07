using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
	GameObject current;

	void Start(){
		// set the first one to active
		transform.GetChild(0).gameObject.SetActive(true);

		// set the rest to inactive
		for(int i = 1; i < transform.childCount; i++){
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	public void Equip(int weaponIndex){
		// can't do it if we don't have that weapon.
		if(weaponIndex >= transform.childCount){
			return;
		}

		// get rid of the last one
		if(current != null) current.SetActive(false);

		// find the new one and activate it
		Transform next = transform.GetChild(weaponIndex);
		current = next.gameObject;
		current.SetActive(true);
	}
}