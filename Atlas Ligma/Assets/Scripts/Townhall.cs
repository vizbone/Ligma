using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Townhall : MonoBehaviour
{
	[Header("Crystals")]
	[SerializeField] Transform mainCrystal;
	[SerializeField] float mainCrystalBounceSpeed;
	[SerializeField] float mainCrystalMaxBounceHeight;
	[SerializeField] float mainCrystalRotateSpeed;
	[Space (15)]
	[SerializeField] Transform[] subCrystals;
	[Space(5)]
	[SerializeField] float[] subCrystalOriHeight;
	[Space(5)]
	[SerializeField] float[] subCrystalBounceSpeed;
	[Space(5)]
	[SerializeField] float[] subCrystalMaxBounceHeight;
	[Space(5)]
	[SerializeField] float[] subCrystalRotateSpeed;
	[Space(15)]

	[Header("Ring and Mananite Holder")]
	[SerializeField] Transform ring;
	[SerializeField] float ringRotateSpeed;
	[Space(15)]
	[SerializeField] Transform mananiteHolder;
	[SerializeField] float holderRotateSpeed;

	private void Start()
	{
		subCrystalOriHeight = new float[subCrystals.Length];

		for (int i = 0; i < subCrystals.Length; i++)
		{
			subCrystalOriHeight[i] = subCrystals[i].transform.localPosition.y;
		}
	}

	// Update is called once per frame
	void Update()
    {
		//Main Crystals
		Rotate(mainCrystal, mainCrystalRotateSpeed);
		BounceUpAndDown(mainCrystal, mainCrystalMaxBounceHeight, mainCrystalBounceSpeed);

		//Sub Crystals
		for (int i = 0; i < subCrystals.Length; i++)
		{
			Rotate(subCrystals[i], subCrystalRotateSpeed[i]);
			BounceUpAndDown(subCrystals[i], subCrystalMaxBounceHeight[i], subCrystalRotateSpeed[i], subCrystalOriHeight[i]);
		}

		//Rings and Mananite Holder
		Rotate(ring, ringRotateSpeed);
		Rotate(mananiteHolder, holderRotateSpeed);
    }

	void BounceUpAndDown(Transform obj, float maxHeight, float speed, float oriHeight = 0)
	{
		float y = MathFunctions.SmoothPingPong(Time.time / speed, maxHeight) + oriHeight;
		obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, y, obj.transform.localPosition.z);
	}

	void Rotate(Transform obj, float speed)
	{
		obj.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
	}
}
