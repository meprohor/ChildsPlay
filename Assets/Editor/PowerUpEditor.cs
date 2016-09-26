using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PowerUpScript))]
public class PowerUpEditor : Editor {
        string[] _choices = new[] { "jump", "left", "kick" };


        public override void OnInspectorGUI()
        {
            // Draw the default inspector
            DrawDefaultInspector();
            PowerUpScript myTarget = (PowerUpScript)target;
            int _choiceIndex = myTarget.choiceIndex;
            _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);

            // Update the selected choice in the underlying object
            myTarget.choiceIndex = _choiceIndex;

            // Save the changes back to the object
            EditorUtility.SetDirty(target);
        }
}
