using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemFocusedController : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public bool isActive = false;
    float speed = 6f;
    Vector3 defaultPosition;
    public HUDController hud;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isActive)
        {
            ToggleActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            var newPosition = new Vector3(767, 300, 0);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, defaultPosition, speed);
        }
    }

    public void ToggleActive(bool state)
    {
        isActive = state;
        if (!isActive) hud.OnItemFocusedClosed();
    }
}
