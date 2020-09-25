using UnityEngine;

namespace AnimalsWorld
{
   public class CommonSettingsService : MonoBehaviour, ICommonSettingsService
   {
      public event OnCommonSettingsVolumeChangedDelegate OnCommonSettingsMusicVolumeChanged;

      public event OnCommonSettingsVolumeChangedDelegate OnCommonSettingsSfxVolumeChanged;

      private float _volume;
      private float _sfxVolume;

      float ICommonSettingsService.MusicVolume
      {
         get => _volume;
         set
         {
            _volume = Mathf.Clamp01(value);
            OnCommonSettingsMusicVolumeChanged?.Invoke(_volume);
         }
      }

      float ICommonSettingsService.SfxVolume
      {
         get => _sfxVolume;
         set
         {
            _sfxVolume = Mathf.Clamp01(value);
            OnCommonSettingsSfxVolumeChanged?.Invoke(_sfxVolume);
         }
      }
   }
}