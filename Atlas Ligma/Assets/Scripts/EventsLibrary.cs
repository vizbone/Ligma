using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventsTemplate : MonoBehaviour
{
	public virtual void EventFunction()
	{
		return;
	}
}

public class Event1 : EventsTemplate
{
	public override void EventFunction()
	{
		print("Event 1");
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