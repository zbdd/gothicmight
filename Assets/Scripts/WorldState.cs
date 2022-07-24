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
    public AudioClip AddToJournalSfx;
    public AudioSource audioSource;

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
            PlayerJournal.Add(GetComponent<JournalEntry>());
        }
    }

    public string GetJournalEntries()
    {
        var journal = "";
        foreach (JournalEntry entry in PlayerJournal)
        {
            journal += "\n" + entry.journalEntry + "\n";
        }

        return journal;
    }

    public void AddToJournal(JournalEntry entry)
    {
        GameObject.Find("HUD").GetComponent<HUDController>().AddTextEvent("Journal entry added");
        PlayerJournal.Add(entry);
        
        if (!audioSource || !AddToJournalSfx) return;
        audioSource.clip = AddToJournalSfx;
        audioSource.Play();
    }

    public void AddToAreasVisited(string place)
    {
        if (AreasVisited.Contains(place)) return;
        
        AreasVisited.Add(place);
        Broadcast(new EventMessage(EventMessage.MessageType.Location, place));
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
