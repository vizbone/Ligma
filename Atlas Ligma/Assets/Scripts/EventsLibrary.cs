using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventsTemplate : MonoBehaviour
{
	public virtual void EventFunction()
	{
		return;
	}

	public virtual void ResetEventChanges()
	{
		return;
	}
}

public class Event1 : EventsTemplate
{
	/// <summary>
	/// Increased Cost of White Turrets Investment by 150% 
	/// If there are 2 or more fully invested turrets
	/// </summary>

	public override void EventFunction ()
	{
		
	}
}

public class Event2 : EventsTemplate
{
	public override void EventFunction()
	{
		print("Event 1");
	}
}

public class Event3 : EventsTemplate
{
	public override void EventFunction()
	{
		print("Event 1");
	}
}
public class Event4 : EventsTemplate
{
	public override void EventFunction()
	{
		print("Event 1");
	}
}