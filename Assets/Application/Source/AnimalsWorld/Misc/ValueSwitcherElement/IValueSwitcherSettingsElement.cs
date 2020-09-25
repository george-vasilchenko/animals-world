namespace AnimalsWorld
{
   public interface IValueSwitcherSettingsElement
   {
      event OnValueSwitcherSettingsElementValueChangedDelegate OnValueSwitcherSettingsElementValueChanged;

      void Initialize(float value);
   }
}