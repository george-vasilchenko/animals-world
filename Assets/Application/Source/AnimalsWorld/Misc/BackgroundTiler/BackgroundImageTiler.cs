using System;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class BackgroundImageTiler : MonoBehaviour
   {
      public Canvas Canvas;
      public Image BackgroundTileImage;
      private Image[] _tiles;

      public void Start()
      {
         if (_tiles != null && _tiles.Length > 0)
         {
            return;
         }

         var imageSize = BackgroundTileImage.rectTransform.rect.size;

         if ((int)imageSize.x != (int)imageSize.y)
         {
            throw new Exception("Background tile image size must be POT.");
         }

         var imageSizeScaled = (imageSize.x * Canvas.scaleFactor);
         var amountHorizontal = Mathf.CeilToInt(Screen.width / imageSizeScaled);
         var amountVertical = Mathf.CeilToInt(Screen.height / imageSizeScaled);

         _tiles = new Image[amountHorizontal * amountVertical];

         var index = 0;

         for (var x = 0; x < amountHorizontal; x++)
         {
            for (var y = 0; y < amountVertical; y++)
            {
               var instance = Instantiate(BackgroundTileImage, transform);
               instance.rectTransform.anchoredPosition = new Vector3(x * imageSize.x, y * imageSize.y);

               _tiles[index++] = instance;
            }
         }
      }
   }
}