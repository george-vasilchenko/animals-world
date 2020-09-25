using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
   public class FallingObject : MonoBehaviour
   {
      public float MovementSpeed = 200.0f;
      public AnimationCurve MovementRandomnessCurve;

      private BoxCollider2D _collider;
      private Rigidbody2D _rigidbody2D;

      private void Awake()
      {
         _collider = GetComponent<BoxCollider2D>();
         _rigidbody2D = GetComponent<Rigidbody2D>();

         Validation.Run(() => _collider != null, "Collider", "Must not be null.");
         Validation.Run(() => _rigidbody2D != null, "Rigidbody2D", "Must not be null.");
      }

      private void Start()
      {
         _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

         MovementSpeed = MovementSpeed * MovementRandomnessCurve.Evaluate(Random.Range(0.5f, 1.0f));
         Destroy(gameObject, 20);
      }

      private void Update()
      {
         var newPostion = transform.position + Vector3.down * MovementSpeed * Time.deltaTime;

         _rigidbody2D.MovePosition(newPostion);
      }

      private void OnTriggerEnter2D(Component collision) => Destroy(gameObject);
   }
}