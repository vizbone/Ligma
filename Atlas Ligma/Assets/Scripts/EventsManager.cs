using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WhiteTurretStats
{
	public TurretTemplate turret;
	public int investmentLevel;
	public bool invested;
}

public struct BlackTurretStats
{
	public TurretTemplate turret;
	public int investmentLevel;
	public bool event3Changed;
}

public struct EventItems
{
	public List<TurretTemplate> affectedWhiteTurrets;
	public List<TurretTemplate> affectedBlackTurrets;
	public int turnCount; //Turn Count before the Event should be Executed

	/// <summary>
	/// If -1, Event is Not Activated For Level
	/// If 0, Event has not met Conditions
	/// If 1, Event has Executed (First Turn of Being Activated)
	/// If 2 and subsequent Numbers, Event has occured over x turns
	/// </summary>
	public int eventExecuted; //Stores Int Value to check if an event is executed
}

public class EventsManager : MonoBehaviour
{
	[Header ("Events Library")]
	public bool[] events;
	public List<int> selectedEventIds;
	public Action<int>[] eventsComp; //Delegate that Runs the Checking of Conditions

	[Header ("Turret Information For Events")]
	[SerializeField] List<TurretTemplate> allWhiteTurrets;
	[SerializeField] List<TurretTemplate> allBlackTurrets;

	[Header ("Event Items")]
	public EventItems[] eventItems;

	[Header ("Events Execution and End")]
	//Both Actions are Called when the Wave has ended in the Wave System
	//Event End Should Come First Before Execute Event
	public Action ExecuteEvent; //Delegate that will only run for 1 frame at the end of a wave
	public Action EventEnd; //Delegate that will reverse the effects of events at end of a wave

	void Start ()
	{
		TurretTemplate[] allTurrets = FindObjectsOfType<TurretTemplate>();
		allWhiteTurrets = new List<TurretTemplate>();
		allBlackTurrets = new List<TurretTemplate>();

		for (int i = 0; i < allTurrets.Length; i++)
		{
			if (allTurrets[i].faction == Faction.white) allWhiteTurrets.Add(allTurrets[i]);
			else if (allTurrets[i].faction == Faction.black) allBlackTurrets.Add(allTurrets[i]);
		}

		eventsComp = new Action<int>[events.Length];
		selectedEventIds = new List<int>();
		eventItems = new EventItems[events.Length];

		for (int i = 0; i < events.Length; i++)
		{
			if (events[i])
			{
				eventsComp[i] = new Action<int>(ConditionList); //Run the Codition Function for Selected Events
				eventItems[i].eventExecuted = 0; //Event has not met conditions
				selectedEventIds.Add(i); //Add the Event ID
			}
			else eventItems[i].eventExecuted = -1; //Event not activated for the level
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
			case 0:
				if (eventItems[0].eventExecuted == 0) Event0 ();
				break;

			case 1:
				if (eventItems[1].eventExecuted == 0) Event1 ();
				break;

			case 2:
				if (eventItems[2].eventExecuted == 0) Event2 ();
				break;

			case 3:
				if (eventItems[3].eventExecuted == 0) Event3 ();
				break;

			default:
				print ("Invalid event index number: \"" + eventIndex + "\"; Check the script to see if it exists");
				break;
		}
	}

	//================================================================================================================================================
	//Event Conditions START
	//================================================================================================================================================
	/// <summary>
	/// When there are 3 or more fully invested white turrets (Condition),
	/// White turrets that had investments are now more expensive by 150% (Effect),
	/// This is only activated on next wave (Duration).
	/// </summary>
	void Event0()
	{
		int maxInvested = 0;
		List<TurretTemplate> turretsToAdd = new List<TurretTemplate>();

		//Check Number of Fully Invested White Turrets
		for (int i = 0; i < allWhiteTurrets.Count; i++)
		{
			if (allWhiteTurrets[i].investmentLevel == 3) maxInvested++;
			turretsToAdd.Add(allWhiteTurrets[i]);
		}

		if (maxInvested >= 3)
		{
			eventItems[0].affectedWhiteTurrets = turretsToAdd;
			ExecuteEvent += Event0Execution;
		}
	}
	//================================================================================================================================================
	/// <summary>
	/// If invested into white for >= 5 rounds (Condition),
	/// Disable investments into Black Turrets (Effect),
	/// This is only activated on next wave (Duration).
	/// </summary>
	void Event1()
	{
		bool activateEvent = false;

		foreach (TurretTemplate whiteTurrets in allWhiteTurrets)
		{
			if (whiteTurrets.investmentLevel >= 0) activateEvent = true;
			else
			{
				activateEvent = false;
				break;
			}
		}

		eventItems[1].turnCount = activateEvent ? ++eventItems[1].turnCount : 0;

		if (eventItems[1].turnCount >= 5) ExecuteEvent += Event1Execution;
	}
	//================================================================================================================================================
	/// <summary>
	/// When there are 3 or more fully invested black turrets (Condition),
	/// All Black turrets will shut down due to overheat (Effect),
	/// This is only activated on next wave (Duration).
	/// </summary>
	void Event2()
	{
		int maxInvested = 0;

		//Check Number of Fully Invested White Turrets
		for (int i = 0; i < allBlackTurrets.Count; i++)
		{
			if (allBlackTurrets[i].investmentLevel == 3) maxInvested++;
		}

		if (maxInvested >= 3)
		{
			eventItems[2].affectedBlackTurrets = allBlackTurrets;
			ExecuteEvent += Event2Execution;
		}
	}
	//================================================================================================================================================
	/// <summary>
	/// If invested into black for more than 3 rounds (Condition),
	/// Black turrets fire rates will decrease by 50% (Effect),
	/// Activate only on next 2 waves (Duration).
	/// </summary>
	void Event3()
	{
		bool activateEvent = false;

		foreach (TurretTemplate blackTurrets in allBlackTurrets)
		{
			if (blackTurrets.investmentLevel >= 0) activateEvent = true;
			else
			{
				activateEvent = false;
				break;
			}
		}

		eventItems[3].turnCount = activateEvent ? ++eventItems[3].turnCount : 0;

		if (eventItems[3].turnCount >= 3)
		{
			eventItems[3].affectedBlackTurrets = allBlackTurrets;
			ExecuteEvent += Event3Execution;
		}
	}
	//================================================================================================================================================
	//Event Conditions END
	//================================================================================================================================================

	
	//================================================================================================================================================
	//Event START
	//================================================================================================================================================
	void Event0Execution()
	{
		foreach (TurretTemplate whiteTurrets in eventItems[0].affectedWhiteTurrets)
		{
			for (int i = 0; i < whiteTurrets.turretValues.upgradeOrInvestCost.Length; i++)
			{
				whiteTurrets.turretValues.upgradeOrInvestCost[i] = (int)(whiteTurrets.turretValues.upgradeOrInvestCost[i] * 1.5f);
			}
		}
		eventItems[0].eventExecuted = 1; //Set Event 0 to "Active"

		//Note: Event End Should Come First Before Execute Event
		if (eventItems[0].eventExecuted == 1) EventEnd += Event0End;
	}

	void Event0End ()
	{
		foreach (TurretTemplate whiteTurrets in eventItems[0].affectedWhiteTurrets)
		{
			for (int i = 0; i < whiteTurrets.turretValues.upgradeOrInvestCost.Length; i++)
			{
				whiteTurrets.turretValues.upgradeOrInvestCost[i] = (int)(whiteTurrets.turretValues.upgradeOrInvestCost[i] / 1.5f);
			}
		}

		eventItems[0].affectedWhiteTurrets = null;
		eventItems[0].eventExecuted = 0; //Set Event 0 to "Not Executed Yet"
	}
	//================================================================================================================================================
	void Event1Execution()
	{
		//Disable Investment Button
		eventItems[1].affectedBlackTurrets = allBlackTurrets;

		foreach (TurretTemplate blackTurrets in eventItems[1].affectedBlackTurrets)
		{
			blackTurrets.investOrUpgradeDisabled = true;
		}

		eventItems[1].eventExecuted = 1; //Set Event 1 to "Active"

		//Note: Event End Should Come First Before Execute Event
		if (eventItems[1].eventExecuted == 1) EventEnd += Event1End;
	}

	void Event1End()
	{
		foreach (TurretTemplate blackTurrets in eventItems[1].affectedBlackTurrets)
		{
			blackTurrets.investOrUpgradeDisabled = false;
		}

		eventItems[1].affectedBlackTurrets = null;
		eventItems[1].eventExecuted = 0; //Set Event 0 to "Not Executed Yet"
	}
	//================================================================================================================================================
	void Event2Execution ()
	{
		foreach (TurretTemplate blackTurrets in eventItems[2].affectedBlackTurrets)
		{
			blackTurrets.enabled = false;
		}

		eventItems[2].eventExecuted = 1; //Set Event 2 to "Active"

		//Note: Event End Should Come First Before Execute Event
		if (eventItems[2].eventExecuted == 1) EventEnd += Event2End;
	}

	public void Event2End ()
	{
		foreach (TurretTemplate blackTurrets in eventItems[2].affectedBlackTurrets)
		{
			blackTurrets.enabled = true;
		}

		eventItems[2].affectedBlackTurrets = null;
		eventItems[2].eventExecuted = 0; //Set Event 2 to "False"
	}
	//================================================================================================================================================
	void Event3Execution ()
	{
		if (eventItems[3].eventExecuted == 0)
		{
			foreach (TurretTemplate blackTurrets in eventItems[3].affectedBlackTurrets)
			{
				blackTurrets.turretValues.fireRate *= 0.5f;
			}
			eventItems[0].eventExecuted = 1; //Set Event 0 to "Active"
		}
		else eventItems[0].eventExecuted++;

		//Note: Event End Should Come First Before Execute Event
		EventEnd += Event3End; //Added No Matter the Turn Count. It will only cease the Event if it checks that it is 2 in EventEnd
	}

	void Event3End ()
	{
		if (eventItems[3].eventExecuted != 2) ExecuteEvent += Event3Execution; //Loop Back to Increase Turn Count
		else
		{
			foreach (TurretTemplate blackTurrets in eventItems[3].affectedBlackTurrets)
			{
				blackTurrets.turretValues.fireRate /= 0.5f;
			}

			eventItems[3].affectedBlackTurrets = null;
			eventItems[3].eventExecuted = 0; //Set Event 2 to "False"
		}
	}
	//================================================================================================================================================
	//Event END
	//================================================================================================================================================


	/*public WhiteTurretStats SetWhiteTurretStats(WhiteTurretStats turret, TurretTemplate tt)
	{
		turret.turret = tt;
		turret.investmentLevel = tt.investmentLevel;
		turret.invested = tt.investmentLevel > 0 ? true : false;
		return turret;
	}

	public BlackTurretStats SetBlackTurretStats(BlackTurretStats turret, TurretTemplate tt)
	{
		turret.turret = tt;
		turret.investmentLevel = tt.investmentLevel;
		return turret;
	}

	public void CheckWhite(TurretTemplate tt)
	{
		for (int i = 0; i < whiteTurrets.Count; i++)
		{
			if (whiteTurrets[i].turret == tt)
			{
				WhiteTurretStats tempo = new WhiteTurretStats();
				tempo = SetWhiteTurretStats(tempo, tt);
				whiteTurrets[i] = tempo;
				return;
			}
		}
		WhiteTurretStats temp = new WhiteTurretStats();
		temp = SetWhiteTurretStats(temp, tt);
		whiteTurrets.Add(temp);
	}

	void EndWaveWhite()
	{
		WhiteTurretStats wts = new WhiteTurretStats();
		for (int i = 0; i < whiteTurrets.Count; i++)
		{
			wts = whiteTurrets[i];
			wts.invested = false;
			whiteTurrets[i] = wts;
		}
	}

	public void CheckBlack(TurretTemplate tt)
	{
		for (int i = 0; i < blackTurrets.Count; i++)
		{
			if (blackTurrets[i].turret == tt)
			{
				BlackTurretStats tempo = new BlackTurretStats();
				tempo = SetBlackTurretStats(tempo, tt);
				blackTurrets[i] = tempo;
				return;
			}
		}
		BlackTurretStats temp = new BlackTurretStats();
		temp = SetBlackTurretStats(temp, tt);
		blackTurrets.Add(temp);
	}

	public void EndWave()
	{
		whiteInvestedAlreadyChecked = false;
		if (event0Executed) ExecuteEvent += Event0End;
		if (event1Executed) ExecuteEvent += Event1End;
		ExecuteEvent = null;
		ExecuteEvent += EndWaveWhite;
	}*/
}