using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
	public bool[] events;
	public List<int> selectedEventIds;
	public Action<int>[] eventsComp;

	/*public delegate void EventsCompilation ();
	public EventsCompilation[] eventsComp;*/

	void Start ()
	{
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
			case 0:
				print("suck 0 balls");
				break;
			case 1:
				print("suck 1 balls");
				break;
			default:
				print("suck " + eventIndex + " balls");
				break;
		}
	}
}