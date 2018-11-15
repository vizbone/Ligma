using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class TowerBuilder : ScriptableObject
{
	public GameObject buildModel;
	public GameObject actualTower;
	public int cost;
}