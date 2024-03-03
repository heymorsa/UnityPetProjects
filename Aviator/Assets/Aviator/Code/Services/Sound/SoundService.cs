using System.Collections.Generic;
using System.Linq;
using Aviator.Code.Data.Enums;
using Aviator.Code.Data.Progress;
using Aviator.Code.Data.StaticData.Sounds;
using UnityEngine;

namespace Aviator.Code.Services.Sound
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        public bool MusicMuted
        {
            get => _musicSource.mute;
            set => _musicSource.mute = !value;
        }

        public bool EffectsMuted
        {
            get => _effectsSource.mute;
            set => _effectsSource.mute = !value;
        }
        
        public bool FlyMuted
        {
            get => _flySource.mute;
            set => _flySource.mute = !value;
        }

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _effectsSource;
        [SerializeField] private AudioSource _flySource;
        
        private Dictionary<SoundId, AudioClipData> _sounds;
        
        public void Construct(SoundData soundData, Settings userSettings)
        {
            _sounds = soundData.AudioEffectClips.ToDictionary(s => s.Id);
            
            _musicSource.clip = soundData.BackgroundMusic;
            _musicSource.volume = userSettings.MusicVolume;
            _musicSource.mute = !userSettings.IsMusicSoundActive;

            _effectsSource.volume = userSettings.EffectsVolume;
            _effectsSource.mute = !userSettings.IsEffectsSoundActive;

            _flySource.clip = soundData.FlySound;
            _flySource.volume = userSettings.EffectsVolume;
            _flySource.mute = !userSettings.IsEffectsSoundActive;
        }

        public void PlayBackgroundMusic() => _musicSource.Play();

        public void StopBackgroundMusic() => _musicSource.Stop();

        public void PlayEffectSound(SoundId soundId) =>
            _effectsSource.PlayOneShot(_sounds[soundId].Clip);

       public void PlayFlySound(SoundId soundId) =>
            _flySource.Play();
       
       public void StopFlySound(SoundId soundId) =>
            _flySource.Stop();

       public void SetBackgroundMusicVolume(float volume) =>
            _musicSource.volume = volume;

        public void SetEffectsVolume(float volume) =>
            _effectsSource.volume = volume;
        
        public void SetFlyVolume(float volume) =>
            _flySource.volume = volume;
    }
}