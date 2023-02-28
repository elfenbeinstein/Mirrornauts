using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event System in the game
/// Event IDs: TURN, ENERGY, AUDIO, etc.
/// </summary>

public class EventManager
{
    private static EventManager instance;

    public static EventManager Instance
    {
        get
        {
            if (instance == null) instance = new EventManager();
            return instance;
        }
    }

    public delegate void EventListener(string eventName, object param = null);
    private Dictionary<string, List<EventListener>> eventListener;

    EventManager()
    {
        eventListener = new Dictionary<string, List<EventListener>>();
    }

    public void AddEventListener(string eventID, EventListener listener)
    {
        if (!eventListener.ContainsKey(eventID)) eventListener.Add(eventID, new List<EventListener>());
        eventListener[eventID].Add(listener);
    }

    public void RemoveEventListener(string eventID, EventListener listener)
    {
        if (eventListener.ContainsKey(eventID))
            eventListener[eventID].Remove(listener);
        // add debug warnings if tried to remove a listener that isn't there
    }

    public void EventGo(string eventID, string eventName, object param = null)
    {
        if (eventListener.ContainsKey(eventID))
            for (int i = eventListener[eventID].Count - 1; i >= 0; i--)
                eventListener[eventID][i](eventName, param);
    }
}
