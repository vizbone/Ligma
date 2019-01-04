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

public class EventsManager : MonoBehaviour
{
	public bool[] events;
	public List<int> selectedEventIds;
	public Action<int>[] eventsComp;

	List<WhiteTurretStats> whiteTurrets;
	List<BlackTurretStats> blackTurrets;

	List<WhiteTurretStats> whiteTurretAffectedEvent0;

	public Action ExecuteEvent;
	Action ExecuteEventThis;

	public bool event0Executed;
	public bool event1Executed;
	public bool event2Executed;
	public bool event3Executed;

	bool event3AlreadyCheckedForTurns;
	int event3AlreadyAffectedNumberOfTurns;

	bool whiteInvestedAlreadyChecked;
	int whiteInvestedInARowCount;

	bool blackInvestedAlreadyChecked;
	int blackInvestedInARowCount;

	void Start ()
	{
		whiteTurrets = new List<WhiteTurretStats> ();
		blackTurrets = new List<BlackTurretStats> ();

		whiteInvestedAlreadyChecked = false;
		event3AlreadyAffectedNumberOfTurns = 0;

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
		ExecuteEventThis ();
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

			//Effect:		Disable investments into Black Turrets
			//Condition:	If invested into white for more than 5 rounds
			//Duration:		Activate only on next wave
			case 1:
				Event1 ();
				break;

			//Effect:		Black turrets will shut down due to overheat
			//Condition:	When there are 3 or more fully invested white turrets 
			//Duration:		Activate only on next wave
			case 2:
				Event2 ();
				break;

			//Effect:		Black turrets fire rates will decrease by 50%
			//Condition:	If invested into black for more than 3 rounds
			//Duration:		Activate only on next 2 waves
			case 3:
				Event3 ();
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
		turret.invested = tt.investmentLevel > 0 ? true : false;		
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

	void EndWaveWhite ()
	{
		WhiteTurretStats wts = new WhiteTurretStats ();
		for (int i = 0; i < whiteTurrets.Count; i++)
		{
			wts = whiteTurrets[i];
			wts.invested = false;
			whiteTurrets[i] = wts;
		}
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

	public void EndWave ()
	{
		whiteInvestedAlreadyChecked = false;
		if (event0Executed) ExecuteEvent += Event0End;
		if (event1Executed) ExecuteEvent += Event1End;
		ExecuteEvent = null;
		ExecuteEvent += EndWaveWhite;
	}

	void Event0Execution()
	{
		whiteTurretAffectedEvent0 = new List<WhiteTurretStats> ();
		for (int i = 0; i < whiteTurrets.Count; i++)
		{
			if (whiteTurrets[i].invested)
			{
				whiteTurretAffectedEvent0.Add (whiteTurrets[i]);
			}
		}

		List<TurretTemplate> tts = new List<TurretTemplate> ();
		for (int i = 0; i < whiteTurretAffectedEvent0.Count; i++)
		{
			tts[i] = whiteTurretAffectedEvent0[i].turret;
			for (int it = 0; it < tts[it].turretValues.upgradeOrInvestCost.Length; it++)
			{
				int tempInt = tts[i].turretValues.upgradeOrInvestCost[it];
				float tempFloat = 1.5f;
				int result = (int) (tempInt * tempFloat);
				tts[i].turretValues.upgradeOrInvestCost[it] = result;
			}
		}
		event0Executed = true;
	}

	void Event0End ()
	{
		TurretTemplate[] tts = new TurretTemplate[whiteTurretAffectedEvent0.Count];
		for (int i = 0; i < tts.Length; i++)
		{
			tts[i] = whiteTurretAffectedEvent0[i].turret;
			for (int it = 0; it < tts[it].turretValues.upgradeOrInvestCost.Length; it++)
			{
				int tempInt = tts[i].turretValues.upgradeOrInvestCost[it];
				float tempFloat = 1.5f;
				int result = (int) (tempInt / tempFloat);
				tts[i].turretValues.upgradeOrInvestCost[it] = result;
			}
		}
		event0Executed = false;
	}

	void Event2Execution ()
	{
		TurretTemplate[] tts = new TurretTemplate[blackTurrets.Count];
		for (int i = 0; i < tts.Length; i++)
		{
			tts[i].enabled = false;
		}
		event2Executed = true;
	}

	public void Event2End ()
	{
		TurretTemplate[] tts = new TurretTemplate[blackTurrets.Count];
		for (int i = 0; i < tts.Length; i++)
		{
			tts[i].enabled = true;
		}
		event2Executed = false;
	}

	void Event1Execution ()
	{
		event1Executed = true;
	}

	void Event1End ()
	{
		event1Executed = false;
	}

	void Event3Execution ()
	{
		if (!event3Executed)
		{
			TurretTemplate[] tts = new TurretTemplate[blackTurrets.Count];
			for (int i = 0; i < tts.Length; i++)
			{
				tts[i] = blackTurrets[i].turret;
				if (!blackTurrets[i].event3Changed)
				{
					tts[i].turretValues.fireRate *= 0.5f;
					BlackTurretStats bts = new BlackTurretStats ();
					bts = blackTurrets[i];
					bts.event3Changed = true;
					blackTurrets[i] = bts;
				}
			}
			event3Executed = true;
		}
	}

	public void Event3ExecutionMidWay ()
	{
		event3AlreadyAffectedNumberOfTurns++;
		if (event3AlreadyAffectedNumberOfTurns >= 2) ExecuteEvent += Event3End;
	}

	void Event3End ()
	{
		TurretTemplate[] tts = new TurretTemplate[blackTurrets.Count];
		for (int i = 0; i < tts.Length; i++)
		{
			tts[i] = blackTurrets[i].turret;
			if (blackTurrets[i].event3Changed)
			{
				tts[i].turretValues.fireRate *= 2;
				BlackTurretStats bts = new BlackTurretStats ();
				bts = blackTurrets[i];
				bts.event3Changed = false;
				blackTurrets[i] = bts;
			}
		}
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
			ExecuteEvent += Event0Execution;
		}
	}

	void Event1 ()
	{
		if (!whiteInvestedAlreadyChecked)
		{
			whiteInvestedAlreadyChecked = true;
			whiteInvestedInARowCount += 1;
		}

		if (whiteInvestedInARowCount >= 5) ExecuteEvent += Event1Execution;
	}

	void Event2 ()
	{
		int maxInvested = 0;
		for (int i = 0; i < blackTurrets.Count; i++)
		{
			if (blackTurrets[i].investmentLevel == 3)
				maxInvested++;
		}

		if (maxInvested >= 3)
		{
			ExecuteEventThis += Event3Execution;
		}
	}

	void Event3 ()
	{
		if (!whiteInvestedAlreadyChecked)
		{
			whiteInvestedAlreadyChecked = true;
			whiteInvestedInARowCount += 1;
		}

		if (whiteInvestedInARowCount >= 3) ExecuteEvent += Event3Execution;
	}
}