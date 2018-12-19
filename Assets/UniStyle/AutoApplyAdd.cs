using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Adds ApplyStyle components to all fitting UI elements in the current scene.
/// </summary>
public class AutoApplyAdd : MonoBehaviour
{
    /// <summary>
    /// Listen to changes in the hierarchy window to search for new UI elements.
    /// </summary>
#if UNITY_EDITOR
    AutoApplyAdd()
    {
        UnityEditor.EditorApplication.hierarchyWindowChanged += CheckForNewObjects;
    }
#endif

    /// <summary>
    /// Searches the current scene for UI elements that are missing ApplyStyle components. Add those
    /// components (under certain circumstances).
    /// <see cref="ApplyStyle"/>
    /// </summary>
    void CheckForNewObjects()
    {
        Button[] allBtns = GameObject.FindObjectsOfType<Button>();
        foreach (Button b in allBtns)
        {
            if (null == b.GetComponent<ApplyStyle>())
                AddApplyStyle(b, UniStyle.CATEGORY_BUTTON);

        }
        Toggle[] allToggles = GameObject.FindObjectsOfType<Toggle>();
        foreach (Toggle t in allToggles)
        {
            if (null == t.GetComponent<ApplyStyle>())
                AddApplyStyle(t, UniStyle.CATEGORY_TOGGLE);
        }
        Image[] allImgs = GameObject.FindObjectsOfType<Image>();
        foreach (Image i in allImgs)
        {
            if (null == i.GetComponent<ApplyStyle>() && (i.name.Contains("Image") || i.name.Contains("Panel")))
                AddApplyStyle(i, UniStyle.CATEGORY_IMAGE);
        }
        Text[] allTexts = GameObject.FindObjectsOfType<Text>();
        foreach (Text t in allTexts)
        {
            if (null == t.GetComponent<ApplyStyle>() && t.name.Contains("Text") && null == t.transform.parent.GetComponent<Button>())
                AddApplyStyle(t, UniStyle.CATEGORY_TEXT);
        }
#if UniStyle_TMPPro
        TMPro.TextMeshProUGUI[] allTMP = GameObject.FindObjectsOfType<TMPro.TextMeshProUGUI>();
        foreach (TMPro.TextMeshProUGUI tm in allTMP)
        {
            if (null == tm.GetComponent<ApplyStyle>() && tm.name.Contains("TextMeshPro") && null == tm.transform.parent.GetComponent<Button>())
                AddApplyStyle(tm, UniStyle.CATEGORY_TEXT);
        }
#endif
        InputField[] allInp = GameObject.FindObjectsOfType<InputField>();
        foreach (InputField inp in allInp)
        {
            if (null == inp.GetComponent<ApplyStyle>())
                AddApplyStyle(inp, UniStyle.CATEGORY_INPUTFIELD);
        }
#if UniStyle_TMPPro
        TMPro.TMP_InputField[] allTMPInp = GameObject.FindObjectsOfType<TMPro.TMP_InputField>();
        foreach (TMPro.TMP_InputField ti in allTMPInp)
        {
            if (null == ti.GetComponent<ApplyStyle>())
                AddApplyStyle(ti, UniStyle.CATEGORY_INPUTFIELD);
        }
#endif
        Slider[] allSliders = GameObject.FindObjectsOfType<Slider>();
        foreach (Slider sl in allSliders)
        {
            if (null == sl.GetComponent<ApplyStyle>())
                AddApplyStyle(sl, UniStyle.CATEGORY_SLIDER);
        }
        ScrollRect[] allScrolls = GameObject.FindObjectsOfType<ScrollRect>();
        foreach (ScrollRect sr in allScrolls)
        {
            if (null == sr.GetComponent<ApplyStyle>() && sr.name.Contains("Scroll View"))
                AddApplyStyle(sr, UniStyle.CATEGORY_SCROLLVIEW);
        }
        Scrollbar[] allScrollBars = GameObject.FindObjectsOfType<Scrollbar>();
        foreach (Scrollbar sb in allScrollBars)
        {
            if (null == sb.GetComponent<ApplyStyle>())
                AddApplyStyle(sb, UniStyle.CATEGORY_SCROLLBAR);
        }
        Dropdown[] allDrops = GameObject.FindObjectsOfType<Dropdown>();
        foreach (Dropdown dd in allDrops)
        {
            if (null == dd.GetComponent<ApplyStyle>())
                AddApplyStyle(dd, UniStyle.CATEGORY_DROPDOWN);
        }
#if UniStyle_TMPPro
        TMPro.TMP_Dropdown[] allTMPDrops = GameObject.FindObjectsOfType<TMPro.TMP_Dropdown>();
        foreach(TMPro.TMP_Dropdown tdd in allTMPDrops)
        {
            if (null == tdd.GetComponent<ApplyStyle>())
                AddApplyStyle(tdd, UniStyle.CATEGORY_DROPDOWN);
        }
#endif
    }

    /// <summary>
    /// Add the ApplyStyle with a pre-selected category on the given MonoBehavior.
    /// </summary>
    /// <param name="applyTo">Add the component to this Monobehaviour.</param>
    /// <param name="category">Pre-selected category ID for the ApplyStyle component.</param>
    /// <see cref="UniStyle" for category ID constants/>
    void AddApplyStyle(MonoBehaviour applyTo, int category)
    {
        ApplyStyle add = applyTo.gameObject.AddComponent<ApplyStyle>();
        add.selectedStyleIndex = UniStyle.ActiveStyle.selectedAutoStyle - 1;
        add.ChangeCategory(category);
    }

    /// <summary>
    /// Unregister and destroy this component.
    /// </summary>
#if UNITY_EDITOR
    public void DestroyMe() 
    {
        UnityEditor.EditorApplication.hierarchyWindowChanged -= CheckForNewObjects;
        DestroyImmediate(this);
    }
#endif

    /// <summary>
    /// Unregister on destruction of this component.
    /// </summary>
#if UNITY_EDITOR
    public void OnDestroy()
    {
        UnityEditor.EditorApplication.hierarchyWindowChanged -= CheckForNewObjects;
    }
#endif
}