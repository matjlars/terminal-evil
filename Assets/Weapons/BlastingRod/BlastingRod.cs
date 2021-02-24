using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class BlastingRod : MonoBehaviour
{
	[SerializeField] Transform firePoint = null;
	[SerializeField] Projectile blastPrefab = null;
	[SerializeField] Projectile fireballPrefab = null;
	Player player;
	float nextBlastTime;
	const float blastCooldown = .5f;
	const int blastProjectileCount = 8;
	const float blastSpeedMin = 75f;
	const float blastSpeedMax = 85f;
	const float blastDamage = 120f / (float)blastProjectileCount;
	const float blastInnacuracy = .1f;

	Projectile fireball = null;
	Collider fireballCollider;
	HDAdditionalLightData fireballLight;
	const float fireballSpeed = 60f;
	const float fireballDamage = 40f;
	const float fireballGrowDuration = 2f;
	const float fireballLightIntensity = 10000f;
	const float fireballLightRange = 10f;
	float fireballSpawnTime;

	void Start() {
		player = GetComponentInParent<Player>();
	}

	void Update(){
		if(player.Controls.Fire0){
			blast();
		}

		if(player.Controls.Fire1){
			// spawn a fireball
			if(fireball == null){
				fireball = GameObject.Instantiate<Projectile>(fireballPrefab);
				fireballSpawnTime = Time.time;
				fireballLight = fireball.GetComponent<HDAdditionalLightData>();
				fireballCollider = fireball.GetComponent<Collider>();
				fireballCollider.enabled = false;
			}

			fireball.transform.position = firePoint.position;

			float fireballFullyGrownTime = fireballSpawnTime + fireballGrowDuration;
			float t = Mathf.InverseLerp(fireballSpawnTime, fireballFullyGrownTime, Time.time);
			float scale = Mathf.Lerp(0f, 1f, t);
			fireballLight.intensity = Mathf.Lerp(0f, fireballLightIntensity, t);
			fireballLight.range = Mathf.Lerp(5f, 15f, t);
			fireball.transform.localScale = new Vector3(scale, scale, scale);
		}else{
			if(fireball != null){
				fireball.transform.position = firePoint.position;
				fireball.Shoot(firePoint.forward * fireballSpeed, new Damage(fireballDamage, player.gameObject));
				fireball = null;
				fireballCollider.enabled = true;
			}
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
				blast.OnHit += OnBlastHit;
				Vector3 velocity = firePoint.forward;
				velocity += Random.insideUnitSphere * blastInnacuracy;
				velocity = velocity.normalized * Random.Range(blastSpeedMin, blastSpeedMax);
				blast.Shoot(velocity, damage);
			}
		}
	}

	void OnBlastHit(){
		player.Mana.Gain(blastDamage);
	}
}