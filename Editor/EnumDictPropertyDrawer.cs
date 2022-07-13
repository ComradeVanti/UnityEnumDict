using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ComradeVanti.EnumDict
{

    [CustomPropertyDrawer(typeof(EnumDict<,>))]
    public class EnumDictPropertyDrawer : PropertyDrawer
    {

        private const int LabelHeight = 20;
        private const int SpacingHeight = 2;
        private const int EntryIndent = 5;
        private const int SpacingWidth = 5;


        private static int GetEntryCount(SerializedProperty dict) =>
            dict.FindPropertyRelative("entries").arraySize;

        private static SerializedProperty GetEntry(SerializedProperty dict, int index) =>
            dict.FindPropertyRelative("entries")
                .GetArrayElementAtIndex(index);

        private static SerializedProperty[] GetEntries(SerializedProperty dict)
        {
            var count = GetEntryCount(dict);
            return Enumerable.Range(0, count)
                             .Select(i => GetEntry(dict, i))
                             .ToArray();
        }

        private static Type GetEnumType(SerializedProperty dict) =>
            Type.GetType(dict.FindPropertyRelative("enumTypeName").stringValue);

        private static string GetName(SerializedProperty entry, Type enumType) =>
            Enum.GetName(enumType, entry.FindPropertyRelative("enum").intValue);

        private static float GetHeight(SerializedProperty entry)
        {
            var prop =  entry.FindPropertyRelative("value");
            return  EditorGUI.GetPropertyHeight(prop, true);
        }


        public override float GetPropertyHeight(SerializedProperty dict, GUIContent label)
        {
            var entryCount = GetEntryCount(dict);
            var entries = GetEntries(dict);
            var entryHeightSum = entries.Select(GetHeight).Sum();

            return LabelHeight + SpacingHeight * entryCount + entryHeightSum;
        }

        public override void OnGUI(Rect position, SerializedProperty dict, GUIContent label)
        {
            var enumType = GetEnumType(dict);

            Rect RelativeRect(float dx, float dy, float w, float h) =>
                new Rect(position.x + dx, position.y + dy, w, h);

            void DrawLabel()
            {
                var rect = RelativeRect(0, 0, position.width, LabelHeight);
                EditorGUI.LabelField(rect, label);
            }

            float DrawEntry(float y, int index)
            {
                var entry = GetEntry(dict, index);
                var valueProp = entry.FindPropertyRelative("value");
                var height = EditorGUI.GetPropertyHeight(valueProp, true);

                var rect = RelativeRect(EntryIndent, y, position.width - EntryIndent, height);
                var name = GetName(entry, enumType);
                EditorGUI.PropertyField(rect, valueProp, new GUIContent(name), true);

                return y + height + SpacingHeight;
            }

            DrawLabel();
            var y = (float)LabelHeight + SpacingHeight;
            for (var i = 0; i < GetEntryCount(dict); i++)
                y = DrawEntry(y, i);
        }

    }

}