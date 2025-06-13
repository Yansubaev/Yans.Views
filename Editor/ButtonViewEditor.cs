using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Yans.UI.Views;

public class ButtonViewEditor
{
    [MenuItem("GameObject/UI/Button View", false, 10)]
    static void CreateButtonView(MenuCommand menuCommand)
    {
        // Create GameObject
        GameObject go = new GameObject("ButtonView");

        // Add Components - Add ButtonView first
        ButtonView buttonView = go.AddComponent<ButtonView>();
        Image image = go.AddComponent<Image>();
        Button button = go.AddComponent<Button>();

        // Configure Button
        button.targetGraphic = image;

        // Configure ButtonView fields
        SerializedObject so = new SerializedObject(buttonView);
        RectTransform rectTransform = go.GetComponent<RectTransform>(); // Get RectTransform
        so.FindProperty("_clickInterceptor").objectReferenceValue = button;
        so.FindProperty("_rectTransform").objectReferenceValue = rectTransform; // Assign RectTransform
        so.ApplyModifiedProperties();

        // Place in hierarchy
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);

        // Undo and Selection
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}
