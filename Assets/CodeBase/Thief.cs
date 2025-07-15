using Assets.CodeBase.ThiefStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class Thief : MonoBehaviour
{
    [SerializeField] private ProtectedArea _area;
    [SerializeField] private Vector3 _escapePoint;
    [SerializeField, Min(0)] private float _alarmSustainabilitySeconds;

    private NavMeshAgent _agent;

    private DestinationMovement _current;
    private DestinationMovement _protectedAreaMovement;
    private DestinationMovement _escapeMovement;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _protectedAreaMovement = new DestinationMovement(_agent, _area.transform.position);
        _escapeMovement = new DestinationMovement(_agent, _escapePoint);

        _current = _protectedAreaMovement;
    }

    private void Start()
    {
        _current.Move();
    }

    private void OnEnable()
    {
        _area.AlarmTriggered += OnAlarmTriggered;
        _area.AlarmReleased += OnAlarmReleased;
    }

    private void OnDisable()
    {
        _area.AlarmTriggered -= OnAlarmTriggered;
        _area.AlarmReleased -= OnAlarmReleased;
    }

    private void OnAlarmReleased() 
    {
        StartCoroutine(MoveDestination(_protectedAreaMovement));
    }

    private void OnAlarmTriggered() 
    {
        StartCoroutine(MoveDestination(_escapeMovement));
    }

    private IEnumerator MoveDestination(DestinationMovement movement) 
    {
        YieldInstruction copeAlarm = new WaitForSeconds(_alarmSustainabilitySeconds);

        yield return copeAlarm;

        _current = movement;

        _current.Move();
    }
}