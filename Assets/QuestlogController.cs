using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestlogController : MonoBehaviour, IPointerClickHandler
{
    public GameObject questlogOpened;

    HUDController hud;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (hud) hud.ShowFocused(GetComponent<Image>());
        //Debug.Log("Got CLICK");
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
