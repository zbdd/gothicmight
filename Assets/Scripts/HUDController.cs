using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    GameObject questlog;
    float speed = 10f;
    Vector3 targetPosition;
    ItemFocusedController iFC;

    public State state = State.idle;
    public GameObject itemFocus;
    public GameObject inventory;
    public bool questLogIsOpen = false;
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public bool dialogIsActive;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (questLogIsOpen) questlog.transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
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
        
        if (state == State.inventory)
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

    public void SetInventoryState(State state)
    {
        CloseOther();
        
        if (this.state == State.inventory && state != State.inventory) if (itemFocus.GetComponent<ItemFocusedController>().isActive) itemFocus.GetComponent<ItemFocusedController>().ToggleActive(false);
        this.state = state;
        if (state == State.inventory) inventory.SetActive(true);
        else inventory.SetActive(false);

    }
}
