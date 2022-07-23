using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour, IPlayerInputListener, IObserver<EventMessage>
{
    GameObject questlog;
    float speed = 10f;
    Vector3 targetPosition;
    ItemFocusedController iFC;

    public State OldState { get; private set; } = State.idle;
    public State CurrentState { get; private set; } = State.idle;
    public GameObject itemFocus;
    public GameObject inventory;
    public bool questLogIsOpen = false;
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public bool dialogIsActive;
    public WeaponController weapon;
    public TextMeshProUGUI debugText;
    public TextMeshProUGUI locationUpdate;

    public enum State
    {
        idle,
        inventory,
        fight
    }

    // Start is called before the first frame update
    void Start()
    {
        questlog = GameObject.Find("Questlog Opened");
        targetPosition = new Vector3(0, -710, 0);
        iFC = itemFocus.GetComponent<ItemFocusedController>();

        StarterAssetsInputs startInput = GameObject.Find("PlayerCapsule").GetComponent<StarterAssetsInputs>();
        startInput.Register(this);

        WorldState.World.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (questLogIsOpen) questlog.transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        debugText.text = CurrentState + "";
    }

    public void SetState(State newState)
    {
        if (CurrentState == newState) return;
        
        OldState = CurrentState;
        OnStateEnd(OldState);
        CurrentState = newState;
        OnStateStart(CurrentState);
    }

    private void OnStateEnd(State oldState)
    {
        switch (oldState)
        {
            case State.fight:
                weapon.PutAway();
                break;
            case State.inventory:
                inventory.SetActive(false);
                if (itemFocus.GetComponent<ItemFocusedController>().isActive) itemFocus.GetComponent<ItemFocusedController>().ToggleActive(false);
                break;
        }
        
        CloseOther();
    }

    private void OnStateStart(State newState)
    {
        switch (newState)
        {
            case State.fight:
                weapon.Brandish();
                break;
            case State.inventory:
                inventory.SetActive(true);
                break;
        }
    }

    public void SetDialogBox(string text)
    {
        if (dialogIsActive) { CloseDialogBox(); return; }

        dialogIsActive = true;
        dialogText.text = text;
        dialogBox.SetActive(true);
    }

    public void OnAnyKey()
    {
        CloseOther();
    }

    private void CloseOther()
    {
        if (dialogIsActive) CloseDialogBox();
    }

    public void CloseDialogBox()
    {
        dialogIsActive = false;
        dialogBox.SetActive(false);
    }

    public void ShowFocused(Image image)
    {
        iFC.image = image;
        iFC.ToggleActive(true);
    }

    public void OnMenuInteract()
    {
        CloseOther();
        
        if (CurrentState == State.inventory)
        {
            if (inventory.GetComponent<InventoryController>().selectedPosition > -1)
            {
                InventoryItemController invCon = inventory.GetComponent<InventoryController>().GetSelected();
                if (invCon)
                {
                    invCon.OnItemSelected();
                }
            }
        }
    }

    public void OnUpdateFromHandler(StarterAssetsInputs.Input type)
    {
        switch (type)
        {
            case StarterAssetsInputs.Input.Any:
                OnAnyKey();
                break;
            case StarterAssetsInputs.Input.FightMode:
                SetState(CurrentState == State.fight ? OldState : State.fight);
                break;
        }
    }

    public void OnCompleted()
    {
       
    }

    public void OnError(Exception error)
    {
       
    }

    public void OnNext(EventMessage value)
    {
        if (value.Type == EventMessage.MessageType.Location) HandleLocationUpdate(value.Message);
    }

    private void HandleLocationUpdate(string message)
    {
        locationUpdate.alpha = 2f;
        locationUpdate.text = "Entering " + message;
        StartCoroutine(FadeToBlack(2f));
    }

    private IEnumerator FadeToBlack(float time)
    {
        while (locationUpdate.alpha > 0.0f)
        {
            locationUpdate.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }
}
