using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMessage
{
    public EventMessage(
        MessageType mType,
        string message)
    {
        Type = mType;
        Message = message;

    }
    public enum MessageType
    {
        Location,
        World,
        Quest,
    }

    public MessageType Type;

    public string Message;
}
