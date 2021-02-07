using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
	void Start(){
		// These will be in every scene so why not
		GameManager.Load();
	}

	void OnDrawGizmos(){
		Vector3 center = transform.position + Vector3.up;
		Vector3 size = new Vector3(1f, 2f, 1f);
		Gizmos.color = new Color(0f, 255f, 0f, 0.3f);
		Gizmos.DrawCube(center, size);
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(center, size);
	}
}