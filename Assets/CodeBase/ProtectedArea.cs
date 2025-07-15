using System;
using System.Collections;
using UnityEngine;

public class ProtectedArea : MonoBehaviour
{
    [SerializeField, Min(0)] private float _alarmMinVolumeLevel = 0;
    [SerializeField, Range(0, 1)] private float _alarmMaxVolumeLevel = 1;
    [SerializeField, Min(0.1f)] private float _alarmVolumeChangeSpeed = 0.1f;

    private AudioSource _alarm;

    public event Action AlarmTriggered;
    public event Action AlarmReleased;

    private void Awake()
    {
        _alarm = GetComponent<AudioSource>();
        _alarm.volume = _alarmMinVolumeLevel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Thief>(out var thief))
        {
            _alarm.Play();

            StartCoroutine(ChangeAlarmVolume(_alarmMaxVolumeLevel));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Thief>(out var thief))
        {
            StartCoroutine(ChangeAlarmVolume(_alarmMinVolumeLevel));
        }
    }

    private IEnumerator ChangeAlarmVolume(float targetVolume)
    {
        while (Mathf.Approximately(_alarm.volume, targetVolume) == false)
        {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, targetVolume, _alarmVolumeChangeSpeed * Time.deltaTime);

            yield return null;
        }

        Notify();
    }

    private void Notify()
    {
        if (_alarm.volume == _alarmMaxVolumeLevel)
        {
            AlarmTriggered?.Invoke();
        }
        else if (_alarm.volume == _alarmMinVolumeLevel)
        {
            AlarmReleased?.Invoke();

            _alarm.Stop();
        }
    }
}