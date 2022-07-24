using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    StarterAssetsInputs starterAssets;
    public TextMeshProUGUI txt;
    HUDController hud;
    public GameObject objectInFocus;

    // Start is called before the first frame update
    void Start()
    {
        starterAssets = GetComponent<StarterAssetsInputs>();

        GameObject hudGO = GameObject.Find("HUD");
        hud = hudGO.GetComponent<HUDController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hud)
        {
            if (hud.CurrentState == HUDController.State.idle) CastFromScreen(); 
            else CastFromCamera();
        }
    }

    public GameObject CastFromCamera()
    {
        txt.text = "";
        var thisPosition = transform.position;
        var position = new Vector3(thisPosition.x, thisPosition.y + 1, thisPosition.z);

        if (!Physics.Raycast(position, transform.forward, out var hit, Mathf.Infinity)) return null;

        if (hit.transform.CompareTag("Location")) return null;
        
        Details deets = hit.transform.GetComponent<Details>();
        if (deets)
        {
            txt.text = deets.name;
            objectInFocus = hit.transform.gameObject;

        }

        return hit.transform.gameObject;
    }

    private void CastFromScreen()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(starterAssets.uiLook);
        txt.text = "";

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Location")) return;
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
