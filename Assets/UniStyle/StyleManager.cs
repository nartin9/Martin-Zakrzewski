using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stores style configuration for the current scene. Provides application of styles throughout
/// the scene.
/// </summary>
[ExecuteInEditMode]
public class StyleManager : MonoBehaviour
{

    [HideInInspector]
    public bool autoAttachStyleScripts;

    [HideInInspector]
    public int selectedAutoStyle = 0;

    public List<GameObject> activeStyles;

    private GameObject canvasObj;
    private AutoApplyAdd currentApply;
    private AutoStyleAdd currentStyle;

    /// <summary>
    /// Defines scripting symbol for Textmesh Pro support.
    /// </summary>
    public static readonly string[] Symbols = new string[]
    {
        "UniStyle_TMPPro"
    };

    /// <summary>
    /// Set StyleManager in the UniStyle static for global access.
    /// </summary>
    /// <see cref="UniStyle"/>
    private StyleManager()
    {
        if (null != UniStyle.ActiveStyle)
            DestroyImmediate(UniStyle.ActiveStyle);
        UniStyle.ActiveStyle = this;
    }

    /// <summary>
    /// Disable or enable Textmesh Pro support by adding scripting define symbol to the editor.
    /// </summary>
    /// <param name="enable">Enable Textmesh Pro support?</param>
    public void ToggleTMP(bool enable)
    {
#if UNITY_EDITOR
        if (enable)
        {
            Debug.Log("[UniStyle] Textmesh Pro support enabled.");
            string definesString = UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup);
            List<string> allDefines = definesString.Split(';').ToList();
            allDefines.AddRange(Symbols.Except(allDefines));
            UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(
                UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join(";", allDefines.ToArray()));
        }
        else
        {
            Debug.Log("[UniStyle] Textmesh Pro support disabled.");
            string definesString = UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup);
            List<string> allDefines = definesString.Split(';').ToList();
            allDefines.Remove(Symbols[0]);
            UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(
                UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join(";", allDefines.ToArray()));
        }
#endif
    }

    /// <summary>
    /// Check for active auto scripts and show debug warning.
    /// </summary>
    private void Awake()
    {
        if (selectedAutoStyle != 0)
            Debug.Log("[UniStyle] Auto Apply script is active. Turn off when you're finished creating UI elements.");
        if (autoAttachStyleScripts)
            Debug.Log("[UniStyle] Auto Style script is active. Turn off when you're finished creating UI style references.");
    }

    /// <summary>
    /// Destroy auto apply scripts when changing the scene.
    /// </summary>
#if UNITY_EDITOR
    private void OnDestroy()
    {
        autoAttachStyleScripts = false;
        if (null != this.GetComponent<AutoApplyAdd>())
            this.GetComponent<AutoApplyAdd>().OnDestroy();
        if (null != this.GetComponent<AutoStyleAdd>())
            this.GetComponent<AutoStyleAdd>().OnDestroy();
    }
#endif

    /// <summary>
    /// Apply any given style configuration to an UI element. All information neccessary (configuration,
    /// reference and target object) are stored available from the ApplyStyle component.
    /// </summary>
    /// <param name="selectedStyle">Information on the styling process.</param>
    /// <see cref="global::ApplyStyle"/>

    public void ApplyStyle(ApplyStyle selectedStyle)
    {
        int elementIndex = selectedStyle.selectedElementIndex;
        int styleIndex = selectedStyle.selectedStyleIndex;
        int category = selectedStyle.selectedCategoryIndex;
        GameObject toStyle = selectedStyle.gameObject;
        if (elementIndex == 0)
        {
            return;
        }
        else
        {
            elementIndex--;
        }
        GameObject reference = null;
        if (styleIndex > activeStyles.Count)
            return;
        List<GameObject> list = null;

        //Get all elements from selected style and category
        switch (category)
        {
            case UniStyle.CATEGORY_TEXT:
                list = activeStyles[styleIndex].GetComponent<Style>().texts;
                break;
            case UniStyle.CATEGORY_IMAGE:
                list = activeStyles[styleIndex].GetComponent<Style>().images;
                break;
            case UniStyle.CATEGORY_BUTTON:
                list = activeStyles[styleIndex].GetComponent<Style>().buttons;
                break;
            case UniStyle.CATEGORY_TOGGLE:
                list = activeStyles[styleIndex].GetComponent<Style>().toggles;
                break;
            case UniStyle.CATEGORY_INPUTFIELD:
                list = activeStyles[styleIndex].GetComponent<Style>().inputFields;
                break;
            case UniStyle.CATEGORY_SLIDER:
                list = activeStyles[styleIndex].GetComponent<Style>().sliders;
                break;
            case UniStyle.CATEGORY_SCROLLVIEW:
                list = activeStyles[styleIndex].GetComponent<Style>().scrollViews;
                break;
            case UniStyle.CATEGORY_SCROLLBAR:
                list = activeStyles[styleIndex].GetComponent<Style>().scrollBars;
                break;
            case UniStyle.CATEGORY_DROPDOWN:
                list = activeStyles[styleIndex].GetComponent<Style>().dropdowns;
                break;
            default:
                Debug.Log("[UniStyle] Category not found.");
                return;
        }

        if (elementIndex >= list.Count)
            return;
        reference = list[elementIndex];

        if (null == reference)
        {
            Debug.Log("[UniStyle] Style not found.");
            return;
        }

        //Execute styling via SetStyleAttributes class
        if (null != reference.GetComponent<ImageStyle>())
            UniStyle.CopyAttributes.ApplyImageStyle(reference.GetComponent<ImageStyle>().toSet, reference, toStyle);
        if (null != reference.GetComponent<ButtonStyle>())
            UniStyle.CopyAttributes.ApplyButtonStyle(reference.GetComponent<ButtonStyle>(), reference, selectedStyle);
        if (null != reference.GetComponent<TextStyle>())
        {
#if UniStyle_TMPPro
            UniStyle.CopyAttributes.ApplyTMPTextStyle(reference.GetComponent<TextStyle>().toSet, reference, toStyle);
#endif
            UniStyle.CopyAttributes.ApplyUGUITextStyle(reference.GetComponent<TextStyle>().toSet, reference, toStyle);
        }
        if (null != reference.GetComponent<ToggleStyle>())
            UniStyle.CopyAttributes.ApplyToggleStyle(reference.GetComponent<ToggleStyle>(), reference, selectedStyle);
        if (null != reference.GetComponent<InputFieldStyle>())
            UniStyle.CopyAttributes.ApplyInputfieldStyle(reference.GetComponent<InputFieldStyle>(), reference, selectedStyle);
        if (null != reference.GetComponent<SliderStyle>())
            UniStyle.CopyAttributes.ApplySliderStyle(reference.GetComponent<SliderStyle>(), reference, selectedStyle);
        if (null != reference.GetComponent<ScrollbarStyle>())
            UniStyle.CopyAttributes.ApplyScrollbarStyle(reference.GetComponent<ScrollbarStyle>(), reference, selectedStyle);
        if (null != reference.GetComponent<ScrollViewStyle>())
            UniStyle.CopyAttributes.ApplyScrollViewStyle(reference.GetComponent<ScrollViewStyle>(), reference, selectedStyle);
        if (null != reference.GetComponent<DropdownStyle>())
            UniStyle.CopyAttributes.ApplyDropdownStyle(reference.GetComponent<DropdownStyle>(), reference, selectedStyle);

    }

    /// <summary>
    /// Finds all ApplyStyle components in the current scene and updates their styling.
    /// </summary>
    /// <see cref="global::ApplyStyle"/>
    public void RefreshStyles()
    {
        if (null == activeStyles || 0 == activeStyles.Count)
        {
            Debug.Log("[UniStyle] No active styles selected.");
            return;
        }

        ApplyStyle[] allStyles = (ApplyStyle[]) FindObjectsOfType(typeof(ApplyStyle));
        foreach (ApplyStyle st in allStyles)
        {
            ApplyStyle(st);
        }
        Debug.Log("[UniStyle] " + allStyles.Length.ToString("N0") + " UI elements refreshed.");
        FindObjectOfType<Canvas>().transform.position = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// Creates AutoApplyAdd component when auto apply is toggled on and removes otherwise.
    /// </summary>
    /// <see cref="AutoApplyAdd"/>
    public void AutoApplyChanged()
    {
        if (0 != selectedAutoStyle)
        {

            if (null == currentApply)
                currentApply = this.gameObject.AddComponent<AutoApplyAdd>();
        }
        else
        {
#if UNITY_EDITOR
            if (null != this.GetComponent<AutoApplyAdd>())
                this.GetComponent<AutoApplyAdd>().DestroyMe();
            if (null != currentApply)
            {
                currentApply.DestroyMe();
                currentApply = null;
            }
#endif
        }
    }

    /// <summary>
    /// Creates AutoStyleAdd component when auto apply is toggled on and removes otherwise.
    /// </summary>
    /// <see cref="AutoStyleAdd"/>
    public void AutoStyleToggled()
    {
        if (autoAttachStyleScripts && null == currentStyle)
        {

            currentStyle = this.gameObject.AddComponent<AutoStyleAdd>();
        }
        else
        {
#if UNITY_EDITOR
            if (null != this.GetComponent<AutoStyleAdd>())
                this.GetComponent<AutoStyleAdd>().DestroyMe();
            if (null != currentStyle)
            {
                currentStyle.DestroyMe();
                currentStyle = null;
            }
#endif
        }
    }

    /// <summary>
    /// Returns element names from a given style in a given category.
    /// </summary>
    /// <param name="styleIndex">Index from the activeStyles list</param>
    /// <param name="categoryIndex">Category ID</param>
    /// <returns>List with names of elements from the category.</returns>
    /// <see cref="UniStyle" for category ID constants/>
    public string[] GetElementNamesFromCategory(int styleIndex, int categoryIndex)
    {
        if (null == activeStyles || 0 == activeStyles.Count)
        {
            Debug.Log("[UniStyle] No active styles selected.");
            return new string[] { "None" };
        }
        List<GameObject> list = null;

        //Get all elements from selected style and category
        switch (categoryIndex)
        {
            case UniStyle.CATEGORY_TEXT:
                list = activeStyles[styleIndex].GetComponent<Style>().texts;
                break;
            case UniStyle.CATEGORY_IMAGE:
                list = activeStyles[styleIndex].GetComponent<Style>().images;
                break;
            case UniStyle.CATEGORY_BUTTON:
                list = activeStyles[styleIndex].GetComponent<Style>().buttons;
                break;
            case UniStyle.CATEGORY_TOGGLE:
                list = activeStyles[styleIndex].GetComponent<Style>().toggles;
                break;
            case UniStyle.CATEGORY_INPUTFIELD:
                list = activeStyles[styleIndex].GetComponent<Style>().inputFields;
                break;
            case UniStyle.CATEGORY_SLIDER:
                list = activeStyles[styleIndex].GetComponent<Style>().sliders;
                break;
            case UniStyle.CATEGORY_SCROLLVIEW:
                list = activeStyles[styleIndex].GetComponent<Style>().scrollViews;
                break;
            case UniStyle.CATEGORY_SCROLLBAR:
                list = activeStyles[styleIndex].GetComponent<Style>().scrollBars;
                break;
            case UniStyle.CATEGORY_DROPDOWN:
                list = activeStyles[styleIndex].GetComponent<Style>().dropdowns;
                break;
            default:
                return null;
        }
        List<string> toReturn = new List<string>() { "None" };
        toReturn.AddRange(list.Select(x => x.name));

        return toReturn.ToArray();
    }

}