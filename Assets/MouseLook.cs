using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        RaycastHit hit;

        GameObject uiText = GameObject.FindGameObjectWithTag("HUD");
        if (uiText)
        {
            TextMeshProUGUI txt = uiText.GetComponent<TextMeshProUGUI>();
            if (txt)
            {

                if (Physics.Raycast(this.transform.position, direction, out hit, Mathf.Infinity))
                {
                    Debug.Log(hit.transform.name);

                    Details deets = hit.transform.GetComponent<Details>();
                    if (deets)
                    {
                        txt.text = deets.name;

                    }

                }
                else txt.text = "";
            }
        }
    }
}
