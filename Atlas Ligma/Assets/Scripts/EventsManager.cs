using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct EventItems
{
	public List<TurretTemplate> affectedWhiteTurrets;
	public List<TurretTemplate> affectedBlackTurrets;
	public int turnCount; //Turn Count before the Event should be Executed
	public GameObject chatText;

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

	[Header("Events Execution and End")]
	public List<string> eventLineUp;
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
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			for (int i = 0; i < selectedEventIds.Count; i++)
			{
				eventsComp[selectedEventIds[i]](selectedEventIds[i]);
			}
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
	/// White turrets that had investments are now more expensive, 150% of original cost (Effect),
	/// This is only activated on next wave (Duration).
	/// </summary>
	void Event0()
	{
		int maxInvested = 0;
		List<TurretTemplate> turretsToAdd = new List<TurretTemplate>();

		//Check Number of Fully Invested White Turrets
		for (int i = 0; i < allWhiteTurrets.Count; i++)
		{
			if (allWhiteTurrets[i].investmentLevel == 3)
			{
				maxInvested++;
				turretsToAdd.Add (allWhiteTurrets[i]);
			} 
		}

		//print (maxInvested);

		if (maxInvested >= 3)
		{
			//print ("condition met");
			if (eventItems[0].affectedWhiteTurrets == null) ExecuteEvent += Event0Execution; //This ensures that the event is only added once.
			eventItems[0].affectedWhiteTurrets = turretsToAdd;
		}
	}
	//================================================================================================================================================
	/// <summary>
	/// If invested into white for >= 3 rounds (Condition),
	/// Disable investments into Black Turrets (Effect),
	/// This is only activated on next wave (Duration).
	/// </summary>
	void Event1()
	{
		eventItems[1].eventExecuted = 1;
		ExecuteEvent += Event1Execution;
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
			eventItems[2].eventExecuted = 1; //Set Event 2 to "Active"
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
		eventItems[3].eventExecuted = 1;
		ExecuteEvent += Event3Execution;
	}
	//================================================================================================================================================
	//Event Conditions END
	//================================================================================================================================================

	
	//================================================================================================================================================
	//Event START
	//================================================================================================================================================
	void Event0Execution()
	{
		eventItems[0].eventExecuted = 1; //Set Event 0 to "Active" //Special Case as still need to add turrets that are level 3 or more even after reaching condition

		TurretValueSettings t = FindObjectOfType<TurretValueSettings> ();
		foreach (TurretTemplate whiteTurrets in eventItems[0].affectedWhiteTurrets)
		{
			for (int i = 0; i < whiteTurrets.turretValues.upgradeOrInvestCost.Length; i++)
			{
				//print (whiteTurrets.gameObject.name);
				//print (t.whiteCatapult1.upgradeOrInvestCost[i]);
				whiteTurrets.turretValues.upgradeOrInvestCost[i] = (int)(whiteTurrets.turretValues.upgradeOrInvestCost[i] * 1.5f);
				//print (t.whiteCatapult1.upgradeOrInvestCost[i]);
				//print ("--");
			}
		}

		eventLineUp.Add("Over Investments has caused the previously fully invested White Turrets to be more expensive on the next wave");

		string chat = "All Fully Invested White Turrets are now more Expensive for 1 Wave!";
		eventItems[0].chatText = ManaSystem.inst.gui.AddEventChat(chat);

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

		Destroy(eventItems[0].chatText);
		eventItems[0].chatText = null;

		eventItems[0].affectedWhiteTurrets = null;
		EventEnd -= Event0End;
		eventItems[0].eventExecuted = 0; //Set Event 0 to "Not Executed Yet"
	}
	//================================================================================================================================================
	void Event1Execution()
	{
		bool reset = false;

		foreach (TurretTemplate whiteTurrets in allWhiteTurrets)
		{
			if (whiteTurrets.investmentLevel > 0)
			{
				eventItems[1].turnCount++;
				reset = false;
				break;
			} 
			else reset = true;
		}

		if (reset) eventItems[1].turnCount = 0;

		if (eventItems[1].turnCount >= 3)
		{
			//Disable Investment Button
			eventItems[1].affectedBlackTurrets = allBlackTurrets;

			foreach (TurretTemplate blackTurrets in eventItems[1].affectedBlackTurrets)
			{
				blackTurrets.investOrUpgradeDisabled = true;
			}

			eventItems[1].eventExecuted = 2; //Set to differentiate that the event has started
			eventItems[1].turnCount = 0; //Reset Turn Count Upon Activation

			eventLineUp.Add("The King of Black is angry at your excessive investments in the White Faction. Investments in Black Turrets are now disabled for the next wave");

			string chat = "All Investments to Black Turrets are now Disabled for 1 Wave!";
			eventItems[1].chatText = ManaSystem.inst.gui.AddEventChat(chat);

			//Note: Event End Should Come First Before Execute Event
			EventEnd += Event1End;
		} 
		else eventItems[1].eventExecuted = 0; //Set Event 0 to "Not Executed Yet"
	}

	void Event1End()
	{
		foreach (TurretTemplate blackTurrets in eventItems[1].affectedBlackTurrets)
		{
			blackTurrets.investOrUpgradeDisabled = false;
		}

		Destroy(eventItems[1].chatText);
		eventItems[1].chatText = null;

		eventItems[1].affectedBlackTurrets = null;
		EventEnd -= Event1End;
		eventItems[1].eventExecuted = 0; //Set Event 0 to "Not Executed Yet"
	}
	//================================================================================================================================================
	void Event2Execution ()
	{
		foreach (TurretTemplate blackTurrets in eventItems[2].affectedBlackTurrets)
		{
			blackTurrets.investOrUpgradeDisabled = true;
			blackTurrets.enabled = false;
		}

		eventLineUp.Add("Over Investments in the Black Faction has caused the Black Turrets to overheat. Black Turrets will not be functioning for the next wave");

		string chat = "All Black Turrets are now Disabled due to Overheat for 1 Wave!";
		eventItems[2].chatText = ManaSystem.inst.gui.AddEventChat(chat);

		//Note: Event End Should Come First Before Execute Event
		if (eventItems[2].eventExecuted == 1) EventEnd += Event2End;
	}

	public void Event2End ()
	{
		foreach (TurretTemplate blackTurrets in eventItems[2].affectedBlackTurrets)
		{
			blackTurrets.investOrUpgradeDisabled = false;
			blackTurrets.enabled = true;
		}

		Destroy(eventItems[2].chatText);
		eventItems[2].chatText = null;

		eventItems[2].affectedBlackTurrets = null;
		EventEnd -= Event2End;
		eventItems[2].eventExecuted = 0; //Set Event 2 to "False"
	}
	//================================================================================================================================================
	void Event3Execution ()
	{
		if (eventItems[3].eventExecuted == 1)
		{
			bool reset = false;

			foreach (TurretTemplate blackTurrets in allBlackTurrets)
			{
				if (blackTurrets.investmentLevel > 0)
				{
					eventItems[3].turnCount++;
					reset = false;
					break;
				} else reset = true;
			}

			if (reset) eventItems[3].turnCount = 0;

			if (eventItems[3].turnCount >= 3)
			{
				eventItems[3].affectedBlackTurrets = allBlackTurrets;

				foreach (TurretTemplate blackTurrets in eventItems[3].affectedBlackTurrets)
				{
					blackTurrets.turretValues.fireRate *= 0.5f;
				}

				eventLineUp.Add("Over Investments has caused all the Black Turrets to have a 50% decrease in fire rate");

				string chat = "All Black Turrets now have Decreased Fire Rates for 2 Waves!";
				eventItems[3].chatText = ManaSystem.inst.gui.AddEventChat(chat);

				eventItems[3].eventExecuted = 2; //Set to differentiate that the event has started
				eventItems[3].turnCount = 0; //Reset Turn Count Upon Activation
			} 
			else
			{
				eventItems[3].eventExecuted = 0; //Set Event 0 to "Not Executed Yet"
				return;
			} 
		}
		else if (eventItems[3].eventExecuted >= 2) eventItems[3].eventExecuted++;//First Turn of Event Start

		//Note: Event End Should Come First Before Execute Event
		if (eventItems[3].eventExecuted >= 2) EventEnd += Event3End; //Added No Matter the Turn Count. It will only cease the Event if it checks that it is 2 in EventEnd
	}

	void Event3End ()
	{
		if (eventItems[3].eventExecuted != 4)
		{
			ExecuteEvent += Event3Execution; //Loop Back to Increase Turn Count. 2 is the 1st turn of Event
			EventEnd -= Event3End;
		} 
		else
		{
			foreach (TurretTemplate blackTurrets in eventItems[3].affectedBlackTurrets)
			{
				blackTurrets.turretValues.fireRate /= 0.5f;
			}

			Destroy(eventItems[3].chatText);
			eventItems[3].chatText = null;

			eventItems[3].affectedBlackTurrets = null;
			EventEnd -= Event3End;
			eventItems[3].turnCount = 0;
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