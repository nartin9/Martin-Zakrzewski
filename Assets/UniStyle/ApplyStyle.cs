using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
/// <summary>
/// Stores the selected style for a given UI element and references to related Gameobjects.
/// </summary>
public class ApplyStyle : MonoBehaviour
{

    public int selectedStyleIndex;
    public int selectedCategoryIndex;
    public int selectedElementIndex;
    public List<Text> targetTexts;
#if UniStyle_TMPPro
    public List<TMPro.TextMeshProUGUI> targetTMPTexts;
#endif
    public List<Image> targetImages;

    /// <summary>
    /// Changes the style-category of the given UI element. Creates references for related Gameobjects.
    /// </summary>
    /// <param name="category">Category ID.</param>
    /// <see cref="UniStyle" for category ID constants/>
    public void ChangeCategory(int category)
    {
        selectedElementIndex = 0;
        selectedCategoryIndex = category;
        if (selectedCategoryIndex == UniStyle.CATEGORY_BUTTON)
        {
            targetImages = new List<Image>();
            targetImages.Add(gameObject.GetComponent<Image>());
            targetTexts = new List<Text>();

            if (null != gameObject.GetComponentInChildren<Text>())
            {
                targetTexts.Add(gameObject.GetComponentInChildren<Text>());
            }
            else
            {
                targetTexts.Add(null);
            }
#if UniStyle_TMPPro
            targetTMPTexts = new List<TMPro.TextMeshProUGUI>();
            if (null != gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>())
            {
                targetTMPTexts.Add(gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>());
            }
            else
            {
                targetTMPTexts.Add(null);
            }
#endif
        }

        if (selectedCategoryIndex == UniStyle.CATEGORY_TOGGLE)
        {
            targetImages = new List<Image>();
            targetImages.Add(gameObject.GetComponentsInChildren<Image>() [0]);
            targetImages.Add(gameObject.GetComponentsInChildren<Image>() [1]);
            targetTexts = new List<Text>();

            if (null != gameObject.GetComponentInChildren<Text>())
            {
                targetTexts.Add(gameObject.GetComponentInChildren<Text>());
            }
            else
            {
                targetTexts.Add(null);
            }

#if UniStyle_TMPPro
            targetTMPTexts = new List<TMPro.TextMeshProUGUI>();
            if (null != gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>())
            {
                targetTMPTexts.Add(gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>());
            }
            else
            {
                targetTMPTexts.Add(null);
            }
#endif
        }
        if (selectedCategoryIndex == UniStyle.CATEGORY_INPUTFIELD)
        {
            targetImages = new List<Image>();
            targetImages.Add(gameObject.GetComponent<Image>());
            targetTexts = new List<Text>();
            if (null != gameObject.GetComponentInChildren<Text>())
            {
                targetTexts.Add(gameObject.GetComponentsInChildren<Text>() [0]);
                targetTexts.Add(gameObject.GetComponentsInChildren<Text>() [1]);
            }
            else
            {
                targetTexts.Add(null);
                targetTexts.Add(null);
            }
#if UniStyle_TMPPro
            targetTMPTexts = new List<TMPro.TextMeshProUGUI>();
            if (null != gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>())
            {
                targetTMPTexts.Add(gameObject.GetComponentsInChildren<TMPro.TextMeshProUGUI>() [0]);
                targetTMPTexts.Add(gameObject.GetComponentsInChildren<TMPro.TextMeshProUGUI>() [1]);
            }
            else
            {
                targetTMPTexts.Add(null);
                targetTMPTexts.Add(null);
            }
#endif
        }
        if (selectedCategoryIndex == UniStyle.CATEGORY_SLIDER)
        {
            targetImages = new List<Image>();
            targetImages.Add(gameObject.GetComponentsInChildren<Image>() [0]);
            targetImages.Add(gameObject.GetComponentsInChildren<Image>() [1]);
            targetImages.Add(gameObject.GetComponentsInChildren<Image>() [2]);
        }
        if (selectedCategoryIndex == UniStyle.CATEGORY_SCROLLVIEW)
        {
            targetImages = new List<Image>();
            targetImages.Add(gameObject.GetComponent<Image>());
            targetImages.Add(gameObject.GetComponentsInChildren<Image>() [1]);
        }
        if (selectedCategoryIndex == UniStyle.CATEGORY_SCROLLBAR)
        {
            targetImages = new List<Image>();
            targetImages.Add(gameObject.GetComponent<Image>());
            targetImages.Add(gameObject.GetComponentsInChildren<Image>() [1]);
        }
        if (selectedCategoryIndex == UniStyle.CATEGORY_DROPDOWN)
        {
            targetImages = new List<Image>();
#if !UniStyle_TMPPro
            GetComponent<Dropdown>().template.gameObject.SetActive(true);
#endif
#if UniStyle_TMPPro
            GetComponent<TMPro.TMP_Dropdown>().template.gameObject.SetActive(true);
#endif
            targetImages.Add(gameObject.GetComponent<Image>());
            targetImages.Add(gameObject.GetComponentsInChildren<Image>() [1]);
            targetImages.Add(null);
            targetImages.Add(null);
            targetImages.Add(gameObject.GetComponentsInChildren<Image>() [4]);
            targetImages.Add(gameObject.GetComponentsInChildren<Image>() [5]);

            targetTexts = new List<Text>();

            if (null != gameObject.GetComponentInChildren<Text>())
            {
                targetTexts.Add(gameObject.GetComponentsInChildren<Text>() [0]);
                targetTexts.Add(gameObject.GetComponentsInChildren<Text>() [1]);
            }
            else
            {
                targetTexts.Add(null);
                targetTexts.Add(null);
            }
#if UniStyle_TMPPro
            targetTMPTexts = new List<TMPro.TextMeshProUGUI>();
            if (null != gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>())
            {
                targetTMPTexts.Add(gameObject.GetComponentsInChildren<TMPro.TextMeshProUGUI>() [0]);
                targetTMPTexts.Add(gameObject.GetComponentsInChildren<TMPro.TextMeshProUGUI>() [1]);
            }
            else
            {
                targetTMPTexts.Add(null);
                targetTMPTexts.Add(null);
            }
            GetComponent<TMPro.TMP_Dropdown>().template.gameObject.SetActive(false);
#endif
#if !UniStyle_TMPPro
            GetComponent<Dropdown>().template.gameObject.SetActive(false);
#endif
        }
    }

}