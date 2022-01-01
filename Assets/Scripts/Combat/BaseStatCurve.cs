using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mahou.Combat
{

    [Serializable]
    public struct BaseStatCurve
    {
        public static readonly BaseStatCurve One = new BaseStatCurve(1f, 1f, 1f);

        private static float SCALING_MULT = 1.2f;
        private static float SCALED_L50 = Mathf.Pow(50 - 1, SCALING_MULT);
        private static float SCALED_L100 = Mathf.Pow(100 - 1, SCALING_MULT);

        public float valForL1;
        public float valForL50;
        public float valForL100;

        public BaseStatCurve(float l1, float l50, float l100)
        {
            valForL1 = l1;
            valForL50 = l50;
            valForL100 = l100;
        }

        public float Compute(int level)
        {
            float scaledLevel = Mathf.Pow(level - 1, SCALING_MULT);
            if (scaledLevel <= SCALED_L50)
            {
                return valForL1 + (valForL50 - valForL1) * scaledLevel / SCALED_L50;
            }
            else if (scaledLevel <= SCALED_L100)
            {
                return valForL50 + (valForL100 - valForL50) * (scaledLevel - SCALED_L50) / (SCALED_L100 - SCALED_L50);
            }
            else
            {
                return valForL100;
            }
        }

#if UNITY_EDITOR
        public void DebugShowLevels()
        {
            var str = "";
            var str2 = "";
            for (int i = 1; i <= 100; i++)
            {
                str += "Lv." + i + ": " + Math.Round(Compute(i)) + "\n";
                str2 += (i != 1 ? ", ": "") + Compute(i);
            }
            Debug.Log("Levels:\n" + "[" + str2 + "]\n" + str);
        }
#endif
    }


    [CustomPropertyDrawer(typeof(BaseStatCurve))]
    public class BaseStatCurveDrawer : PropertyDrawer
    {
        private static readonly GUIContent[] s_ValForLabels = { new GUIContent("L1"), new GUIContent("L50"), new GUIContent("L100") };

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * (EditorGUIUtility.wideMode ? 1 : 2);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var l1 = property.FindPropertyRelative(nameof(BaseStatCurve.valForL1));
            var l50 = property.FindPropertyRelative(nameof(BaseStatCurve.valForL50));
            var l100 = property.FindPropertyRelative(nameof(BaseStatCurve.valForL100));

            EditorGUI.BeginProperty(position, label, property);
            {
                if (EditorGUI.DropdownButton(new Rect(position) { width = 20 }, new GUIContent("?"), FocusType.Passive))
                {
                    var tst = new BaseStatCurve();
                    tst.valForL1 = l1.floatValue;
                    tst.valForL50 = l50.floatValue;
                    tst.valForL100 = l100.floatValue;
                    tst.DebugShowLevels();
                }

                float savedLabelWidth = EditorGUIUtility.labelWidth;
                position = EditorGUI.PrefixLabel(position, label);
                EditorGUIUtility.labelWidth = 35;
                var savedIndentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                var lspacing = 5;
                var lw = position.width / 3 - lspacing / 2;
                var lpos = new Rect(position.x, position.y, lw, position.height);
                EditorGUI.PropertyField(lpos, l1, s_ValForLabels[0]);
                lpos.x += lw + lspacing;
                EditorGUI.PropertyField(lpos, l50, s_ValForLabels[1]);
                lpos.x += lw + lspacing;
                EditorGUI.PropertyField(lpos, l100, s_ValForLabels[2]);
                EditorGUIUtility.labelWidth = savedLabelWidth;
                EditorGUI.indentLevel = savedIndentLevel;
            }
            EditorGUI.EndProperty();
        }
    }

}
