using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ArrayList list;
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
            if (i > 0) xAxis = maxX / i;
            obj.transform.localPosition = new Vector3(startPos.x + (110 * xAxis), startPos.y + (110 * Mathf.FloorToInt(i/maxX)));

           // Debug.Log("" + startPos.x + (110 * xAxis) + "" + startPos.y + (110 * Mathf.FloorToInt(i / maxX)));
        }
    }
}
