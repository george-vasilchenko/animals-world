using MalenkiyApps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AnimalsWorld
{
   [RequireComponent(typeof(Image))]
   public class LetterBehaviour : MonoBehaviour, ILetter
   {
      [SerializeField]
      private List<Sprite> _letterSprites = default;

      [Header("Appear Effect")]
      [SerializeField] private float _appearEffectDuration = 2f;

      [SerializeField] private AnimationCurve _appearEffectCurve = default;
      [SerializeField] private float _appearEffectDelayMax = 0.5f;

      [Header("Bubble Effect")]
      [SerializeField] private float _bubbleEffectDuration = 0.5f;

      [SerializeField] private AnimationCurve _bubbleEffectCurve = default;

      private Image _imageComponent;
      private Transform _transform;

      private bool _isAppearEffectTriggered;
      private bool _isBubbleEffectFinished;

      public void TriggerAppearEffect()
      {
         if (!_isAppearEffectTriggered)
         {
            StartCoroutine(PlayAppearEffect());
            _isAppearEffectTriggered = true;
         }
      }

      public void TriggerBubbleEffect()
      {
         if (_isBubbleEffectFinished)
         {
            _isBubbleEffectFinished = false;
            StartCoroutine(PlayBubbleEffect());
         }
      }

      public void Set(char letter)
      {
         var targetSprite =
            _letterSprites.FirstOrDefault(o => string.Equals(o.name, $"Letter_{letter}", StringComparison.InvariantCultureIgnoreCase));

         _imageComponent.sprite = targetSprite;
      }

      public void SetLocation(float xLocation, float yLocation)
      {
         _transform.position = new Vector2(xLocation, yLocation);
      }

      private void Awake()
      {
         Validation.Run(() => _letterSprites != null, nameof(_letterSprites), "Must not be null.");
         Validation.Run(() => _letterSprites.Any(), nameof(_letterSprites), "Must not be empty.");

         _imageComponent = GetComponent<Image>();
         Validation.Run(() => _imageComponent != null, nameof(_imageComponent), "Must not be null.");

         _transform = transform;
         _transform.localScale = Vector3.zero;
      }

      private IEnumerator PlayBubbleEffect()
      {
         float counter = 0;
         while ((counter += Time.deltaTime) < _bubbleEffectDuration)
         {
            var lerpTime = Mapper.MapRange(0, _bubbleEffectDuration, 0, 1, counter);
            var valueOnCurve = _bubbleEffectCurve.Evaluate(lerpTime);

            _transform.localScale = Vector3.one * valueOnCurve;
            yield return null;
         }

         _isBubbleEffectFinished = true;
      }

      private IEnumerator PlayAppearEffect()
      {
         float counter = 0;
         var delay = Random.Range(0f, _appearEffectDelayMax);

         yield return new WaitForSeconds(delay);

         while ((counter += Time.deltaTime) < _appearEffectDuration)
         {
            var lerpTime = Mapper.MapRange(0, _appearEffectDuration, 0, 1, counter);
            var valueOnCurve = _appearEffectCurve.Evaluate(lerpTime);

            _transform.localScale = Vector3.one * valueOnCurve;
            yield return null;
         }

         _isBubbleEffectFinished = true;
      }
   }
}