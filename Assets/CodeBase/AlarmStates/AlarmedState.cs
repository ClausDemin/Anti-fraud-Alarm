using Assets.CodeBase.AlarmStates.Interface;
using UnityEngine;

namespace Assets.CodeBase.AlarmStates
{
    public class AlarmedState : IAlarmState
    {
        private AudioSource _alarmSoundPlayer;
        private float _targetVolume;
        private float _volumeChangeRate;

        public AlarmedState(AudioSource alarmSoundPlayer, float targetVolume, float volumeChangeRate)
        {
            _alarmSoundPlayer = alarmSoundPlayer;
            _targetVolume = targetVolume;
            _volumeChangeRate = volumeChangeRate;
        }

        public void ChangeVolumeLevel()
        {
            float volume = _alarmSoundPlayer.volume;

            if (_alarmSoundPlayer.isPlaying == false)
            {
                _alarmSoundPlayer.Play();
            }

            if (volume < _targetVolume)
            {
                _alarmSoundPlayer.volume = Mathf.MoveTowards(_alarmSoundPlayer.volume, _targetVolume, _volumeChangeRate * Time.fixedDeltaTime);
            }
        }
    }
}
