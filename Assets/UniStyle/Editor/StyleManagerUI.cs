using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Provides inspector options to configure global options for UniStyle.
/// </summary>
[CustomEditor(typeof(StyleManager))]
public class StyleManagerUI : Editor
{

    /// <summary>
    /// Draw inspector options for the StyleManager component and call coresponding functions when 
    /// an option has been selected. 
    /// </summary>
    public override void OnInspectorGUI()
    {

        //Draw refresh button to apply changed styles to all elements
        Texture icon = Resources.Load("refresh") as Texture;
        EditorGUILayout.LabelField("Update current Scene");
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button(icon, GUILayout.Width(150)))
            UniStyle.ActiveStyle.RefreshStyles();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        EditorGUILayout.LabelField("");

        //Draw Auto-Apply toggle to enable automatic apply component adding
        EditorGUI.BeginDisabledGroup(UniStyle.ActiveStyle.autoAttachStyleScripts);
        EditorGUILayout.BeginHorizontal();
        List<string> autoApplyOptions = new List<string>();
        autoApplyOptions.Add("Disabled");
        if (UniStyle.ActiveStyle.activeStyles != null && UniStyle.ActiveStyle.activeStyles.Count > 0)
            autoApplyOptions.AddRange(UniStyle.ActiveStyle.activeStyles.Select(x => x.name));
        EditorGUILayout.LabelField(new GUIContent("Auto Apply Style:", "Automatically adds the ApplyStyle script to any UI elements created. Warning: might be slow in large scenes. Disabled when currently not used."), GUILayout.Width(140));
        EditorGUI.BeginChangeCheck();
        UniStyle.ActiveStyle.selectedAutoStyle = EditorGUILayout.Popup(UniStyle.ActiveStyle.selectedAutoStyle, autoApplyOptions.ToArray());
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RegisterCompleteObjectUndo(target, "Not Available");
            UniStyle.ActiveStyle.AutoApplyChanged();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUI.EndDisabledGroup();

        //Draw Auto-Style toggle to enable automatic style component adding
        EditorGUI.BeginDisabledGroup(UniStyle.ActiveStyle.selectedAutoStyle!=0);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Auto Style Config:", "Automatically adds Style configurations to any UI elements created. Warning: might be slow in large scenes. Disabled when currently not used."), GUILayout.Width(140));
        EditorGUI.BeginChangeCheck();
        UniStyle.ActiveStyle.autoAttachStyleScripts = EditorGUILayout.Toggle(UniStyle.ActiveStyle.autoAttachStyleScripts);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RegisterCompleteObjectUndo(target, "Not Available");
            UniStyle.ActiveStyle.AutoStyleToggled();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUI.EndDisabledGroup();

        //Draw toggle to disable Textmesh Pro support
#if UniStyle_TMPPro
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Textmesh Pro support:", "Disable Textmesh Pro support for UniStyle."), GUILayout.Width(140));
        if (!EditorGUILayout.Toggle(true))
            UniStyle.ActiveStyle.ToggleTMP(false);
        EditorGUILayout.EndHorizontal();
#endif

        //Draw toggle to enable Textmesh Pro support
#if !UniStyle_TMPPro
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Textmesh Pro support:", "Enable Textmesh Pro support for UniStyle. Textmesh Pro has to be imported beforehand."), GUILayout.Width(140));
        if (EditorGUILayout.Toggle(false))
            UniStyle.ActiveStyle.ToggleTMP(true);
        EditorGUILayout.EndHorizontal();
#endif
        DrawDefaultInspector();
    }

    /// <summary>
    /// Add menu item to create a StyleManager in the current scene or show debug message
    /// when a style manager is already present.
    /// </summary>
    /// <param name="menuCommand"></param>
    [MenuItem("GameObject/UniStyle/Style Manager", false, 10)]
    static void CreateStyleManager(MenuCommand menuCommand)
    {
        if (null == UniStyle.ActiveStyle)
        {
            // Create a custom game object
            GameObject go = new GameObject("Style Manager");
            go.AddComponent<StyleManager>();
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
        else
        {
            Debug.Log("[UniStyle] Style Manager already exists!");
        }
    }

    /// <summary>
    /// Add menu item to create a custom style gameobject in the current scene.
    /// </summary>
    /// <param name="menuCommand"></param>
    [MenuItem("GameObject/UniStyle/Custom Style", false, 10)]
    static void CreateStyle(MenuCommand menuCommand)
    {
        // Create a custom game object
        GameObject go = new GameObject("Custom Style");
        go.AddComponent<Style>();
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }

}