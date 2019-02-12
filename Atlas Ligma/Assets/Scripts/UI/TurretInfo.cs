using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurretInfo : MonoBehaviour
{
	public Text turretDes; //Stores the Turret Description
	public Text[] turretStats; //0 is Turret Name and Level, 1 is Faction, 2 is Damage, 3 is Fire Rate, 4 is Attack Type

	public void UpdateTurretInfo(TurretTemplate turret)
	{
		if (turret.turretType == typeof(Cannon))
		{
			turretDes.text = "A powerful turret that shoots piercing bullets!";
			turretStats[0].text = "Cannon Lvl " + turret.level;
			turretStats[4].text = "Target Enemies: Ground, Sea";
		}
		else if (turret.turretType == typeof(Catapult))
		{
			turretDes.text = "A turret that shoots explosives at enemies, dealing AOE damage.";
			turretStats[0].text = "Catapult Lvl " + turret.level;
			turretStats[4].text = "Target Enemies: Ground, Sea";
		}
		else if (turret.turretType == typeof(Crossbow))
		{
			turretDes.text = "A turret that can attack at rapid speeds but deal lower damage.";
			turretStats[0].text = "Crossbow Lvl " + turret.level;
			turretStats[4].text = "Target Enemies: Air, Ground, Sea";
		}
		else if (turret.turretType == typeof(Rockets))
		{
			turretDes.text = "A turret that exclusively attacks air enemies but at a higher efficiency.";
			turretStats[0].text = "Rocket Lvl " + turret.level;
			turretStats[4].text = "Target Enemies: Air";
		}
		else print("Type Error");

		switch (turret.faction)
		{
			case Faction.black:
				turretStats[1].color = Color.red;
				turretStats[1].text = "Red";
				break;
			case Faction.white:
				turretStats[1].color = Color.blue;
				turretStats[1].text = "Blue";
				break;
			case Faction.own:
				turretStats[1].color = Color.gray;
				turretStats[1].text = "Own";
				break;
		}

		turretStats[2].text = "Dmg: " + turret.turretValues.dmg;
		turretStats[3].text = "Fire Rate: " + turret.turretValues.fireRate;
	}

	public void UpdateTurretInfo(string turret)
	{
		if (turret == "Cannon")
		{
			turretDes.text = "A powerful turret that shoots piercing bullets!";
			turretStats[0].text = "Cannon Lvl 1";
			turretStats[2].text = "Dmg: " + TurretValueSettings.cannon1s.dmg;
			turretStats[3].text = "Fire Rate: " + TurretValueSettings.cannon1s.fireRate;
			turretStats[4].text = "Target Enemies: Ground, Sea";
		}
		else if (turret == "Catapult")
		{
			turretDes.text = "A turret that shoots explosives at enemies, dealing AOE damage.";
			turretStats[0].text = "Catapult Lvl 1";
			turretStats[2].text = "Dmg: " + TurretValueSettings.catapult1s.dmg;
			turretStats[3].text = "Fire Rate: " + TurretValueSettings.catapult1s.fireRate;
			turretStats[4].text = "Target Enemies: Ground, Sea";
		}
		else if (turret == "Crossbow")
		{
			turretDes.text = "A turret that can attack at rapid speeds but deal lower damage.";
			turretStats[0].text = "Crossbow Lvl 1";
			turretStats[2].text = "Dmg: " + TurretValueSettings.crossbow1s.dmg;
			turretStats[3].text = "Fire Rate: " + TurretValueSettings.crossbow1s.fireRate;
			turretStats[4].text = "Target Enemies: Air, Ground, Sea";
		}
		else if (turret == "Rocket")
		{
			turretDes.text = "A turret that exclusively attacks air enemies but at a higher efficiency.";
			turretStats[0].text = "Rocket Lvl 1";
			turretStats[2].text = "Dmg: " + TurretValueSettings.rocket1s.dmg;
			turretStats[3].text = "Fire Rate: " + TurretValueSettings.rocket1s.fireRate;
			turretStats[4].text = "Target Enemies: Air";
		}

		turretStats[1].color = Color.gray;
		turretStats[1].text = "Own";
	}
}