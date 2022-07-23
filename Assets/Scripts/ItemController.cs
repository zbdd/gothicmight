using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IComparable<ItemController>
{
    private HUDController _hud;

    public bool canPickUp = false;
    public bool canEquip = false;
    public bool canFocus = false;
    
    public ItemStats stats;
    public void OnPointerClick(PointerEventData eventData)
    {
        OnItemClicked();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Inventory").GetComponent<InventoryController>().SetSelected(this);
    }

    public void OnItemClicked()
    {
        if (_hud && canFocus) _hud.ShowFocused(GetComponent<Image>());
    }

    // Start is called before the first frame update
    void Start()
    {
        _hud = GameObject.Find("HUD").GetComponent<HUDController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public int CompareTo(ItemController other)
    {
        Details d1 = gameObject.GetComponent<Details>();
        Details d2 = other.GetComponent<Details>();

        if (d1 == null || d2 == null) return 0;

        return String.CompareOrdinal(d1.name, d2.name);
    }
}
