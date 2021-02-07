using UnityEngine;

public struct Damage
{
	float amount;
	GameObject instigator;

	public float Amount{
		get{return amount;}
	}
	public GameObject Instigator{
		get{return instigator;}
	}

	public Damage(float amount, GameObject instigator){
		this.amount = amount;
		this.instigator = instigator;
	}
}