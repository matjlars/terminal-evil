using UnityEngine;

public class BlastingRod : MonoBehaviour
{
	[SerializeField] Transform firePoint = null;
	[SerializeField] Projectile blastPrefab = null;
	Player player;
	float nextBlastTime;
	const float blastCooldown = .5f;
	const int blastProjectileCount = 8;
	const float blastSpeedMin = 75f;
	const float blastSpeedMax = 85f;
	const float blastDamage = 120f / (float)blastProjectileCount;
	const float blastInnacuracy = .1f;

	void Start() {
		player = GetComponentInParent<Player>();
	}

	void Update(){
		if(player.Controls.Fire0){
			blast();
		}
	}

	void blast(){
		if(Time.time >= nextBlastTime){
			nextBlastTime = Time.time + blastCooldown;
			Damage damage = new Damage(blastDamage, gameObject);

			for(int i = 0; i < blastProjectileCount; i++){
				Projectile blast = GameObject.Instantiate<Projectile>(blastPrefab);
				blast.transform.position = firePoint.position;
				blast.transform.rotation = Random.rotation;
				Vector3 velocity = firePoint.forward;
				velocity += Random.insideUnitSphere * blastInnacuracy;
				velocity = velocity.normalized * Random.Range(blastSpeedMin, blastSpeedMax);
				blast.Shoot(velocity, damage);
			}
		}
	}

	void OnBlastHit(){
		player.GainMana(blastDamage);
	}
}