using System;
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

        private static Type GetEnumType(SerializedProperty dict) =>
            Type.GetType(dict.FindPropertyRelative("enumTypeName").stringValue);

        private static string GetName(SerializedProperty entry, Type enumType) =>
            Enum.GetName(enumType, entry.FindPropertyRelative("enum").intValue);


        public override float GetPropertyHeight(SerializedProperty dict, GUIContent label)
        {
            var entryCount = GetEntryCount(dict);
            var entryHeight = EditorGUI.GetPropertyHeight(GetEntry(dict, 0).FindPropertyRelative("value"),true);
            
            return LabelHeight + (SpacingHeight + entryHeight) * entryCount;
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

            void DrawEntry(int index)
            {
                var entry = GetEntry(dict, index);
                var height = EditorGUI.GetPropertyHeight(entry.FindPropertyRelative("value"),true);

                var yOffset = LabelHeight +
                              SpacingHeight * (index + 1) +
                              height * index;

                void DrawEntryLabel()
                {
                    var name = GetName(entry, enumType);

                    var width = (position.width - EntryIndent) * 0.25f - SpacingWidth;
                    var rect = RelativeRect(EntryIndent, yOffset, width, height);

                    EditorGUI.LabelField(rect, name);
                }

                void DrawEntryValueField()
                {
                    var prop = entry.FindPropertyRelative("value");

                    var width = (position.width - EntryIndent) * 0.75f;
                    var x = EntryIndent + position.width * 0.25f;
                    var rect = RelativeRect(x, yOffset, width, height);

                    EditorGUI.PropertyField(rect, prop, GUIContent.none, true);
                }

                DrawEntryLabel();
                DrawEntryValueField();
            }

            DrawLabel();
            for (var i = 0; i < GetEntryCount(dict); i++)
                DrawEntry(i);
        }

    }

}