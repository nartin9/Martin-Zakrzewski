using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Adds style configuration components to all fitting UI elements in the current scene.
/// </summary>
public class AutoStyleAdd : MonoBehaviour
{
    /// <summary>
    /// Listen to changes in the hierarchy window to search for new UI elements.
    /// </summary>
#if UNITY_EDITOR
    AutoStyleAdd()
    {
        UnityEditor.EditorApplication.hierarchyWindowChanged += CheckForNewObjects;
    }
#endif

    /// <summary>
    /// Searches the current scene for UI elements that are missing style configuration components. Add those
    /// components (under certain circumstances). Set references to related Gameobjects for newly created
    /// configurations.
    /// </summary>
    void CheckForNewObjects()
    {
        Button[] allBtns = GameObject.FindObjectsOfType<Button>();
        foreach (Button b in allBtns)
        {
            if (null == b.GetComponent<ButtonStyle>())
            {
                ButtonStyle bs = b.gameObject.AddComponent<ButtonStyle>();
                bs.buttonImage = b.GetComponentInChildren<Image>();
                if (null != b.GetComponentInChildren<Text>()) bs.buttonText = b.GetComponentInChildren<Text>();
#if UniStyle_TMPPro
                if (null != b.GetComponentInChildren<TMPro.TextMeshProUGUI>()) bs.buttonTMPText = b.GetComponentInChildren<TMPro.TextMeshProUGUI>();
#endif
            }
        }
        Toggle[] allToggles = GameObject.FindObjectsOfType<Toggle>();
        foreach (Toggle t in allToggles)
        {
            if (null == t.GetComponent<ToggleStyle>())
            {
                ToggleStyle ts = t.gameObject.AddComponent<ToggleStyle>();
                ts.backgroundImage = t.GetComponentsInChildren<Image>() [0];
                ts.checkmarkImage = t.GetComponentsInChildren<Image>() [1];
                if (null != t.GetComponentInChildren<Text>()) ts.labelText = t.GetComponentInChildren<Text>();
#if UniStyle_TMPPro
                if (null != t.GetComponentInChildren<TMPro.TextMeshProUGUI>()) ts.labelTMPText = t.GetComponentInChildren<TMPro.TextMeshProUGUI>();
#endif
            }

        }
        Image[] allImgs = GameObject.FindObjectsOfType<Image>();
        foreach (Image i in allImgs)
        {
            if (null == i.GetComponent<ImageStyle>() && i.name.Contains("Image"))
            {
                i.gameObject.AddComponent<ImageStyle>();
            }
        }
        Text[] allTexts = GameObject.FindObjectsOfType<Text>();
        foreach (Text t in allTexts)
        {
            if (null == t.GetComponent<TextStyle>() && t.name.Contains("Text") && null == t.transform.parent.GetComponent<Button>())
            {
                t.gameObject.AddComponent<TextStyle>();
            }
        }
#if UniStyle_TMPPro
        TMPro.TextMeshProUGUI[] allTMP = GameObject.FindObjectsOfType<TMPro.TextMeshProUGUI>();
        foreach (TMPro.TextMeshProUGUI tm in allTMP)
        {
            if (null == tm.GetComponent<TextStyle>() && tm.name.Contains("TextMeshPro") && null == tm.transform.parent.GetComponent<Button>())
            {
                tm.gameObject.AddComponent<TextStyle>();
            }
        }
#endif
        InputField[] allInp = GameObject.FindObjectsOfType<InputField>();
        foreach (InputField inp in allInp)
        {
            if (null == inp.GetComponent<InputFieldStyle>())
            {
                InputFieldStyle inst = inp.gameObject.AddComponent<InputFieldStyle>();
                inst.placeholderText = inp.GetComponentsInChildren<Text>() [0];
                inst.textfieldText = inp.GetComponentsInChildren<Text>() [1];
                inst.backgroundImage = inp.GetComponent<Image>();
            }
        }
#if UniStyle_TMPPro
        TMPro.TMP_InputField[] allTMPInp = GameObject.FindObjectsOfType<TMPro.TMP_InputField>();
        foreach (TMPro.TMP_InputField ti in allTMPInp)
        {
            if (null == ti.GetComponent<InputFieldStyle>())
            {
                InputFieldStyle inst = ti.gameObject.AddComponent<InputFieldStyle>();
                inst.placeholderTMPText = ti.GetComponentsInChildren<TMPro.TextMeshProUGUI>() [0];
                inst.textfieldTMPText = ti.GetComponentsInChildren<TMPro.TextMeshProUGUI>() [1];
                inst.backgroundImage = ti.GetComponent<Image>();
            }
        }
#endif
        Slider[] allSliders = GameObject.FindObjectsOfType<Slider>();
        foreach (Slider sl in allSliders)
        {
            if (null == sl.GetComponent<SliderStyle>())
            {
                SliderStyle sls = sl.gameObject.AddComponent<SliderStyle>();
                sls.backgroundImage = sl.GetComponentsInChildren<Image>() [0];
                sls.fillImage = sl.GetComponentsInChildren<Image>() [1];
                sls.handleImage = sl.GetComponentsInChildren<Image>() [2];
            }
        }
        ScrollRect[] allScrolls = GameObject.FindObjectsOfType<ScrollRect>();
        foreach (ScrollRect sr in allScrolls)
        {
            if (null == sr.GetComponent<ScrollViewStyle>())
            {
                ScrollViewStyle svs = sr.gameObject.AddComponent<ScrollViewStyle>();
                svs.backgroundImage = sr.GetComponent<Image>();
                svs.viewportImage = sr.GetComponentsInChildren<Image>() [1];
            }
        }
        Scrollbar[] allScrollBars = GameObject.FindObjectsOfType<Scrollbar>();
        foreach (Scrollbar sb in allScrollBars)
        {
            if (null == sb.GetComponent<ScrollbarStyle>())
            {
                ScrollbarStyle sbs = sb.gameObject.AddComponent<ScrollbarStyle>();
                sbs.backgroundImage = sb.GetComponent<Image>();
                sbs.handleImage = sb.GetComponentsInChildren<Image>() [1];
            }
        }
        Dropdown[] allDrops = GameObject.FindObjectsOfType<Dropdown>();
        foreach (Dropdown dd in allDrops)
        {
            if (null == dd.GetComponent<DropdownStyle>())
            {
                DropdownStyle dds = dd.gameObject.AddComponent<DropdownStyle>();
                dds.backgroundImage = dd.GetComponent<Image>();
                dds.arrowImage = dd.GetComponentsInChildren<Image>() [1];
                dd.template.gameObject.SetActive(true);
                dds.itemBackgroundImage = dd.GetComponentsInChildren<Image>() [4];
                dds.itemCheckmarkImage = dd.GetComponentsInChildren<Image>() [5];
                if (null != dds.GetComponentInChildren<Text>())
                {
                    dds.captionText = dds.GetComponentsInChildren<Text>() [0];
                    dds.itemText = dds.GetComponentsInChildren<Text>() [1];
                }
                dd.template.gameObject.SetActive(false);
            }
        }
#if UniStyle_TMPPro
        TMPro.TMP_Dropdown[] allTMPDrops = GameObject.FindObjectsOfType<TMPro.TMP_Dropdown>();
        foreach (TMPro.TMP_Dropdown dd in allTMPDrops)
        {
            if (null == dd.GetComponent<DropdownStyle>())
            {
                DropdownStyle dds = dd.gameObject.AddComponent<DropdownStyle>();
                dds.backgroundImage = dd.GetComponent<Image>();
                dds.arrowImage = dd.GetComponentsInChildren<Image>() [1];
                dd.template.gameObject.SetActive(true);
                dds.itemBackgroundImage = dd.GetComponentsInChildren<Image>() [4];
                dds.itemCheckmarkImage = dd.GetComponentsInChildren<Image>() [5];
                if (null != dds.GetComponentInChildren<TMPro.TextMeshProUGUI>())
                {
                    dds.captionTMPText = dds.GetComponentsInChildren<TMPro.TextMeshProUGUI>() [0];
                    dds.itemTMPText = dds.GetComponentsInChildren<TMPro.TextMeshProUGUI>() [1];
                }
                dd.template.gameObject.SetActive(false);
            }
        }
#endif
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