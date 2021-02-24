using UnityEngine;
using UnityEngine.AI;

public class Lost : MonoBehaviour
{
	[SerializeField] NavMeshAgent nav = null;
	[SerializeField] DamageTarget dmgTarget = null;
	Transform target;
	DamageTarget targetDmgTarget;
	const float biteCooldown = .5f;
	const float biteDamage = 18f;
	const float biteRange = 1f;
	float nextBiteTime = 0f;
	NumberPool health = new NumberPool(100f);

	void Start(){
		dmgTarget.OnHit += OnHit;
	}

	void Update(){
		// try to find player
		if(target == null){
			var player = FindObjectOfType<Player>();
			if(player){
				target = player.transform;
				targetDmgTarget = player.GetComponent<DamageTarget>();
			}
		}

		// follow player
		if(target != null){
			nav.destination = target.position;
		}

		// try to bite target:
		if(targetDmgTarget != null){
			if(Vector3.Distance(transform.position, targetDmgTarget.transform.position) <= biteRange){
				if(Time.time >= nextBiteTime){
					targetDmgTarget.Hit(new Damage(biteDamage, gameObject));
					nextBiteTime = Time.time + biteCooldown;
				}
			}
		}
	}

	void OnHit(Damage damage){
		health.Lose(damage.Amount);
		if(health.Empty) Destroy(gameObject);
	}
}