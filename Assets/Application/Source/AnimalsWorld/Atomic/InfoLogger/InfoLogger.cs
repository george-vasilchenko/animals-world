using MalenkiyApps;
using MalenkiyApps.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimalsWorld
{
   public class InfoLogger : MonoBehaviour, IInfoLogger
   {
      public MessageBubble MessageBubblePrefab;

      private Queue<ILogMessage> _messageQueue;
      private MessageBubble _currentBubble;

      public void Log(string message)
      {
         Log(new InfoMessage(message));
      }

      public void Log(ILogMessage message)
      {
         var array = _messageQueue.ToArray();

         if (array.Any(o => o.Content == message.Content))
         {
            return;
         }

         if (_currentBubble != null && _currentBubble.BubbleText.text == message.Content)
         {
            return;
         }

         _messageQueue.Enqueue(message);
      }

      public void Awake()
      {
         Validation.Run(() => MessageBubblePrefab != null, "MessageBubblePrefab", "Must not be null.");

         _messageQueue = new Queue<ILogMessage>();
      }

      private void Update()
      {
         if (_messageQueue.Count > 0)
         {
            if (_currentBubble == null)
            {
               var message = _messageQueue.Dequeue();
               _currentBubble = CreateBubble();
               _currentBubble.Initialize(message.Content);
            }
         }
      }

      private MessageBubble CreateBubble()
      {
         var instance = Instantiate(MessageBubblePrefab, Vector3.zero, Quaternion.identity);
         instance.transform.SetParent(transform, false);
         return instance;
      }
   }
}