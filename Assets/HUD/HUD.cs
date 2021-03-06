using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	Player player;
	[SerializeField] Image health = null;
	[SerializeField] Image mana = null;
	[SerializeField] Image stamina = null;

	public void Init(Player player){
		this.player = player;
		gameObject.SetActive(true);
	}

	void Update() {
		if(player == null){
			gameObject.SetActive(false);
			return;
		}

		health.fillAmount = player.Health.Ratio;
		mana.fillAmount = player.Mana.Ratio;
		stamina.fillAmount = player.Stamina.Ratio;
	}
}