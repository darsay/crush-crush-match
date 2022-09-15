#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Level))]
[CanEditMultipleObjects]
public class LevelInspector : Editor
{

    Level level;
    SerializedProperty IsPredefined;

    SerializedProperty DefinedBoard;

    private void OnEnable()
    {
        level = (Level)target;
        IsPredefined = serializedObject.FindProperty("IsBoardPreDefined");

        DefinedBoard = serializedObject.FindProperty("Board");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        base.OnInspectorGUI();

        level.IsBoardPreDefined = EditorGUILayout.Toggle("Is predefined", level.IsBoardPreDefined);

        if (IsPredefined.boolValue)
        {
            GenerateBoard();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void GenerateBoard()
    {
        int counter = 0;
        for (int i = 0; i < 9; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < 9; j++)
            {               
                DefinedBoard.GetArrayElementAtIndex(counter).enumValueIndex =
                    (int)(TilesTypes)EditorGUILayout.EnumPopup((TilesTypes)TilesTypes.
                    GetValues(typeof(TilesTypes)).GetValue(DefinedBoard.GetArrayElementAtIndex(counter).enumValueIndex));
                counter++;
            }
            GUILayout.EndHorizontal();
        }
    }
}

#endif
