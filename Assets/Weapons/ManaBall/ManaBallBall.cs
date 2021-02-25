using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBallBall : MonoBehaviour
{
	ManaBall manaBall;

	void Start() {
		manaBall = GetComponentInParent<ManaBall>();
	}

	void OnTriggerEnter(Collider other){
		manaBall.ReportHit(other.gameObject);
	}
}