using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public Action OnHit;
	[SerializeField] float lifespan = 1f;
	[SerializeField] Rigidbody rb = null;
	Vector3 velocity;
	Damage damage;

	public void Shoot(Vector3 velocity, Damage damage){
		this.velocity = velocity;
		this.damage = damage;
		rb.velocity = velocity;
		StartCoroutine(timeout());
	}

	void FixedUpdate(){
		if(rb.isKinematic){
			rb.MovePosition(transform.position + (velocity * Time.deltaTime));
		}
	}

	void OnTriggerEnter(Collider other){
		TryHit(other.gameObject);
	}
	void OnCollisionEnter(Collision collision) {
		TryHit(collision.gameObject);
	}

	void TryHit(GameObject other){
		DamageTarget dmgTarget = other.GetComponent<DamageTarget>();
		if(dmgTarget) dmgTarget.Hit(damage);

		if(OnHit != null) OnHit();

		Destroy(gameObject);
	}

	IEnumerator timeout(){
		yield return new WaitForSeconds(lifespan);
		Destroy(gameObject);
	}
}