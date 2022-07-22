using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Vector3 _defaultPosition;

    private Vector3 _position;

    private bool _isEnabled = false;

    private float _speed = 6;
    // Start is called before the first frame update
    void Start()
    {
        _defaultPosition = transform.localPosition;
        _position = new Vector3(_defaultPosition.x, -210);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _isEnabled ? _position : _defaultPosition, _speed);
    }

    public void Brandish()
    {
        _isEnabled = true;
    }
    public void PutAway()
    {
        _isEnabled = false;
    }
}
