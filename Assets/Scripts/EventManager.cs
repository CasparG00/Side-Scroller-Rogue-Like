using System.Collections.Generic;

public enum EventType
{
    OnGenerated,
    OnPlayerDeath,
    OnPlayerDamaged
}

public delegate void EventCallback(EventType evt, object value);

public static class EventManager
{
    private static readonly Dictionary<EventType, EventCallback> eventRegister = new Dictionary<EventType, EventCallback>();

    public static void Subscribe(EventType evt, EventCallback func)
    {
        if (!eventRegister.ContainsKey(evt))
        {
            eventRegister.Add(evt, func);
        }
        else
        {
            eventRegister[evt] = func;
        }
    }

    public static void Unsubscribe(EventType evt, EventCallback func)
    {
        if (eventRegister.ContainsKey(evt))
        {
            eventRegister[evt] = null;
        }
    }

    public static void InvokeEvent(EventType evt, object value)
    {
        if (eventRegister.ContainsKey(evt))
        {
            eventRegister[evt]?.Invoke(evt, value);
        }
    }
}
