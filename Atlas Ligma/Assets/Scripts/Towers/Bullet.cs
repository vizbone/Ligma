using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum BulletType {crossbow, rocket, catapult, cannon};

public class Bullet : MonoBehaviour
{
	public TurretTemplate turret;
	public Vector3 velocity; //Save the Speed the Bullet is supposed to move
	public float lifetime;
	float timeElpased;
	public int hitCount; //Only used by Cannon

	public float frequency1;
	public Vector3 target;
	public float amplitude;
	public float speed;
	public bool catapult;

	public Vector3 oriPos;
	public float currentStep;
	Rigidbody rb;

	public float currentY;

	[Header("Particle Effects")]
	[SerializeField] protected Particles damageFeedback;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		hitCount = 0;
		lifetime = 5;
		oriPos = transform.position;
		oriPos = new Vector3 (oriPos.x, oriPos.y - currentY, oriPos.z);

		if (catapult)
		{
			rb.useGravity = false;
			currentStep = 0;
			target = new Vector3(target.x, target.y, target.z);
		}
	}

	void Update ()
	{
		if (catapult) ArcTravel();
		else
		{
			rb.velocity = velocity;
			timeElpased += Time.deltaTime;

			if (timeElpased >= lifetime) Destroy(gameObject);
		}
	}

	void ArcTravel()
	{
		/*if (currentStep < 1) */MathFunctions.ParabolicCurve(target, amplitude, currentStep, transform, frequency1, oriPos, currentY, oriPos.y);
		currentStep += speed * Time.deltaTime;
		//if (currentStep >= 1) turret.Hit(null, turret.isPrebuilt, gameObject, hitCount);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "AI")
		{
			if (turret != null)
			{
				turret.Hit(other.GetComponentInParent<AITemplate>(), turret.isPrebuilt, gameObject, hitCount);

				if (turret.GetType() == typeof(Catapult)) return;

				Vector3 dir = velocity.normalized;
				
				//For First Quadrant
				float designatedAngle = Mathf.Atan(dir.x / dir.z) * Mathf.Rad2Deg;
				if (dir.x > 0)
				{
					//Second Quad
					if (dir.z < 0) designatedAngle = 180 - Mathf.Abs(designatedAngle);
				}
				else
				{
					//Third Quad
					if (dir.z < 0) designatedAngle += 180;
					//Fourth Quad
					else designatedAngle = 360 - Mathf.Abs(designatedAngle);
				}

				//print(designatedAngle);

				Instantiate(damageFeedback, other.transform.position, Quaternion.Euler(0, designatedAngle, 0));
			}
		} 
		else
		{
			if (catapult) turret.Hit (null, turret.isPrebuilt, gameObject, hitCount);
		}
	}
}