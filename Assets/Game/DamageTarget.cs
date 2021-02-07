using System;
using UnityEngine;

public class DamageTarget : MonoBehaviour
{
	public Action<Damage> OnHit;

	public void Hit(Damage damage){
		if(OnHit == null){
			Destroy(gameObject);
		}else{
			OnHit(damage);
		}
	}
}