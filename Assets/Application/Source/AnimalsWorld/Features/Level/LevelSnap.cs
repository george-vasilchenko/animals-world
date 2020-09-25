using MalenkiyApps;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class LevelSnap : Level
   {
      public Figure[] Figures;

      protected override void OnLevelEnabled()
      {
         foreach (var element in Figures)
         {
            element.OnSnap += OnSnapElement;
         }
      }

      protected override void OnLevelDisabled()
      {
         foreach (var element in Figures)
         {
            element.OnSnap -= OnSnapElement;
            element.ResetElement();
         }
      }

      public override void Awake()
      {
         base.Awake();

         Validation.Run(() => Figures != null, "Figures", "Must not be null.");
         Validation.Run(() => Figures.Any(), "Figures", "Must not be empty.");
      }

      public virtual void Start()
      {
         LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

         foreach (var element in Figures)
         {
            element.SnapToAnchor();
         }
      }

      private void OnSnapElement(object sender, OnFigureSnapEventArgs args)
      {
         if (Figures.All(e => e.IsSnapped))
         {
            StartCoroutine(CompleteLevel());
         }
      }
   }
}