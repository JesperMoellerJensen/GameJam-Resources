using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
	private Dictionary<string, Action<object>> actionEvents;
	private Dictionary<string, Func<object, object>> funcEvents;

	private Queue<KeyValuePair<string, object>> queueToProcess;
	private Queue<KeyValuePair<string, object>> activeQueue;

	private static EventManager instance;
	private static readonly float EVENT_LOOP_MAX_TIME = 0.200f;

	public static EventManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType(typeof(EventManager)) as EventManager;

				instance.Init();
			}

			return instance;
		}
	}

	private void Init()
	{
		if(actionEvents == null || queueToProcess == null || activeQueue == null)
		{
			actionEvents = new Dictionary<string, Action<object>>();

			queueToProcess = new Queue<KeyValuePair<string, object>>();
			activeQueue = new Queue<KeyValuePair<string, object>>();
		}
	}

	void Awake()
	{
		instance = this;
	}

	void Update()
	{
		if (instance == null)
		{
			return;
		}


		ProcessQueue();
	}

	public static void AddListener(string eventName, Action<object> eventDelegate)
	{
		Action<object> eventHandler = null;

		if (Instance.actionEvents.TryGetValue(eventName, out eventHandler))
		{
			eventHandler = Delegate.Combine(eventHandler, eventDelegate) as Action<object>;
		}
		else
		{
			Instance.actionEvents.Add(eventName, eventDelegate);
		}
	}

	public static void AddListener(string eventName, Func<object, object> eventDelegate)
	{
		Func<object, object> eventHandler = null;

		if (Instance.funcEvents.TryGetValue(eventName, out eventHandler))
		{
			eventHandler = Delegate.Combine(eventHandler, eventDelegate) as Func<object, object>;
		}
		else
		{
			Instance.funcEvents.Add(eventName, eventDelegate);
		}
	}

	public static void RemoveListener(string eventName, Action<object> eventDelegate)
	{
		if(instance == null)
		{
			return;
		}

		Action<object> eventHandler = null;

		if (instance.actionEvents.TryGetValue(eventName, out eventHandler))
		{
			eventHandler = Delegate.Combine(eventHandler, eventDelegate) as Action<object>;

			if (eventHandler == null)
			{
				instance.actionEvents.Remove(eventName);
			}
		}
	}

	public static void RemoveListener(string eventName, Func<object, object> eventDelegate)
	{
		if (instance == null)
		{
			return;
		}

		Func<object, object> eventHandler = null;

		if (instance.funcEvents.TryGetValue(eventName, out eventHandler))
		{
			eventHandler = Delegate.Combine(eventHandler, eventDelegate) as Func<object, object>;

			if (eventHandler == null)
			{
				instance.funcEvents.Remove(eventName);
			}
		}
	}

	public static void QueueEvent(string eventName, object eventArgs)
	{
		Instance.queueToProcess.Enqueue(new KeyValuePair<string, object>(eventName, eventArgs));
	}

	public static void TriggerActionEvent(string eventName, object eventArgs)
	{
		Action<object> eventHandler = null;
		if (Instance.actionEvents.TryGetValue(eventName, out eventHandler))
		{
			print($"Trigger action event: {eventName}");
			eventHandler.Invoke(eventArgs);
		}
	}

	public static object TriggerFuncEvent(string eventName, object eventArgs)
	{
		Func<object, object> eventHandler = null;
		if (Instance.funcEvents.TryGetValue(eventName, out eventHandler))
		{
			print($"Trigger func event: {eventName}");
			return eventHandler.Invoke(eventArgs);
		}

		return null;
	}

	private void ProcessQueue()
	{
		if (queueToProcess.Count != 0)
		{
			while (queueToProcess.Count != 0)
			{
				activeQueue.Enqueue(queueToProcess.Dequeue());
			}
		}

		if (activeQueue.Count != 0)
		{
			float loopTimer = Time.realtimeSinceStartup;
			float endTime = loopTimer + EVENT_LOOP_MAX_TIME;

			while (loopTimer < endTime && activeQueue.Count != 0)
			{
				KeyValuePair<string, object> eventArgs = activeQueue.Dequeue();

				TriggerActionEvent(eventArgs.Key, eventArgs.Value);

				loopTimer = Time.realtimeSinceStartup;
			}
		}
	}
}