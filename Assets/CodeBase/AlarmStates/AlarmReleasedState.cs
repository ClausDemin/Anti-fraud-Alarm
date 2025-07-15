using Assets.CodeBase.AlarmStates.Interface;
using UnityEngine;

namespace Assets.CodeBase.AlarmStates
{
    public class AlarmReleasedState : IAlarmState
    {
        private AudioSource _alarmSoundPlayer;
        private float _targetVolume;
        private float _volumeChangeRate;

        public AlarmReleasedState(AudioSource alarmSoundPlayer, float targetVolume, float volumeChangeRate)
        {
            _alarmSoundPlayer = alarmSoundPlayer;
            _targetVolume = targetVolume;
            _volumeChangeRate = volumeChangeRate;
        }

        public void ChangeVolumeLevel()
        {
            float volume = _alarmSoundPlayer.volume;

            if (volume > _targetVolume)
            {
                _alarmSoundPlayer.volume = Mathf.MoveTowards(_alarmSoundPlayer.volume, _targetVolume, _volumeChangeRate * Time.fixedDeltaTime);
            }
            else
            {
                if (_alarmSoundPlayer.isPlaying)
                {
                    _alarmSoundPlayer.Stop();
                }
            }
        }
    }
}
