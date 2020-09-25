using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MalenkiyApps
{
   [CustomEditor(typeof(LocalizationCsvCreator))]
   public class LocalizationCsvManagerEditor : Editor
   {
      private LocalizationCsvCreator _localizationCsvCreator;
      private SerializedObject _serializedObject;
      private SerializedProperty _localizationCollectionSerializedProperty;

      private string _newEntryKey = "";
      private SystemLanguage _systemLanguage = SystemLanguage.Unknown;

      public void OnEnable()
      {
         _localizationCsvCreator = (LocalizationCsvCreator)target;
         _serializedObject = new SerializedObject(target);
         _localizationCollectionSerializedProperty = _serializedObject.FindProperty("LocalizationCollection");

         _localizationCsvCreator.Load();
      }

      public override void OnInspectorGUI()
      {
         _serializedObject.Update();

         DrawEditor();

         _serializedObject.ApplyModifiedProperties();
      }

      private void DrawEditor()
      {
         EditorGUILayout.BeginVertical("Box", GUILayout.MaxWidth(900));

         if (_localizationCsvCreator.LocalizationCollection?.Columns == null)
         {
            DrawCreateCollectionControls();
         }
         else
         {
            DrawTable();
            EditorGUILayout.Space();

            DrawColumnControlButtons();
            EditorGUILayout.Space();

            DrawEntryControlButtons();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            DrawSaveButton();
         }

         EditorGUILayout.EndVertical();
      }

      private void DrawCreateCollectionControls()
      {
         GUI.color = Color.green;
         if (GUILayout.Button("Create Collection"))
         {
            if (_localizationCsvCreator.CreateCollection())
            {
               AssetDatabase.Refresh();
               _localizationCsvCreator.Load();
               EditorUtility.SetDirty(target);
            }
         }
      }

      private void DrawTable()
      {
         EditorGUILayout.LabelField("Translations");
         EditorGUILayout.BeginHorizontal();

         var columnsListProperty = _localizationCollectionSerializedProperty.FindPropertyRelative("Columns");
         for (var i = 0; i < columnsListProperty.arraySize; i++)
         {
            var columnProperty = columnsListProperty.GetArrayElementAtIndex(i);

            DrawColumn(columnProperty);
         }

         EditorGUILayout.EndHorizontal();
      }

      private void DrawColumn(SerializedProperty columnProperty)
      {
         EditorGUILayout.BeginVertical("Box", GUILayout.Width(100), GUILayout.MaxWidth(400), GUILayout.ExpandWidth(true));

         var columnTitleProperty = columnProperty.FindPropertyRelative("Title");
         var columnElementsProperty = columnProperty.FindPropertyRelative("Entries");

         EditorGUILayout.LabelField(columnTitleProperty.stringValue, GUILayout.Width(100));

         for (var i = 0; i < columnElementsProperty.arraySize; i++)
         {
            var columnElementProperty = columnElementsProperty.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(columnElementProperty, new GUIContent(""));
         }

         EditorGUILayout.EndVertical();
      }

      private void DrawEntryControlButtons()
      {
         EditorGUILayout.BeginVertical("Box");

         _newEntryKey = EditorGUILayout.TextField("New entry key", _newEntryKey);

         EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(60));

         GUI.enabled = !string.IsNullOrWhiteSpace(_newEntryKey);

         if (GUILayout.Button("Add"))
         {
            if (_localizationCsvCreator.AddEntry(_newEntryKey))
            {
               AssetDatabase.Refresh();
               _localizationCsvCreator.Load();
               EditorUtility.SetDirty(target);
            }
         }

         if (GUILayout.Button("Remove"))
         {
            if (_localizationCsvCreator.RemoveEntry(_newEntryKey))
            {
               AssetDatabase.Refresh();
               _localizationCsvCreator.Load();
               EditorUtility.SetDirty(target);
            }
         }

         GUI.enabled = true;
         EditorGUILayout.EndHorizontal();
         EditorGUILayout.EndVertical();
      }

      private void DrawColumnControlButtons()
      {
         EditorGUILayout.BeginVertical("Box");
         _systemLanguage = (SystemLanguage)EditorGUILayout.EnumPopup("New language title", _systemLanguage);

         EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(60));
         GUI.enabled = _systemLanguage != SystemLanguage.Unknown;

         if (GUILayout.Button("Add"))
         {
            var newLanguageTitle = Enum.GetName(typeof(SystemLanguage), _systemLanguage);

            if (_localizationCsvCreator.AddLanguage(newLanguageTitle))
            {
               AssetDatabase.Refresh();
               _localizationCsvCreator.Load();
               EditorUtility.SetDirty(target);
            }
         }

         if (GUILayout.Button("Remove"))
         {
            var newLanguageTitle = Enum.GetName(typeof(SystemLanguage), _systemLanguage);

            if (_localizationCsvCreator.RemoveLanguage(newLanguageTitle))
            {
               AssetDatabase.Refresh();
               _localizationCsvCreator.Load();
               EditorUtility.SetDirty(target);
            }
         }

         GUI.enabled = true;
         EditorGUILayout.EndHorizontal();
         EditorGUILayout.EndVertical();
      }

      private void DrawSaveButton()
      {
         if (GUILayout.Button("Save"))
         {
            _localizationCsvCreator.Save();
         }
      }

      [MenuItem("Assets/Create/Localization Csv Manager Editor")]
      public static void CreateAssetFromMenu()
      {
         const string name = "LocalizationCsvManagerEditor";
         const string path = "Assets/Resources";
         var existingAssets = AssetDatabase.FindAssets(name, new[] { path });

         if (existingAssets.Length > 0)
         {
            Debug.Log("Localization Csv Manager Editor already exists.");

            EditorUtility.FocusProjectWindow(); Selection.activeObject =
               AssetDatabase.LoadAssetAtPath(path, typeof(LocalizationCsvCreator));
         }
         else
         {
            var asset = CreateInstance<LocalizationCsvCreator>();

            AssetDatabase.CreateAsset(asset, Path.Combine(path, name + ".asset")); AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
         }
      }
   }
}