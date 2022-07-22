using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour, IPointerClickHandler
{
    public GameObject questlogOpened;

    HUDController hud;
    public void OnPointerClick(PointerEventData eventData)
    {
        OnItemSelected();
        //Debug.Log("Got CLICK");
    }

    public void OnItemSelected()
    {
        if (hud) hud.ShowFocused(GetComponent<Image>());
    }

    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.Find("HUD").GetComponent<HUDController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
