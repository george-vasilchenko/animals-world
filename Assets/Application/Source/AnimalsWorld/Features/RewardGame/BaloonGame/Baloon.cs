using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class Baloon : MonoBehaviour, IPointerClickHandler
   {
      public float MovementSpeed = 1.75f;
      public AnimationCurve MovementRandomnessCurve;
      public Sprite[] AnimationFrames;

      private bool _isClickedSwitch;
      private Animation _popAnimation;
      private float _scaleFactor;
      private RectTransform _rectTransform;
      private Image _image;

      public void Awake()
      {
         _popAnimation = GetComponent<Animation>() ?? gameObject.AddComponent<Animation>();
         _scaleFactor = GameManager.Instance.UiManager.ScaleFactor;
         _image = GetComponent<Image>();
         _rectTransform = GetComponent<RectTransform>();
      }

      public void Start()
      {
         _popAnimation.clip = GameManager.Instance.AnimationManager.PopAnimationClip;

         _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x * _scaleFactor, _rectTransform.sizeDelta.y * _scaleFactor);
         MovementSpeed = MovementSpeed * MovementRandomnessCurve.Evaluate(Random.Range(0.5f, 1.0f));
      }

      public void UpdateImage(int index)
      {
         _image.sprite = AnimationFrames[index];
      }

      public void OnPointerClick(PointerEventData eventData)
      {
         if (_isClickedSwitch)
         {
            return;
         }

         _isClickedSwitch = true;
         GameManager.Instance.SoundManager.PlaySnapSound();
         PopBaloon();
      }

      private void PopBaloon()
      {
         GameManager.Instance.SoundManager.PlayPopSound();
         _popAnimation.Play();
         Destroy(gameObject, 0.2f);
      }

      public void Update() => transform.position += Vector3.up * MovementSpeed * GameManager.Instance.UiManager.ScaleFactor;
   }
}