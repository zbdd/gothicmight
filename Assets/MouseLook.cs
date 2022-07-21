using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    StarterAssetsInputs starterAssets;
    TextMeshProUGUI txt;
    public GameObject objectInFocus;

    // Start is called before the first frame update
    void Start()
    {
        starterAssets = GetComponent<StarterAssetsInputs>();

        GameObject uiText = GameObject.FindGameObjectWithTag("HUD");
        if (uiText)
        {
            txt = uiText.GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        objectInFocus = null;
        if (starterAssets)
        {
            if (starterAssets.inventoryActive) CastFromScreen(); 
            else CastFromCamera();
        }
    }

    private void CastFromCamera()
    {
        RaycastHit hit;
        txt.text = "";
        var position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

        if (Physics.Raycast(position, transform.forward, out hit, Mathf.Infinity))
        {
            Details deets = hit.transform.GetComponent<Details>();
            if (deets)
            {
                txt.text = deets.name;
                objectInFocus = hit.transform.gameObject;

            }

        }
    }

    private void CastFromScreen()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(starterAssets.uiLook);
        txt.text = "";

        if (Physics.Raycast(ray, out hit))
        {
           // Debug.Log(hit.transform.name);
           Details deets = hit.transform.GetComponent<Details>();
            if (deets)
            {
                txt.text = deets.name;
                objectInFocus = hit.transform.gameObject;
            }
        }
    }
}
