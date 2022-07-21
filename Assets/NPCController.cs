using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public ArrayList hostileTo;

    public string GUARD_IDLE_CONVO = "Do not disturb me citizen";

    // Start is called before the first frame update
    void Start()
    {
        hostileTo = new ArrayList();
        hostileTo.Add(GameObject.Find("PlayerCapsule"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
