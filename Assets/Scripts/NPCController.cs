using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    public ArrayList hostileTo;

    private AttackMelee _attackMelee;
    private ArrayList _objectsInProxmity;

    public string GUARD_IDLE_CONVO = "Do not disturb me citizen";

    // Start is called before the first frame update
    void Start()
    {
        _objectsInProxmity = new ArrayList();
        hostileTo = new ArrayList { GameObject.Find("PlayerCapsule") };
        _attackMelee = GetComponent<AttackMelee>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_attackMelee) Attack();
    }

    void Attack()
    {
        hostileTo ??= GetComponent<NPCController>().hostileTo;
        foreach (GameObject obj in _objectsInProxmity)
        {
            if (hostileTo.Contains(obj)) _attackMelee.Attack(obj);
        }
        
    }

    public void OnInteract()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            _objectsInProxmity.Add(other.gameObject);
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
       /* if(other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            if (hostileTo.Count <= 0) return;
            
            foreach (GameObject obj in hostileTo)
            {
                if (obj == other.gameObject) canAttack = other.gameObject;
            }
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        _objectsInProxmity.Remove(other.gameObject);
    }
}
