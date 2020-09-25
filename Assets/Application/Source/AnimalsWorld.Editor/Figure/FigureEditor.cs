using UnityEditor;
using UnityEngine;

namespace AnimalsWorld
{
   [CustomEditor(typeof(Figure))]
   public class FigureEditor : Editor
   {
      private Figure _target;

      public void OnEnable() => _target = target as Figure;

      public override void OnInspectorGUI()
      {
         base.OnInspectorGUI();

         if (GUILayout.Button("Snap to anchor"))
         {
            _target.SnapToAnchor();
         }
      }
   }
}