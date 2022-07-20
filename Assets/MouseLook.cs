using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    StarterAssetsInputs starterAssets;
    TextMeshProUGUI txt;

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
        if (starterAssets)
        {
            if (starterAssets.inventoryActive) CastFromScreen(); 
            else CastFromCamera();
        }
    }

    private void CastFromCamera()
    {
        Vector3 direction = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        RaycastHit hit;
        txt.text = "";

        if (Physics.Raycast(this.transform.position, direction, out hit, Mathf.Infinity))
        {
            Details deets = hit.transform.GetComponent<Details>();
            if (deets)
            {
                txt.text = deets.name;

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
            Details deets = hit.transform.GetComponent<Details>();
            if (deets)
            {
                txt.text = deets.name;

            }
        }
    }
}
