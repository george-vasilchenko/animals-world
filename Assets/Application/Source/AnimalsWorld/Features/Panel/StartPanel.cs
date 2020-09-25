using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class StartPanel : Panel
   {
      [SerializeField]
      private Animator _titleAnimator = default;

      public override void ApplyData(object data)
      {
         throw new System.NotImplementedException();
      }

      private void Awake()
      {
         Validation.Run(() => _titleAnimator != null, nameof(_titleAnimator), "Must not be null.");
      }

      private void Start()
      {
         _titleAnimator.Play("DropTitle");
      }
   }
}