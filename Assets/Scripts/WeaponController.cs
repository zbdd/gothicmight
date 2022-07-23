using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Vector3 _defaultPosition;
    private Vector3 _defaultRotation;
    private Vector3 _raisedPosition;

    private Vector3 _attackedPosition;
    private Vector3 _attackedRotation;
    private float _currentRotation;

    private State _currentState;
    private State _oldState;
    private State _nextState;

    public enum State
    {
        Raised,
        Lowered,
        Attack,
        Reset,
        Idle
    }

    private float _speed = 4;
    // Start is called before the first frame update
    void Start()
    {
        _currentState = State.Lowered;
        _oldState = State.Lowered;
        _nextState = State.Idle;
        
        _defaultPosition = transform.localPosition;
        _defaultRotation = new Vector3(0, 0, -38);
        _attackedRotation = new Vector3(0, 0, 38);

        _raisedPosition = new Vector3(_defaultPosition.x, -210);
        _attackedPosition = new Vector3(_raisedPosition.x - 400, _raisedPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case State.Lowered:
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _defaultPosition, _speed);
                if (transform.localPosition == _defaultPosition) SetState(State.Idle);
                break;
            case State.Raised:
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _raisedPosition, _speed);
                transform.eulerAngles = _defaultRotation;
                if (transform.localPosition == _raisedPosition) SetState(State.Idle);
                break;
            case State.Attack:
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _attackedPosition, _speed);
                if (transform.eulerAngles != _attackedRotation)
                    transform.eulerAngles = new Vector3(0, 0, _currentRotation += 0.6f);
                if (transform.localPosition == _attackedPosition) SetState(State.Reset);
                break;
            case State.Reset:
                transform.localPosition = _raisedPosition;
                transform.eulerAngles = _defaultRotation;
                _currentRotation = _defaultRotation.z;
                break;

        }
    }

    public void SetState(State newState)
    {
        if (_nextState != State.Idle)
        {
            newState = _nextState;
            _nextState = State.Idle;
        }
        if (newState == State.Attack && _currentState == State.Lowered)
        {
            newState = State.Raised;
            _nextState = State.Attack;
        }

        _oldState = _currentState;
        _currentState = newState;
    }
}
