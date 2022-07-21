using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationController : MonoBehaviour
{
    public InventoryController iC;
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
    }

    public void OnInteract()
    {
        string option = "";
        switch (state)
        {
            case State.idle:
                option = "Do not disturb me citizen";
                break;
        }

        if (iC)
        {
            iC.SetDialogBox(option);
        }
    }
}
