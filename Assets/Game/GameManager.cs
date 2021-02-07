using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	/// <summary>
	/// If the Singletons scene is not loaded, load it.
	/// </summary>
	public static void Load(){
		Scene singletonsScene = SceneManager.GetSceneByBuildIndex(1);
		if(singletonsScene.isLoaded) return;
		SceneManager.LoadScene(1, LoadSceneMode.Additive);
	}

	void Start(){
		PlayerManager.Spawn();
	}
}