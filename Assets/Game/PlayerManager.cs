using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	[SerializeField] Player prefab = null;
	[SerializeField] HUD hud = null;

	Player current;

	static PlayerManager instance;
	void Awake(){
		instance = this;
	}

	public static Player Current{
		get{return instance.current;}
	}

	public static void Spawn(){
		PlayerSpawn spawn = FindObjectOfType<PlayerSpawn>();
		if(spawn == null){
			Debug.LogError("Unable to spawn because there is no PlayerSpawn object.");
			return;
		}

		if(instance.current != null){
			GameObject.Destroy(instance.current.gameObject);
		}

		instance.current = GameObject.Instantiate<Player>(instance.prefab, spawn.transform.position, spawn.transform.rotation);
		instance.hud.Init(instance.current);
	}
}