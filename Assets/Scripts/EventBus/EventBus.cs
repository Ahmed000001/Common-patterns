using System;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : Singleton<EventBus>
{
    private Dictionary<Type, object> eventDictionary ;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitAwake()
    {
        Instance.Init();
    }

    public override void Awake()
    {
        base.Awake();
    }

    private void Init()
    {
        if (Instance.eventDictionary == null)
        {
            Instance.eventDictionary = new Dictionary<Type, object>() ;
        }
    }

    public static void Subscribe<T>( Action<T> listener) where T : IEventType
    {
        object thisEvent;
        if (Instance.eventDictionary.TryGetValue(typeof(T), out thisEvent))
        {
            var outEvent = (Action<T>) thisEvent;
            outEvent += listener;
        }
        else
        {
            var newEvent=new Action<T>(listener);
            Instance.eventDictionary.Add(typeof(T), newEvent);
            Debug.Log("new  Event Added");

        }

       
    }

    public static void UnSubscribe<T>(Action<T> listener) where T : IEventType
    {
        object thisEvent ;
        if (Instance.eventDictionary.TryGetValue(typeof(T), out thisEvent))
        {
            var outEvent=(Action<T>) thisEvent;
            outEvent -= listener;
            Debug.Log("Subscriber Removed");

        }
    }

    public static void Fire<T>(T eventType) where T : IEventType
    {
        object thisEvent ;
        if (Instance.eventDictionary.TryGetValue(eventType.GetType(), out thisEvent))
        {
            var outEvent = (Action<T>) thisEvent;
            outEvent.Invoke(eventType);
            print(eventType.GetType()+ "is Fired");
        }
    }
}

public interface IEventType
{
    
}