using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ArrayList list;
    public GameObject selected;
    public int selectedPosition = -1;
    Vector3 startPos;
    int maxX = 3;


    private void Start()
    {
        list = new ArrayList();
        startPos = new Vector3(380, 200);

        AddToList(GameObject.Find("Questlog"));
    }

    private void Update()
    {
        
    }

    public void AddToList(GameObject item)
    {
        list.Add(item);
        list.Sort();
        DisplayList();
    }

    private void DisplayList()
    {
        for(var i =0; i < list.Count; i++)
        {
            GameObject obj = list[i] as GameObject;
            var xAxis = 0;
            var yAxis = 0;

            if (i > 0) xAxis = 110 * (maxX / i);
            if (i > 0) yAxis = 110 * Mathf.FloorToInt(i / maxX);

            if (selectedPosition == -1) selectedPosition = i;
            if (selectedPosition == i) selected.transform.localPosition = new Vector3(startPos.x + xAxis, startPos.y + yAxis);
            obj.transform.localPosition = new Vector3(startPos.x + xAxis, startPos.y + yAxis);

           // Debug.Log("" + startPos.x + (110 * xAxis) + "" + startPos.y + (110 * Mathf.FloorToInt(i / maxX)));
        }
    }

    public InventoryItemController GetSelected()
    {
        if (selectedPosition > -1) return (list[selectedPosition] as GameObject).GetComponent<InventoryItemController>();
        else return null;
    }
}
