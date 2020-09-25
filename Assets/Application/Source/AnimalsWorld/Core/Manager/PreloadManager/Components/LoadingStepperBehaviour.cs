using System;
using System.Collections;
using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class LoadingStepperBehaviour : MonoBehaviour
   {
      [SerializeField]
      private LoadingProgressStepBehaviour _loadingProgressStepPrefab = default;

      [SerializeField]
      private float _intervalSeconds = 0.3f;

      [SerializeField]
      private int _stepCount = 3;

      private ILoadingProgressStepBehaviour[] _loadingProgressSteps;
      private Transform _transform;

      private int _currentIndex = 0;
      private float _counter = 0;

      private void Awake()
      {
         _transform = GetComponent<Transform>();

         Validation.Run(() => _loadingProgressStepPrefab != null, nameof(_loadingProgressStepPrefab),
            "Must not be null.");

         Validation.Run(() => _stepCount > 1, nameof(_loadingProgressStepPrefab),
            "Count must be greater than 1.");

         Validation.Run(() => _intervalSeconds > 0.25f, nameof(_loadingProgressStepPrefab),
            "Count must be greater than 0.25.");
      }

      private void Start()
      {
         _loadingProgressSteps = new ILoadingProgressStepBehaviour[_stepCount];

         for (var i = 0; i < _stepCount; i++)
         {
            _loadingProgressSteps[i] = Instantiate(_loadingProgressStepPrefab, _transform);
         }
      }

      private void Update()
      {
         // update steps
         for (var i = 0; i < _loadingProgressSteps.Length; i++)
         {
            if (i != _currentIndex)
            {
               _loadingProgressSteps[i].SetEnabled(false);
               continue;
            }

            _loadingProgressSteps[i].SetEnabled(true);
         }

         // wait
         _counter += Time.deltaTime;
         if (_counter < _intervalSeconds)
         {
            return;
         }
         _counter = 0;

         // increment index
         _currentIndex++;

         if (_currentIndex > _loadingProgressSteps.Length - 1)
         {
            _currentIndex = 0;
         }
      }
   }
}