using MalenkiyApps;
using System;
using UnityEngine;

namespace AnimalsWorld
{
   public class LoadingProgressBehaviour : MonoBehaviour, ILoadingProgressBehaviour
   {
      [SerializeField]
      private LoadingProgressStepBehaviour _loadingProgressStepPrefab = default;

      private ILoadingProgressStepBehaviour[] _loadingProgressSteps;
      private Transform _transform;

      public void Initialize(int stepsCount)
      {
         if (stepsCount < 1)
         {
            throw new InvalidOperationException("Count must be greater than 1.");
         }

         _loadingProgressSteps = new ILoadingProgressStepBehaviour[stepsCount];

         for (var i = 0; i < stepsCount; i++)
         {
            _loadingProgressSteps[i] = Instantiate(_loadingProgressStepPrefab, _transform);
         }

         SetProgress(0);
      }

      public void SetProgress(int value)
      {
         if (value < 0 || value > 100)
         {
            throw new InvalidOperationException("Value must be in a range between (inclusive) 0 and 100.");
         }

         switch (value)
         {
            case 0:

               _loadingProgressSteps[0].SetEnabled(true);

               break;

            case 100:

               foreach (var step in _loadingProgressSteps)
               {
                  step.SetEnabled(true);
               }

               break;

            default:

               // 'out max value' is [length - 2] because the last index is used to complete the progress
               var targetIndex = (value - 0) * ((_loadingProgressSteps.Length - 2) - 0) / (100 - 0) + 1;

               for (var i = 0; i < _loadingProgressSteps.Length; i++)
               {
                  _loadingProgressSteps[i].SetEnabled(i <= targetIndex);
               }
               break;
         }
      }

      private void Awake()
      {
         _transform = GetComponent<Transform>();

         Validation.Run(() => _loadingProgressStepPrefab != null, nameof(_loadingProgressStepPrefab), "Must not be null.");
      }

      private void ResetProgress()
      {
         foreach (var step in _loadingProgressSteps)
         {
            step.SetEnabled(false);
         }
      }

      private void SetCompletedProgress()
      {
         foreach (var step in _loadingProgressSteps)
         {
            step.SetEnabled(true);
         }
      }
   }
}