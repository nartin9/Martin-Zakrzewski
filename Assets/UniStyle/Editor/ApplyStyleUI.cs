using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides inspector options to select and apply a style to the current UI element.
/// </summary>
[CustomEditor(typeof(ApplyStyle))]
public class ApplyStyleUI : Editor
{

    private bool elementChanged = false;

    /// <summary>
    /// Register Redraw function to undo actions.
    /// </summary>
    ApplyStyleUI()
    {
        Undo.undoRedoPerformed += Redraw;
    }

    /// <summary>
    /// Un-Register Redraw function from undo actions.
    /// </summary>
    private void OnDestroy()
    {
        Undo.undoRedoPerformed -= Redraw;
    }

    /// <summary>
    /// Redraw the inspector when undo-operation was performed. 
    /// </summary>
    public void Redraw()
    {
        Repaint();
        elementChanged = false;
    }

    /// <summary>
    /// Draw inspector options for the ApplyStyle component and call coresponding functions when 
    /// an option has been selected. Set default related objects to current UI element.
    /// Also provide undo functionality for changes.
    /// </summary>
    public override void OnInspectorGUI()
    {
        //Draw dropdown for style selection
        ApplyStyle myScript = (ApplyStyle) target;
        EditorGUI.BeginChangeCheck();
        string[] styleOptions = new string[UniStyle.ActiveStyle.activeStyles.Count];
        for (int i = 0; i < styleOptions.Length; i++)
            styleOptions[i] = UniStyle.ActiveStyle.activeStyles[i].name;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Style:", GUILayout.Width(80));
        myScript.selectedStyleIndex = EditorGUILayout.Popup(myScript.selectedStyleIndex, styleOptions);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        //Draw dropdown for category selection
        EditorGUILayout.BeginHorizontal();
        string[] categoryOptions = new string[] { "Text", "Image", "Button", "Toggle", "InputField", "Slider", "ScrollView", "ScrollBar", "Dropdown" };
        EditorGUILayout.LabelField("Category:", GUILayout.Width(80));
        myScript.selectedCategoryIndex = EditorGUILayout.Popup(myScript.selectedCategoryIndex, categoryOptions);
        if (EditorGUI.EndChangeCheck())
            myScript.ChangeCategory(myScript.selectedCategoryIndex);
        EditorGUILayout.EndHorizontal();

        //Draw dropdown for element selection
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginChangeCheck();
        string[] elementOptions = UniStyle.ActiveStyle.GetElementNamesFromCategory(myScript.selectedStyleIndex, myScript.selectedCategoryIndex);
        EditorGUILayout.LabelField("Element:", GUILayout.Width(80));
        int saveSelection = EditorGUILayout.Popup(myScript.selectedElementIndex, elementOptions);
        EditorGUILayout.EndHorizontal();

        //If element changed create undo lists and apply selected style
        if (EditorGUI.EndChangeCheck())
        {

            List<Object> toRecord = new List<Object>();
            foreach (Image toAdd in myScript.GetComponentsInChildren<Image>()) toRecord.Add(toAdd);
            foreach (Text toAdd in myScript.GetComponentsInChildren<Text>()) toRecord.Add(toAdd);
            foreach (InputField toAdd in myScript.GetComponentsInChildren<InputField>()) toRecord.Add(toAdd);

            foreach (Slider toAdd in myScript.GetComponentsInChildren<Slider>()) toRecord.Add(toAdd);
            foreach (RectTransform toAdd in myScript.GetComponentsInChildren<RectTransform>()) toRecord.Add(toAdd);
#if UniStyle_TMPPro
            foreach (TMPro.TMP_InputField toAdd in myScript.GetComponentsInChildren<TMPro.TMP_InputField>()) toRecord.Add(toAdd);
            foreach (TMPro.TextMeshProUGUI toAdd in myScript.GetComponentsInChildren<TMPro.TextMeshProUGUI>()) toRecord.Add(toAdd);
#endif

            Undo.RecordObjects(toRecord.ToArray(), "ApplyStyle");
            myScript.selectedElementIndex = saveSelection;
            UniStyle.ActiveStyle.ApplyStyle(myScript);

            elementChanged = true;
            myScript.GetComponentInParent<Canvas>().transform.position = new Vector3(0, 0, 0);
        }
        //Add object fields for ui element sub-components. Assign default objects to fields.
        switch (myScript.selectedCategoryIndex)
        {
            case UniStyle.CATEGORY_BUTTON:
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[0] = (Image) EditorGUILayout.ObjectField("Image: ", myScript.targetImages[0], typeof(Image), true);

                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetTexts[0] = (Text) EditorGUILayout.ObjectField("Text: ", myScript.targetTexts[0], typeof(Text), true);
#if UniStyle_TMPPro
                if (myScript.targetTMPTexts.Count == 0) myScript.targetTMPTexts.Add(null);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetTMPTexts[0] = (TMPro.TextMeshProUGUI) EditorGUILayout.ObjectField("Text (TMP): ", myScript.targetTMPTexts[0], typeof(TMPro.TextMeshProUGUI), true);
#endif
                break;

            case UniStyle.CATEGORY_TOGGLE:
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[0] = (Image) EditorGUILayout.ObjectField("Background: ", myScript.targetImages[0], typeof(Image), true);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[1] = (Image) EditorGUILayout.ObjectField("Checkmark: ", myScript.targetImages[1], typeof(Image), true);

                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetTexts[0] = (Text) EditorGUILayout.ObjectField("Label: ", myScript.targetTexts[0], typeof(Text), true);
#if UniStyle_TMPPro
                if (myScript.targetTMPTexts.Count == 0) myScript.targetTMPTexts.Add(null);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetTMPTexts[0] = (TMPro.TextMeshProUGUI) EditorGUILayout.ObjectField("Label (TMP): ", myScript.targetTMPTexts[0], typeof(TMPro.TextMeshProUGUI), true);
#endif

                break;
            case UniStyle.CATEGORY_INPUTFIELD:
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[0] = (Image) EditorGUILayout.ObjectField("Background: ", myScript.targetImages[0], typeof(Image), true);

                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetTexts[0] = (Text) EditorGUILayout.ObjectField("Placeholder: ", myScript.targetTexts[0], typeof(Text), true);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetTexts[1] = (Text) EditorGUILayout.ObjectField("Textfield: ", myScript.targetTexts[1], typeof(Text), true);

#if UniStyle_TMPPro
                if (myScript.targetTMPTexts.Count == 0) myScript.targetTMPTexts.Add(null);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetTMPTexts[0] = (TMPro.TextMeshProUGUI) EditorGUILayout.ObjectField("Placeholder (TMP): ", myScript.targetTMPTexts[0], typeof(TMPro.TextMeshProUGUI), true);
                if (myScript.targetTMPTexts.Count <= 1) myScript.targetTMPTexts.Add(null);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetTMPTexts[1] = (TMPro.TextMeshProUGUI) EditorGUILayout.ObjectField("Textfield (TMP): ", myScript.targetTMPTexts[1], typeof(TMPro.TextMeshProUGUI), true);
#endif
                break;
            case UniStyle.CATEGORY_SLIDER:
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[0] = (Image) EditorGUILayout.ObjectField("Background: ", myScript.targetImages[0], typeof(Image), true);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[1] = (Image) EditorGUILayout.ObjectField("Fill: ", myScript.targetImages[1], typeof(Image), true);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[2] = (Image) EditorGUILayout.ObjectField("Handle: ", myScript.targetImages[2], typeof(Image), true);
                break;
            case UniStyle.CATEGORY_SCROLLVIEW:
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[0] = (Image) EditorGUILayout.ObjectField("Background: ", myScript.targetImages[0], typeof(Image), true);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[1] = (Image) EditorGUILayout.ObjectField("View Port: ", myScript.targetImages[1], typeof(Image), true);
                break;
            case UniStyle.CATEGORY_SCROLLBAR:
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[0] = (Image) EditorGUILayout.ObjectField("Background: ", myScript.targetImages[0], typeof(Image), true);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[1] = (Image) EditorGUILayout.ObjectField("Handle: ", myScript.targetImages[1], typeof(Image), true);
                break;
            case UniStyle.CATEGORY_DROPDOWN:
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[0] = (Image) EditorGUILayout.ObjectField("Background: ", myScript.targetImages[0], typeof(Image), true);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[1] = (Image) EditorGUILayout.ObjectField("Arrow: ", myScript.targetImages[1], typeof(Image), true);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[2] = (Image) EditorGUILayout.ObjectField("Caption Image: ", myScript.targetImages[2], typeof(Image), true);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[3] = (Image) EditorGUILayout.ObjectField("Item Image: ", myScript.targetImages[3], typeof(Image), true);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[4] = (Image) EditorGUILayout.ObjectField("Item Background: ", myScript.targetImages[4], typeof(Image), true);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetImages[5] = (Image) EditorGUILayout.ObjectField("Item Checkmark: ", myScript.targetImages[5], typeof(Image), true);

                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetTexts[0] = (Text) EditorGUILayout.ObjectField("Caption Text: ", myScript.targetTexts[0], typeof(Text), true);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetTexts[1] = (Text) EditorGUILayout.ObjectField("Item Text: ", myScript.targetTexts[1], typeof(Text), true);

#if UniStyle_TMPPro
                if (myScript.targetTMPTexts.Count == 0) myScript.targetTMPTexts.Add(null);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetTMPTexts[0] = (TMPro.TextMeshProUGUI) EditorGUILayout.ObjectField("Caption Text (TMP): ", myScript.targetTMPTexts[0], typeof(TMPro.TextMeshProUGUI), true);
                if (myScript.targetTMPTexts.Count <= 1) myScript.targetTMPTexts.Add(null);
                if (!elementChanged) Undo.RecordObject(myScript, "Inspector");
                myScript.targetTMPTexts[1] = (TMPro.TextMeshProUGUI) EditorGUILayout.ObjectField("Item Text (TMP): ", myScript.targetTMPTexts[1], typeof(TMPro.TextMeshProUGUI), true);
#endif
                break;
        }
    }
}