using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	[System.Serializable]
	public class Action {
		public Color color;
		public Sprite sprite;
		public string title;
	}

	public Action[] options;
	public GameObject currentTower;
	BoxCollider[] sensor;

	private void Start()
	{
		currentTower = gameObject;
		sensor = GetComponents<BoxCollider>();
	}

	void OnMouseDown()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (sensor[0].Raycast(ray, out hit, 100.0f)) RadialMenuSpawner.ins.SpawnMenu(this);
	}
}