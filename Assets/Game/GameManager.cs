using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	static GameManager instance;

	/// <summary>
	/// If the Singletons scene is not loaded, load it.
	/// </summary>
	public static void Load(){
		Scene singletonsScene = SceneManager.GetSceneByBuildIndex(1);
		if(singletonsScene.isLoaded) return;
		SceneManager.LoadScene(1, LoadSceneMode.Additive);
	}

	void Awake(){
		#if UNITY_EDITOR
		if(instance != null) Debug.LogError("two GameManagers!");
		#endif

		instance = this;
	}

	void Start(){
		PlayerManager.Spawn();
	}

	public static void Restart(){
		Scene currentScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(currentScene.buildIndex);
	}
}