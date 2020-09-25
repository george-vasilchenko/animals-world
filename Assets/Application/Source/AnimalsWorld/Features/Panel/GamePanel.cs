using System;

namespace AnimalsWorld
{
   public class GamePanel : Panel
   {
      private ILevel _currentLevel;

      public void LoadLevelChapter1(int levelIndex)
      {
         _currentLevel = GameManager.Instance.LevelManager.GetLevelAtIndex(0, levelIndex);

         if (_currentLevel == null)
         {
            throw new NullReferenceException($"Returned level at index {levelIndex} is null.");
         }

         var allLevels = GameManager.Instance.LevelManager.GetAllLevels();

         foreach (var level in allLevels)
         {
            if (level != _currentLevel)
            {
               level.Hide();
            }
         }

         _currentLevel.Show();
      }

      public void LoadLevelChapter2(int levelIndex)
      {
         _currentLevel = GameManager.Instance.LevelManager.GetLevelAtIndex(1, levelIndex);

         if (_currentLevel == null)
         {
            throw new NullReferenceException($"Returned level at index {levelIndex} is null.");
         }

         var allLevels = GameManager.Instance.LevelManager.GetAllLevels();

         foreach (var level in allLevels)
         {
            if (level != _currentLevel)
            {
               level.Hide();
            }
         }

         _currentLevel.Show();
      }

      public override void ApplyData(object data)
      {
         throw new NotImplementedException(GetType().Name);
      }

      private void HideAllLevels()
      {
         foreach (var level in GameManager.Instance.LevelManager.GetAllLevels())
         {
            level.Hide();
         }
      }

      #region Unity API

      private void OnEnable()
      {
         HideAllLevels();
         _currentLevel = null;
      }

      private void OnDisable()
      {
         HideAllLevels();
         _currentLevel = null;
      }

      #endregion Unity API
   }
}