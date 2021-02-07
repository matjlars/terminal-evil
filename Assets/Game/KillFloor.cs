using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
	Player player;

	void Update(){
		// try to find player
		if(player == null) player = GameObject.FindObjectOfType<Player>();

		// if there's a player, kill them if they're too low.
		if(player != null && player.transform.position.y < transform.position.y){
			player.Die();
		}
	}

	void OnDrawGizmos() {
		Vector3 center = transform.position;
		center.y -= 1f;
		Vector3 size = new Vector3(1000f, 2f, 1000f);
		Gizmos.color = new Color(1f, 0f, 0f, .2f);
		Gizmos.DrawCube(center, size);
	}
}