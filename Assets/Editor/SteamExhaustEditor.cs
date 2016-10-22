using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SteamExhaustScript))]
public class SteamExhaustEditor : Editor {
        string[] _choices = new[] { "High Jump", "Create Platform" };


        public override void OnInspectorGUI()
        {
            // Draw the default inspector
            DrawDefaultInspector();
            SteamExhaustScript myTarget = (SteamExhaustScript)target;
            int _choiceIndex = myTarget.choiceIndex;
            _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);

            // Update the selected choice in the underlying object
            myTarget.choiceIndex = _choiceIndex;

            // Save the changes back to the object
            EditorUtility.SetDirty(target);
        }
}