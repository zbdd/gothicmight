using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Interfaces;

public class ItemController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IComparable<ItemController>, IDetails
{
    private HUDController _hud;

    public bool canPickUp = false;
    public bool canEquip = false;
    public bool canFocus = false;

    public Sprite imageFocused;
    public string itemFocusedText;
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
        if (_hud && canFocus) _hud.ShowFocused(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _hud = GameObject.Find("HUD").GetComponent<HUDController>();
    }

    public int CompareTo(ItemController other)
    {
        Details d1 = gameObject.GetComponent<Details>();
        Details d2 = other.GetComponent<Details>();

        if (d1 == null || d2 == null) return 0;

        return String.CompareOrdinal(d1.name, d2.name);
    }

    public string Name { get; set; }
    public string Description { get; set; }
}
