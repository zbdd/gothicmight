using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour, IObservable<EventMessage>
{
    public static WorldState World;
    private static ArrayList CompletedQuests = new ArrayList();
    private static ArrayList FailedQuests = new ArrayList();
    private static ArrayList CharactersMet = new ArrayList();
    private static ArrayList AreasVisited = new ArrayList();
    private static ArrayList PlayerJournal = new ArrayList();

    private ArrayList listeners = new ArrayList();

    void Awake()
    {
        if(World != null)
            Destroy(World);
        else
            World = this;
         
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        if (PlayerJournal.Count <= 0)
        {
            PlayerJournal.Add(new JournalEntry("Day 1", "Day 1"));
        }
    }

    public string GetJournalEntries()
    {
        var journal = "";
        foreach (JournalEntry entry in PlayerJournal)
        {
            journal += entry.journalEntry + "\n";
        }

        return journal;
    }

    public void AddToJournal(JournalEntry entry)
    {
        PlayerJournal.Add(entry);
    }

    public void AddToAreasVisited(string place)
    {
        if (AreasVisited.Contains(place)) return;
        
        AreasVisited.Add(place);
        Broadcast(new EventMessage(EventMessage.MessageType.Location, place));
        AddToJournal(new JournalEntry(place, "Arrived at " + place));
    }

    public void Broadcast(EventMessage message)
    {
        foreach (var listener in listeners)
        {
            (listener as IObserver<EventMessage>)?.OnNext(message);
        }
    }

    public IDisposable Subscribe(IObserver<EventMessage> observer)
    {
        if (!listeners.Contains(observer)) listeners.Add(observer);

        return new Unsubscriber(listeners, observer);
    }
    
    private class Unsubscriber : IDisposable
    {
        private ArrayList _observers;
        private IObserver<EventMessage> _observer;

        public Unsubscriber(ArrayList observers, IObserver<EventMessage> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
