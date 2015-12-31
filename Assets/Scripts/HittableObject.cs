using UnityEngine;
using System.Collections;

public class HittableObject : MonoBehaviour
{
	Vector3 origin;
	bool isDead;
	float timeToReset = 5;
	float timeToDie = 2;
	Vector3 farAway = new Vector3(0, -9999, 0);
	Rigidbody body;

	void Awake () {
		body = GetComponent<Rigidbody>();
	}

	void Start ()
	{
		isDead = false;
		origin = transform.localPosition;
	}

	void OnCollisionEnter(Collision other)
     {
     	if (!isDead) {
			Debug.Log(other.gameObject.tag);
	 		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Cop") {
				StartCoroutine(Die());
			}
		}
     }

	IEnumerator Die() {
		yield return new WaitForSeconds(timeToDie);
		transform.position = farAway;
		body.isKinematic = true;
		yield return new WaitForSeconds(timeToReset);
		transform.localPosition = origin;
		transform.rotation = Quaternion.Euler (Vector3.zero);
		body.isKinematic = false;
		isDead = false;
	}
}

