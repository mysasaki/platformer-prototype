using System;

public class Event
{
    private event Action OnEventRaised;
    
    public void Subscribe(Action listener)
    {
        OnEventRaised += listener;
    }

    public void Unsubscribe(Action listener)
    {
        OnEventRaised -= listener;
    }

    public void Raise()
    {
        OnEventRaised?.Invoke();
    }
}

public class Event<T>
{
    private event Action<T> OnEventRaised;
    public void Subscribe(Action<T> listener)
    {
        OnEventRaised += listener;
    }

    public void Unsubscribe(Action<T> listener)
    {
        OnEventRaised -= listener;
    }

    public void Raise(T param)
    {
        OnEventRaised?.Invoke(param);
    }
}