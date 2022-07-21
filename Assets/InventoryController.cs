using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    GameObject questlog;
    float speed = 10f;
    Vector3 targetPosition;

    public bool questLogIsOpen = false;
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public bool dialogIsActive;

    // Start is called before the first frame update
    void Start()
    {
        questlog = GameObject.Find("Questlog Opened");
        targetPosition = new Vector3(0, -710, 0);
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

    public void CloseDialogBox()
    {
        dialogIsActive = false;
        dialogBox.SetActive(false);
    }

}
