using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour, IPlayerInputListener
{
    public List<ItemController> list;
    public GameObject selected;
    public int selectedPosition = -1;
    public Image selectedImage;
    public TextMeshProUGUI selectedText;
    readonly Vector3 startPos = new Vector3(-115f, 160f);
    int maxX = 3;


    private void Start()
    {
        list ??= new List<ItemController>();
        GameObject.Find("PlayerCapsule").GetComponent<StarterAssetsInputs>().Register(this);
    }

    public void OpenInventory()
    {
        list.Sort();
        DisplayList();
    }

    public void AddToList(ItemController item)
    {
        list.Add(item);
        list.Sort();
        DisplayList();
    }

    private void DisplayList()
    {
        for(var i =0; i < list.Count; i++)
        {
            ItemController obj = list[i];
            var xAxis = 0;
            var yAxis = 0;

            if (i > 0) xAxis = 110 * Mathf.FloorToInt(i%maxX);
            if (i > 0) yAxis = 110 * Mathf.FloorToInt(i / maxX);

            if (selectedPosition == -1) selectedPosition = 0;
            else if (selectedPosition > list.Count - 1) selectedPosition = list.Count - 1;
            
            if (selectedPosition == i) selected.transform.localPosition = new Vector3((startPos.x + xAxis), (startPos.y - yAxis));
   
            obj.transform.localPosition = new Vector3(startPos.x + xAxis, startPos.y - yAxis);

           // Debug.Log("" + startPos.x + (110 * xAxis) + "" + startPos.y + (110 * Mathf.FloorToInt(i / maxX)));
        }

        if (selectedPosition > -1)
        {
            ItemController select = (list[selectedPosition]);
            if (select)
            {
                Image img = select.GetComponent<Image>();
                if (img) selectedImage.sprite = img.sprite;

                Details deets = select.GetComponent<Details>();
                if (deets) selectedText.text = deets.name;
            }
        }
    }
    
    public void SetSelectedView(bool state)
    {
        if (state) selectedImage.gameObject.SetActive(true);
        else selectedImage.gameObject.SetActive(false);
    }

    public void SetSelected(ItemController select)
    {
        var pos = list.IndexOf(select);
        if (pos <= -1) return;
        
        selectedPosition = pos;
        DisplayList();
    }

    public ItemController GetSelected()
    {
        if (selectedPosition > -1) return (list[selectedPosition]).GetComponent<ItemController>();
        else return null;
    }

    public void OnUpdateFromHandler(StarterAssetsInputs.Input type)
    {
        if (!gameObject.activeSelf) return;
        
        switch (type)
        {
            case StarterAssetsInputs.Input.Left:
                selectedPosition--;
                break;
            case StarterAssetsInputs.Input.Right:
                selectedPosition++;
                break;
            case StarterAssetsInputs.Input.Up:
                selectedPosition -= 3;
                break;
            case StarterAssetsInputs.Input.Down:
                selectedPosition += 3;
                break;
        }
        
        DisplayList();
    }
}
