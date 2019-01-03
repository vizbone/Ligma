using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WhiteTurretStats
{
	public TurretTemplate turret;
	public int investmentLevel;
}

public struct BlackTurretStats
{
	public TurretTemplate turret;
	public int investmentLevel;
}

public class EventsManager : MonoBehaviour
{
	public bool[] events;
	public List<int> selectedEventIds;
	public Action<int>[] eventsComp;

	List<WhiteTurretStats> whiteTurrets;
	List<BlackTurretStats> blackTurrets;

	public Action ExecuteEvent;

	void Start ()
	{
		whiteTurrets = new List<WhiteTurretStats> ();
		blackTurrets = new List<BlackTurretStats> ();

		eventsComp = new Action<int>[events.Length];
		selectedEventIds = new List<int>();

		for (int i = 0; i < events.Length; i++)
		{
			if (events[i])
			{
				eventsComp[i] = new Action<int>(ConditionList);
				selectedEventIds.Add(i);
			}
		}
	}

	void Update ()
	{
		for (int i = 0; i < selectedEventIds.Count; i++)
		{
			eventsComp[selectedEventIds[i]](selectedEventIds[i]);
		}
	}

	public void ConditionList (int eventIndex)
	{
		switch (eventIndex)
		{
			//Effect:		White turrets that had investments are now more expensive by 150% 
			//Condition:	When there are 3 or more fully invested white turrets 
			//Duration:		Activate only on next wave
			case 0:
				Event0 ();
				break;

			//Effect:		White turrets that had investments are now more expensive by 150% 
			//Condition:	When there are 3 or more fully invested white turrets 
			//Duration:		Activate only on next wave
			case 1:
				Event1 ();
				break;

			//Effect:		White turrets that had investments are now more expensive by 150% 
			//Condition:	When there are 3 or more fully invested white turrets 
			//Duration:		Activate only on next wave
			case 2:
				Event2 ();
				break;

			//Effect:		White turrets that had investments are now more expensive by 150% 
			//Condition:	When there are 3 or more fully invested white turrets 
			//Duration:		Activate only on next wave
			case 3:
				Event3 ();
				break;

			//Effect:		White turrets that had investments are now more expensive by 150% 
			//Condition:	When there are 3 or more fully invested white turrets 
			//Duration:		Activate only on next wave
			case 4:
				break;

			default:
				print ("Invalid event index number: \"" + eventIndex + "\"; Check the script to see if it exists");
				break;
		}

	}

	public WhiteTurretStats SetWhiteTurretStats (WhiteTurretStats turret, TurretTemplate tt)
	{
		turret.turret = tt;
		turret.investmentLevel = tt.investmentLevel;
		return turret;
	}

	public BlackTurretStats SetBlackTurretStats (BlackTurretStats turret, TurretTemplate tt)
	{
		turret.turret = tt;
		turret.investmentLevel = tt.investmentLevel;
		return turret;
	}

	public void CheckWhite (TurretTemplate tt)
	{
		for (int i = 0; i < whiteTurrets.Count; i++)
		{
			if (whiteTurrets[i].turret == tt)
			{
				WhiteTurretStats tempo = new WhiteTurretStats ();
				tempo = SetWhiteTurretStats (tempo, tt);
				whiteTurrets[i] = tempo;
				return;
			}
		}
		WhiteTurretStats temp = new WhiteTurretStats ();
		temp = SetWhiteTurretStats (temp, tt);
		whiteTurrets.Add (temp);
	}

	public void CheckBlack (TurretTemplate tt)
	{
		for (int i = 0; i < blackTurrets.Count; i++)
		{
			if (blackTurrets[i].turret == tt)
			{
				BlackTurretStats tempo = new BlackTurretStats ();
				tempo = SetBlackTurretStats (tempo, tt);
				blackTurrets[i] = tempo;
				return;
			}
		}
		BlackTurretStats temp = new BlackTurretStats ();
		temp = SetBlackTurretStats (temp, tt);
		blackTurrets.Add (temp);
	}

	void Event0 ()
	{
		int maxInvested = 0;
		for (int i = 0; i < whiteTurrets.Count; i++)
		{
			if (whiteTurrets[i].investmentLevel == 3) maxInvested++;
		}

		if (maxInvested >= 3)
		{
			//event
			ExecuteEvent += Event0Execution;
		}
	}

	void Event0Execution()
	{
		//Things to happen for Event 0
	}

	void Event1 ()
	{

	}

	void Event2 ()
	{

	}

	void Event3 ()
	{

	}
}