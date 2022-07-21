using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestlogController : MonoBehaviour, IPointerClickHandler
{
    public GameObject questlogOpened;
    bool isOpened = false;
    float speed = 6f;
    Vector3 position;
    public void OnPointerClick(PointerEventData eventData)
    {
        isOpened = !isOpened;
        //Debug.Log("Got CLICK");
    }

    // Start is called before the first frame update
    void Start()
    {
         position = questlogOpened.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isOpened)
        {
            var newPosition = new Vector3(767, 300, 0);
            questlogOpened.transform.position = Vector3.MoveTowards(questlogOpened.transform.position, newPosition, speed);
        } else
        {
            questlogOpened.transform.position = Vector3.MoveTowards(questlogOpened.transform.position, position, speed);
        }
    }
}
