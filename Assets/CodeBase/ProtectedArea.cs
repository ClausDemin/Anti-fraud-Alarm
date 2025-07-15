using Assets.CodeBase.AlarmStates;
using Assets.CodeBase.AlarmStates.Interface;
using System;
using UnityEngine;


[RequireComponent(typeof(BoxCollider), typeof(AudioSource))]
public class ProtectedArea : MonoBehaviour
{
    [SerializeField, Min(0)] private float _alarmMinVolumeLevel = 0;
    [SerializeField, Range(0, 1)] private float _alarmMaxVolumeLevel = 1;
    [SerializeField, Min(0.1f)] private float _alarmVolumeChangeSpeed = 0.1f;

    private AudioSource _alarm;

    private IAlarmState _current;
    private IAlarmState _alarmState;
    private IAlarmState _releaseState;

    public event Action AlarmTriggered;
    public event Action AlarmReleased;

    private void Awake()
    {
        _alarm = GetComponent<AudioSource>();
        _alarm.volume = _alarmMinVolumeLevel;

        _releaseState = new AlarmReleasedState(_alarm, _alarmMinVolumeLevel, _alarmVolumeChangeSpeed);
        _alarmState = new AlarmedState(_alarm, _alarmMaxVolumeLevel, _alarmVolumeChangeSpeed);

        _current = _releaseState;
    }

    private void FixedUpdate()
    {
        _current.ChangeVolumeLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Thief>(out var thief))
        {
            _current = _alarmState;

            AlarmTriggered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Thief>(out var thief))
        {
            _current = _releaseState;

            AlarmReleased?.Invoke();
        }
    }
}
