using System;
using UnityEngine;

namespace AnimalsWorld
{
   public class StepperBasedScrollTrigger : ScrollTriggerBase
   {
      public override event OnScrollTriggerDelegate OnTriggered;

      public IScrollItemsCollection ScrollItemsCollection => _scrollItemsCollection;

      public IScrollController ScrollController => _scrollController;

      public GameObject ScrollStepContainerPrefab;

      [SerializeField]
      private ScrollController _scrollController = default;

      [SerializeField]
      private ScrollItemsCollection _scrollItemsCollection = default;

      private ScrollStep[] _steps;
      private int _lastStepIndex;

      #region Unity API

      public void Awake()
      {
         if (ScrollController == null ||
             ScrollItemsCollection == null ||
             ScrollStepContainerPrefab == null)
         {
            throw new Exception("Not all dependencies were resolved.");
         }
      }

      public void OnEnable()
      {
         if (ScrollController != null)
         {
            ScrollController.OnScrollControllerCurrentIndexChanged += OnTargetValueExternallyChanged;
         }

         if (ScrollItemsCollection != null)
         {
            ScrollItemsCollection.OnScrollItemsCollectionLengthSet += ScrollItemsCollection_OnScrollItemsCollectionLengthSet;
         }
      }

      public void OnDisable()
      {
         if (ScrollController != null)
         {
            ScrollController.OnScrollControllerCurrentIndexChanged -= OnTargetValueExternallyChanged;
         }

         if (ScrollItemsCollection != null)
         {
            ScrollItemsCollection.OnScrollItemsCollectionLengthSet -= ScrollItemsCollection_OnScrollItemsCollectionLengthSet;
         }
      }

      #endregion Unity API

      public override void OnTargetValueExternallyChanged(int newValue)
      {
         _lastStepIndex = newValue;
         UpdateSteps(newValue);
      }

      private void ScrollItemsCollection_OnScrollItemsCollectionLengthSet(int length)
      {
         SpawnSteps(length);
         UpdateSteps(0, true);
      }

      private void OnScrollStepperStepClickedEventHandlerAndTrigger(int stepIndex)
      {
         var changeDirection = _lastStepIndex - stepIndex;

         OnTriggered?.Invoke(changeDirection);

         _lastStepIndex = stepIndex;

         UpdateSteps(stepIndex);
      }

      private void SpawnSteps(int count)
      {
         _steps = new ScrollStep[count];

         for (var i = 0; i < count; i++)
         {
            var instance = Instantiate(ScrollStepContainerPrefab, transform, false);
            _steps[i] = instance.transform.GetChild(0).GetComponent<ScrollStep>();
            _steps[i].name = "ScrollStep_" + i;
            _steps[i].Initialize(i, OnScrollStepperStepClickedEventHandlerAndTrigger);
         }
      }

      public void UpdateSteps(int index, bool instant = false)
      {
         for (var i = 0; i < _steps.Length; i++)
         {
            _steps[i].SetEnabled(i == index, instant);
         }
      }
   }
}