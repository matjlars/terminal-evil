using System.Collections;
using UnityEngine;

public class PlayerHealSpell : MonoBehaviour
{
	[SerializeField] Player player = null;
	[SerializeField] new Light light = null;
	WaitForSeconds lightWait = new WaitForSeconds(.5f);

	const float manaCost = 20f;
	const float healAmount = 50f;

	void Start(){
		light.enabled = false;
	}

	public void Cast(){
		// don't do it if the player has full health:
		if(player.Health.Full) return;

		if(player.Mana.Use(manaCost)){
			player.Health.Gain(healAmount);
			StopAllCoroutines();
			StartCoroutine(handleLight());
		}
	}

	IEnumerator handleLight(){
		light.enabled = true;
		yield return lightWait;
		light.enabled = false;
	}
}