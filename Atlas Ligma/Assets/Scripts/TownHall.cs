using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour
{
	public static int currentLevel;

	void Start()
	{
		currentLevel = 1;
	}

	void LevelUp()
	{
		currentLevel++;
	}
}