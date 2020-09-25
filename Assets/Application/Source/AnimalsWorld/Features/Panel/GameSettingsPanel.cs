using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class GameSettingsPanel : Panel
   {
      [SerializeField] private GameSettingsManager _gameSettingsManager = default;
      [SerializeField] private ValueSwitcherSettingsElement _musicVolumeSwitcher = default;
      [SerializeField] private ValueSwitcherSettingsElement _sfxVolumeSwitcher = default;

      private IGameSettingsManager GameSettingsManager => _gameSettingsManager;
      private IValueSwitcherSettingsElement MusicVolumeSwitcher => _musicVolumeSwitcher;
      private IValueSwitcherSettingsElement SfxVolumeSwitcher => _sfxVolumeSwitcher;

      private ICommonSettingsService _commonSettingsService;

      private void Awake()
      {
         Validation.Run(() => GameSettingsManager != null, nameof(GameSettingsManager), "Must not be null.");
         Validation.Run(() => MusicVolumeSwitcher != null, nameof(MusicVolumeSwitcher), "Must not be null.");
         Validation.Run(() => SfxVolumeSwitcher != null, nameof(SfxVolumeSwitcher), "Must not be null.");

         _commonSettingsService = GameSettingsManager.CommonSettingsService;
      }

      private void Start()
      {
         MusicVolumeSwitcher.Initialize(_commonSettingsService.MusicVolume);
         SfxVolumeSwitcher.Initialize(_commonSettingsService.SfxVolume);
      }

      private void OnEnable()
      {
         MusicVolumeSwitcher.OnValueSwitcherSettingsElementValueChanged += OnMusicVolumeValueChanged;
         SfxVolumeSwitcher.OnValueSwitcherSettingsElementValueChanged += OnSfxVolumeValueChanged;
      }

      private void OnDisable()
      {
         MusicVolumeSwitcher.OnValueSwitcherSettingsElementValueChanged -= OnMusicVolumeValueChanged;
         SfxVolumeSwitcher.OnValueSwitcherSettingsElementValueChanged -= OnSfxVolumeValueChanged;
      }

      private void OnMusicVolumeValueChanged(float value)
      {
         _commonSettingsService.MusicVolume = value;
      }

      private void OnSfxVolumeValueChanged(float value)
      {
         _commonSettingsService.SfxVolume = value;
      }

      public override void ApplyData(object data)
      {
         throw new System.NotImplementedException();
      }
   }
}