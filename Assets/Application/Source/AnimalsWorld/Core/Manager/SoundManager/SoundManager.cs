using MalenkiyApps;
using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AnimalsWorld
{
   public class SoundManager : MonoBehaviour, ISoundManager
   {
      public IGameSettingsManager GameSettingsManager => _gameSettingsManager;

      public AudioSource BackgroundAudioSource;
      public AudioSource SfxAudioSource;

      [Header("Clips")]
      public AudioClip ClickClip;

      public AudioClip WinClip;
      public AudioClip[] SnapClips;
      public AudioClip[] BackgroundClips;
      public AudioClip[] PopClips;

      [SerializeField]
      private GameSettingsManager _gameSettingsManager = default;

      private void OnEnable()
      {
         GameSettingsManager.CommonSettingsService.OnCommonSettingsMusicVolumeChanged +=
            CommonSettingsService_CommonSettings_OnMusicVolumeChanged;

         GameSettingsManager.CommonSettingsService.OnCommonSettingsSfxVolumeChanged +=
            CommonSettingsService_CommonSettings_OnSfxVolumeChanged;
      }

      private void OnDisable()
      {
         GameSettingsManager.CommonSettingsService.OnCommonSettingsMusicVolumeChanged -=
            CommonSettingsService_CommonSettings_OnMusicVolumeChanged;

         GameSettingsManager.CommonSettingsService.OnCommonSettingsMusicVolumeChanged -=
            CommonSettingsService_CommonSettings_OnSfxVolumeChanged;
      }

      public void Awake()
      {
         Validation.Run(() => BackgroundAudioSource != null, "BackgroundAudioSource", "Must not be null.");
         Validation.Run(() => SfxAudioSource != null, "SfxAudioSource", "Must not be null.");
         Validation.Run(() => BackgroundClips.Any(), "BackgroundClips", "Must not be empty.");
         Validation.Run(() => PopClips.Any(), "PopClips", "Must not be empty.");
         Validation.Run(() => ClickClip != null, "ClickClip", "Must not be null.");
         Validation.Run(() => WinClip != null, "WinClip", "Must not be null.");
         Validation.Run(() => SnapClips.Any(), "SnapClips", "Must not be empty.");
         Validation.Run(() => GameSettingsManager != null, "GameSettingsManager", "Must not be null.");
      }

      private void Start()
      {
         BackgroundAudioSource.loop = true;

         PlayBackgroundMusic();
      }

      public void PlayClickSound()
      {
         if (SfxAudioSource.isPlaying)
         {
            SfxAudioSource.Stop();
         }

         SfxAudioSource.PlayOneShot(ClickClip);
      }

      public void PlayVictorySound()
      {
         if (SfxAudioSource.isPlaying)
         {
            SfxAudioSource.Stop();
         }

         SfxAudioSource.PlayOneShot(WinClip);
      }

      public void PlayPopSound()
      {
         if (SfxAudioSource.isPlaying)
         {
            SfxAudioSource.Stop();
         }

         var randomIndex = Random.Range(0, PopClips.Length - 1);
         SfxAudioSource.PlayOneShot(PopClips[randomIndex]);
      }

      public void PlaySnapSound()
      {
         if (SfxAudioSource.isPlaying)
         {
            SfxAudioSource.Stop();
         }

         var randomIndex = Random.Range(0, BackgroundClips.Length - 1);
         SfxAudioSource.PlayOneShot(SnapClips[randomIndex]);
      }

      public void PlayBackgroundMusic()
      {
         if (!BackgroundAudioSource.isPlaying)
         {
            var randomIndex = Random.Range(0, BackgroundClips.Length - 1);
            BackgroundAudioSource.clip = BackgroundClips[randomIndex];
            BackgroundAudioSource.Play();
         }
      }

      private void CommonSettingsService_CommonSettings_OnMusicVolumeChanged(float value)
      {
         if (value < 0 || value > 1.0f)
         {
            throw new ArgumentException("MusicVolume value must be within a range of 0 and 1.", nameof(value));
         }

         BackgroundAudioSource.volume = value;
      }

      private void CommonSettingsService_CommonSettings_OnSfxVolumeChanged(float value)
      {
         if (value < 0 || value > 1.0f)
         {
            throw new ArgumentException("SfxVolume value must be within a range of 0 and 1.", nameof(value));
         }

         SfxAudioSource.volume = value;
      }
   }
}