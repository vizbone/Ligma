using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
	public bool[] events;
	public delegate void EventsCompilation ();
	public EventsCompilation[] eventsComp;

	/*void Start ()
	{
		Startup ();
	}

	void Startup ()
	{
		Type thisType = this.GetType ();
		for (int i = 0; i < events.Length; i++)
		{
			if (events[i])
			{
				MethodInfo method = thisType.GetMethod ("Condition" + i);
				print (method);
				eventsComp[i] = (EventsCompilation) Delegate.CreateDelegate (typeof (EventsCompilation), method);
			}
		}
	}*/

	void Update ()
	{
		Run ();
	}

	void Run ()
	{
		if (events[0]) Condition0 ();
		if (events[1]) Condition1 ();
	}

	public void Condition0 ()
	{
		print ("suck 0 balls");
	}
	public void Condition1 ()
	{
		print ("suck 1 balls");
	}
}