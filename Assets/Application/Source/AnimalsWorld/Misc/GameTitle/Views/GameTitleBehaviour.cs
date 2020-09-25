using MalenkiyApps;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimalsWorld
{
   public class GameTitleBehaviour : MonoBehaviour
   {
      [SerializeField] private LetterBehaviour LetterPrefab = default;
      [SerializeField] private LetterSpawnPointBehaviour[] SpawnPoints = default;
      [SerializeField] private float _effectInterval = 5f;

      private ILetterService _letterService;
      private IEnumerable<ILetter> _letters;

      private readonly string _titleText = "animalsworld";
      private float _effectCounter;

      private void Awake()
      {
         Validation.Run(() => LetterPrefab != null, nameof(LetterPrefab), "Must not be null.");
         Validation.Run(() => SpawnPoints != null, nameof(SpawnPoints), "Must not be null.");
         Validation.Run(() => SpawnPoints.Any(), nameof(SpawnPoints), "Must not be empty.");

         _letterService = new LetterService();
      }

      private void Start()
      {
         _letters = CreateLetters();
         _letterService.PlaceLetters(SpawnPoints, _letters);

         _effectCounter = _effectInterval;
      }

      private void Update()
      {
         UpdateLetterEffect();
      }

      private void UpdateLetterEffect()
      {
         var counter = _effectCounter -= Time.deltaTime;
         if (counter <= 0)
         {
            var randomLetterIndex = Random.Range(0, _titleText.Length);
            _letterService.BubbleLetter(_letters.ElementAt(randomLetterIndex));
            _effectCounter = _effectInterval;
         }
      }

      private IEnumerable<ILetter> CreateLetters()
      {
         var letters = new List<ILetter>();

         foreach (var character in _titleText)
         {
            var letter = (ILetter)Instantiate(LetterPrefab, transform);
            letter.Set(character);
            letters.Add(letter);
         }

         return letters;
      }
   }
}