
using UnityEngine;

public class JournalEntry: MonoBehaviour
{
    public string entryName;
    public string journalEntry;
    
    public JournalEntry(string name, string message)
    {
        entryName = name;
        journalEntry = message;
    }
}
