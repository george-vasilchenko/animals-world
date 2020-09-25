using UnityEngine;

namespace MalenkiyApps.Misc
{
   public class PingPongSmoothScaleEffectBehaviour : MonoBehaviour
   {
      public Vector3 TargetScale;
      public float PhaseDuration = 1.0f;

      private Transform _transform;
      private Vector3 _originalScale;
      private float _time;
      private bool _isCountingUp;

      private void Start()
      {
         _transform = transform;
         _originalScale = _transform.localScale;
      }

      private void Update()
      {
         UpdateTime();
         UpdateScale();
      }

      private void UpdateScale()
      {
         _transform.localScale = Vector3.Lerp(_originalScale, TargetScale, _time);
      }

      private void UpdateTime()
      {
         _time = _isCountingUp
            ? _time + Time.deltaTime / PhaseDuration
            : _time - Time.deltaTime / PhaseDuration;

         if (_time >= 1)
         {
            _isCountingUp = false;
         }

         if (_time <= 0)
         {
            _isCountingUp = true;
         }
      }
   }
}