using MalenkiyApps;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class UiManager : MonoBehaviour, IUiManager
   {
      [SerializeField] private Canvas _canvas = default;
      [SerializeField] private GraphicRaycaster _graphicRaycaster = default;

      float IUiManager.ScaleFactor => _canvas.scaleFactor;

      void IUiManager.SetInputEnabled(bool enable) => _graphicRaycaster.enabled = enable;

      void IUiManager.DisableInputForDuration(float duration) => StartCoroutine(DisableInputForDuration(duration));

      public void Awake()
      {
         Validation.Run(() => _graphicRaycaster != null, "graphicRaycaster", "Must not be null.");
         Validation.Run(() => _canvas != null, "canvas", "Must not be null.");
      }

      private IEnumerator DisableInputForDuration(float duration)
      {
         _graphicRaycaster.enabled = false;

         yield return new WaitForSeconds(duration);

         _graphicRaycaster.enabled = true;
      }
   }
}