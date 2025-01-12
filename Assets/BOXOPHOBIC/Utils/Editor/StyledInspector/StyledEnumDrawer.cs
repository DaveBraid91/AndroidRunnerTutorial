﻿// Cristian Pop - https://boxophobic.com/

using System.Collections.Generic;
using System.IO;
using BOXOPHOBIC.Utils.Scripts.StyledInspector;
using UnityEditor;
using UnityEngine;

namespace BOXOPHOBIC.Utils.Editor.StyledInspector
{
    [CustomPropertyDrawer(typeof(StyledEnum))]
    public class StyledEnumAttributeDrawer : PropertyDrawer
    {
        StyledEnum a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (StyledEnum)attribute;

            GUIStyle styleLabel = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true
            };

            if (Resources.Load<TextAsset>(a.file) != null)
            {
                var layersPath = AssetDatabase.GetAssetPath(Resources.Load<TextAsset>(a.file));

                StreamReader reader = new StreamReader(layersPath);

                a.options = reader.ReadLine();

                reader.Close();
            }

            string[] enumSplit = a.options.Split(char.Parse(" "));
            List<string> enumOptions = new List<string>(enumSplit.Length / 2);
            List<int> enumIndices = new List<int>(enumSplit.Length / 2);

            for (int i = 0; i < enumSplit.Length; i++)
            {
                if (i % 2 == 0)
                {
                    enumOptions.Add(enumSplit[i].Replace("_", " "));
                }
                else
                {
                    enumIndices.Add(int.Parse(enumSplit[i]));
                }
            }

            GUILayout.Space(a.top);

            int index = property.intValue;
            int realIndex = enumIndices[0];

            for (int i = 0; i < enumIndices.Count; i++)
            {
                if (enumIndices[i] == index)
                {
                    realIndex = i;
                }
            }

            if (a.display == "")
            {
                a.display = property.displayName;
            }

            realIndex = EditorGUILayout.Popup(a.display, realIndex, enumOptions.ToArray());

            //Debug Value
            //EditorGUILayout.LabelField(enumIndices[realIndex].ToString());

            property.intValue = enumIndices[realIndex];

            GUILayout.Space(a.down);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}
