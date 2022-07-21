using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationController : MonoBehaviour
{
    public InventoryController iC;
    NPCController npc;
    State state;
    enum State
    {
        idle,
        hostile,
        friendly
    }

    public void Start()
    {
        state = State.idle;
        npc = GetComponent<NPCController>();
    }

    public void OnInteract()
    {
        string option = "";
        switch (state)
        {
            case State.idle:
                option = npc.GUARD_IDLE_CONVO;
                break;
        }

        if (iC)
        {
            iC.SetDialogBox(option);
        }
    }
}
