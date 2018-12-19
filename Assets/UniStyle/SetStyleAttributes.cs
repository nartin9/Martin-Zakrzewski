using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Copies UI configuration from referenced objects to selected UI elements.
/// </summary>
public class SetStyleAttributes
{
    /// <summary>
    /// Apply image configuration from a reference object to a given UI element.
    /// </summary>
    /// <param name="style">Style configuration containing which attributes should be copied</param>
    /// <param name="reference">Reference object</param>
    /// <param name="toStyle">UI element to be styled</param>
    public void ApplyImageStyle(ImageAttributes style, GameObject reference, GameObject toStyle)
    {
        if (null == reference || null == toStyle)
            return;
        Image targetImg = toStyle.GetComponentInChildren<Image>(),
            refImg = reference.GetComponent<Image>();
        if (null == targetImg || null == targetImg)
            return;
        if (style.setDefaultSprite)
            targetImg.sprite = refImg.sprite;
        if (style.setImageType)
            targetImg.type = refImg.type;
        if (style.setTintColor)
            targetImg.color = refImg.color;
        RectTransform targetRect = toStyle.GetComponent<RectTransform>(),
            refRect = reference.GetComponent<RectTransform>();
        if (style.setAnchors)
        {
            targetRect.anchorMin = refRect.anchorMin;
            targetRect.anchorMax = refRect.anchorMax;
            Vector2 newOffsetMin = targetRect.offsetMin;
            Vector2 newOffsetMax = targetRect.offsetMax;
            if (style.setHeight)
            {
                newOffsetMax.y = refRect.offsetMax.y;
                newOffsetMin.y = refRect.offsetMin.y;
            }
            if (style.setWidth)
            {
                newOffsetMax.x = refRect.offsetMax.x;
                newOffsetMin.x = refRect.offsetMin.x;
            }
            targetRect.offsetMax = newOffsetMax;
            targetRect.offsetMin = newOffsetMin;
        }

        Vector2 newSize = targetRect.sizeDelta;
        if (style.setHeight)
            newSize.y = refRect.sizeDelta.y;
        if (style.setWidth)
            newSize.x = refRect.sizeDelta.x;
        targetRect.sizeDelta = newSize;

    }

    /// <summary>
    /// Apply button configuration from a reference object to a given UI element.
    /// </summary>
    /// <param name="style">Style configuration containing which attributes should be copied</param>
    /// <param name="reference">Reference object</param>
    /// <param name="toStyle">UI element to be styled</param>
    public void ApplyButtonStyle(ButtonStyle style, GameObject reference, ApplyStyle toStyle)
    {
        if (null == reference || null == toStyle)
            return;
        Button targetBtn = toStyle.GetComponent<Button>(),
            refBtn = reference.GetComponent<Button>();
        if (null == targetBtn || null == refBtn)
            return;
        if (style.transition.setTransitionType)
            targetBtn.transition = refBtn.transition;
        if (style.transition.setColorBlock)
            targetBtn.colors = refBtn.colors;
        if (style.transition.setTransitionSprites)
            targetBtn.spriteState = refBtn.spriteState;
        if (style.transition.setAnimations)
            targetBtn.animationTriggers = refBtn.animationTriggers;
        
        if (null != toStyle.targetImages[0] && null != style.buttonImage)
            ApplyImageStyle(style.imageStyle, style.buttonImage.gameObject, toStyle.targetImages[0].gameObject);
        if (null != toStyle.targetTexts[0] && null != style.buttonText)
            ApplyUGUITextStyle(style.textStyle, style.buttonText.gameObject, toStyle.targetTexts[0].gameObject);
#if UniStyle_TMPPro
        if (null != toStyle.targetTMPTexts[0] && null != style.buttonTMPText)
            ApplyTMPTextStyle(style.textStyle, style.buttonTMPText.gameObject, toStyle.targetTMPTexts[0].gameObject);
#endif
    }

#if UniStyle_TMPPro
    /// <summary>
    /// Apply Textmesh Pro text configuration from a reference object to a given UI element.
    /// </summary>
    /// <param name="style">Style configuration containing which attributes should be copied</param>
    /// <param name="reference">Reference object</param>
    /// <param name="toStyle">UI element to be styled</param>
    public void ApplyTMPTextStyle(TextAttributes style, GameObject reference, GameObject toStyle)
    {
        if (null == reference || null == toStyle)
            return;
        TMPro.TextMeshProUGUI targetTmp = toStyle.GetComponentInChildren<TMPro.TextMeshProUGUI>(),
            refTmp = reference.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (null == targetTmp || null == refTmp)
            return;
        if (style.setTextColor)
            targetTmp.color = refTmp.color;
        if (style.setTextFont)
            targetTmp.font = refTmp.font;
        if (style.setTextSize)
            targetTmp.fontSize = refTmp.fontSize;
        if (style.setTextStyle)
            targetTmp.fontStyle = refTmp.fontStyle;
        if (style.setAlignment)
            targetTmp.alignment = refTmp.alignment;
        if (style.setAutoSize)
        {
            targetTmp.enableAutoSizing = refTmp.enableAutoSizing;
            if (style.setAutoSizeMax)
                targetTmp.fontSizeMax = refTmp.fontSizeMax;
            if (style.setAutoSizeMin)
                targetTmp.fontSizeMin = refTmp.fontSizeMin;
        }

        RectTransform styleTrans = targetTmp.GetComponent<RectTransform>(),
            styleRef = refTmp.GetComponent<RectTransform>();
        if (style.setAnchors)
        {
            styleTrans.anchorMin = styleRef.anchorMin;
            styleTrans.anchorMax = styleRef.anchorMax;
            Vector2 newOffsetMin = styleTrans.offsetMin;
            Vector2 newOffsetMax = styleTrans.offsetMax;
            if (style.setHeight)
            {
                newOffsetMax.y = styleRef.offsetMax.y;
                newOffsetMin.y = styleRef.offsetMin.y;
            }
            if (style.setWidth)
            {
                newOffsetMax.x = styleRef.offsetMax.x;
                newOffsetMin.x = styleRef.offsetMin.x;
            }
            styleTrans.offsetMax = newOffsetMax;
            styleTrans.offsetMin = newOffsetMin;
        }

        Vector2 newSize = styleTrans.sizeDelta;
        if (style.setHeight)
        {
            newSize.y = styleRef.sizeDelta.y;
        }
        if (style.setWidth)
        {
            newSize.x = styleRef.sizeDelta.x;
        }
        styleTrans.sizeDelta = newSize;
    }
#endif

    /// <summary>
    /// Apply text configuration from a reference object to a given UI element.
    /// </summary>
    /// <param name="style">Style configuration containing which attributes should be copied</param>
    /// <param name="reference">Reference object</param>
    /// <param name="toStyle">UI element to be styled</param>
    public void ApplyUGUITextStyle(TextAttributes style, GameObject reference, GameObject toStyle)
    {
        if (null == reference || null == toStyle)
            return;
        Text targetTxt = toStyle.GetComponent<Text>(),
            refTxt = reference.GetComponent<Text>();
        if (null == targetTxt || null == refTxt)
            return;
        if (style.setTextColor)
            targetTxt.color = refTxt.color;
        if (style.setTextFont)
            targetTxt.font = refTxt.font;
        if (style.setTextSize)
            targetTxt.fontSize = refTxt.fontSize;
        if (style.setTextStyle)
            targetTxt.fontStyle = refTxt.fontStyle;
        if (style.setAlignment)
            targetTxt.alignment = refTxt.alignment;
        if (style.setAutoSize)
        {
            targetTxt.resizeTextForBestFit = refTxt.resizeTextForBestFit;

            if (style.setAutoSizeMax)
                targetTxt.resizeTextMaxSize = refTxt.resizeTextMaxSize;
            if (style.setAutoSizeMin)
                targetTxt.resizeTextMinSize = refTxt.resizeTextMinSize;
        }

        RectTransform styleTrans = targetTxt.GetComponent<RectTransform>(),
            styleRef = refTxt.GetComponent<RectTransform>();
        if (style.setAnchors)
        {
            styleTrans.anchorMin = styleRef.anchorMin;
            styleTrans.anchorMax = styleRef.anchorMax;
            Vector2 newOffsetMin = styleTrans.offsetMin;
            Vector2 newOffsetMax = styleTrans.offsetMax;
            if (style.setHeight)
            {
                newOffsetMax.y = styleRef.offsetMax.y;
                newOffsetMin.y = styleRef.offsetMin.y;
            }
            if (style.setWidth)
            {
                newOffsetMax.x = styleRef.offsetMax.x;
                newOffsetMin.x = styleRef.offsetMin.x;
            }
            styleTrans.offsetMax = newOffsetMax;
            styleTrans.offsetMin = newOffsetMin;
        }

        Vector2 newSize = styleTrans.sizeDelta;
        if (style.setHeight)
        {
            newSize.y = styleRef.sizeDelta.y;
        }
        if (style.setWidth)
        {
            newSize.x = styleRef.sizeDelta.x;
        }
        styleTrans.sizeDelta = newSize;
    }

    /// <summary>
    /// Apply toggle configuration from a reference object to a given UI element.
    /// </summary>
    /// <param name="style">Style configuration containing which attributes should be copied</param>
    /// <param name="reference">Reference object</param>
    /// <param name="toStyle">UI element to be styled</param>
    public void ApplyToggleStyle(ToggleStyle style, GameObject reference, ApplyStyle toStyle)
    {
        if (null == reference || null == toStyle)
            return;
        Toggle targetTgl = toStyle.GetComponent<Toggle>(),
            refTgl = reference.GetComponent<Toggle>();
        if (null == targetTgl || null == refTgl)
            return;
        if (style.transition.setTransitionType)
            targetTgl.transition = refTgl.transition;
        if (style.transition.setColorBlock)
            targetTgl.colors = refTgl.colors;
        if (style.transition.setTransitionSprites)
            targetTgl.spriteState = refTgl.spriteState;
        if (style.transition.setAnimations)
            targetTgl.animationTriggers = refTgl.animationTriggers;

        if (null != toStyle.targetImages[0] && null != style.backgroundImage)
            ApplyImageStyle(style.backgroundStyle, style.backgroundImage.gameObject, toStyle.targetImages[0].gameObject);
        if(null != toStyle.targetImages[1] && null != style.checkmarkImage)
            ApplyImageStyle(style.checkmarkStyle, style.checkmarkImage.gameObject, toStyle.targetImages[1].gameObject);
        if (null != toStyle.targetTexts[0] && null != style.labelText)
            ApplyUGUITextStyle(style.labelStyle, style.labelText.gameObject, toStyle.targetTexts[0].gameObject);
#if UniStyle_TMPPro
        if (null != toStyle.targetTMPTexts[0] && null != style.labelTMPText)
            ApplyTMPTextStyle(style.labelStyle, style.labelTMPText.gameObject, toStyle.targetTMPTexts[0].gameObject);
#endif
    }

    /// <summary>
    /// Apply input field configuration from a reference object to a given UI element.
    /// </summary>
    /// <param name="style">Style configuration containing which attributes should be copied</param>
    /// <param name="reference">Reference object</param>
    /// <param name="toStyle">UI element to be styled</param>
    public void ApplyInputfieldStyle(InputFieldStyle style, GameObject reference, ApplyStyle toStyle)
    {
        if (null == reference || null == toStyle)
            return;
        InputField targetIf = toStyle.GetComponent<InputField>(),
            refIf = reference.GetComponent<InputField>();
        if (null != targetIf && null != refIf)
        {
            if (style.transition.setTransitionType)
                targetIf.transition = refIf.transition;
            if (style.transition.setColorBlock)
                targetIf.colors = refIf.colors;
            if (style.transition.setTransitionSprites)
                targetIf.spriteState = refIf.spriteState;
            if (style.transition.setAnimations)
                targetIf.animationTriggers = refIf.animationTriggers;

            if (style.setCaretBlinkRate)
                targetIf.caretBlinkRate = refIf.caretBlinkRate;
            if (style.setCaretWidth)
                targetIf.caretWidth = refIf.caretWidth;
            if (style.setCaretColor)
            {
                targetIf.customCaretColor = refIf.customCaretColor;
                targetIf.caretColor = refIf.caretColor;
            }
            if (style.setSelectionColor)
                targetIf.selectionColor = refIf.selectionColor;
            if (style.setReadOnly)
                targetIf.readOnly = refIf.readOnly;
            if(null != toStyle.targetImages[0] && null != style.backgroundImage)
                ApplyImageStyle(style.backgroundStyle, style.backgroundImage.gameObject, toStyle.targetImages[0].gameObject);
            if(null != toStyle.targetTexts[0] && null != style.placeholderText)
                ApplyUGUITextStyle(style.placeholderStyle, style.placeholderText.gameObject, toStyle.targetTexts[0].gameObject);
            if(null != toStyle.targetTexts[1] && null != style.textfieldText)
                ApplyUGUITextStyle(style.textfieldStyle, style.textfieldText.gameObject, toStyle.targetTexts[1].gameObject);
        }
#if UniStyle_TMPPro
        TMPro.TMP_InputField targetTmpIf = toStyle.GetComponent<TMPro.TMP_InputField>(),
            refTmpIf = reference.GetComponent<TMPro.TMP_InputField>();
        if (null != targetTmpIf && null != refTmpIf)
        {
            if (style.transition.setTransitionType)
                targetTmpIf.transition = refTmpIf.transition;
            if (style.transition.setColorBlock)
                targetTmpIf.colors = refTmpIf.colors;
            if (style.transition.setTransitionSprites)
                targetTmpIf.spriteState = refTmpIf.spriteState;
            if (style.transition.setAnimations)
                targetTmpIf.animationTriggers = refTmpIf.animationTriggers;
            if (style.setCaretBlinkRate)
                targetTmpIf.caretBlinkRate = refTmpIf.caretBlinkRate;
            if (style.setCaretWidth)
                targetTmpIf.caretWidth = refTmpIf.caretWidth;
            if (style.setCaretColor)
            {
                targetTmpIf.customCaretColor = refTmpIf.customCaretColor;
                targetTmpIf.caretColor = refTmpIf.caretColor;
            }
            if (style.setSelectionColor)
                targetTmpIf.selectionColor = refTmpIf.selectionColor;
            if (style.setReadOnly)
                targetTmpIf.readOnly = refTmpIf.readOnly;
            if(null != toStyle.targetImages[0] && null != style.backgroundImage)
                ApplyImageStyle(style.backgroundStyle, style.backgroundImage.gameObject, toStyle.targetImages[0].gameObject);
            if(null != toStyle.targetTMPTexts[0] && null != style.placeholderTMPText)
                ApplyTMPTextStyle(style.placeholderStyle, style.placeholderTMPText.gameObject, toStyle.targetTMPTexts[0].gameObject);
            if(null != toStyle.targetTMPTexts[1] && null != style.placeholderTMPText)
                ApplyTMPTextStyle(style.textfieldStyle, style.textfieldTMPText.gameObject, toStyle.targetTMPTexts[1].gameObject);

        }
#endif
    }

    /// <summary>
    /// Apply slider configuration from a reference object to a given UI element.
    /// </summary>
    /// <param name="style">Style configuration containing which attributes should be copied</param>
    /// <param name="reference">Reference object</param>
    /// <param name="toStyle">UI element to be styled</param>
    public void ApplySliderStyle(SliderStyle style, GameObject reference, ApplyStyle toStyle)
    {
        if (null == reference || null == toStyle)
            return;
        Slider targetSlider = toStyle.GetComponent<Slider>(),
            refSlider = reference.GetComponent<Slider>();
        if (null == targetSlider || null == refSlider)
            return;
        if (style.transition.setTransitionType)
            targetSlider.transition = refSlider.transition;
        if (style.transition.setColorBlock)
            targetSlider.colors = refSlider.colors;
        if (style.transition.setTransitionSprites)
            targetSlider.spriteState = refSlider.spriteState;
        if (style.transition.setAnimations)
            targetSlider.animationTriggers = refSlider.animationTriggers;

        Vector2 newSize = targetSlider.GetComponent<RectTransform>().sizeDelta;
        if (style.setWidth)
            newSize.x = refSlider.GetComponent<RectTransform>().sizeDelta.x;
        if (style.setHeight)
            newSize.y = refSlider.GetComponent<RectTransform>().sizeDelta.y;
        targetSlider.GetComponent<RectTransform>().sizeDelta = newSize;
        if(null != toStyle.targetImages[0] && null != style.backgroundImage)
            ApplyImageStyle(style.backgroundStyle, style.backgroundImage.gameObject, toStyle.targetImages[0].gameObject);
        if(null != toStyle.targetImages[1] && null != style.fillImage)
            ApplyImageStyle(style.fillStyle, style.fillImage.gameObject, toStyle.targetImages[1].gameObject);
        if(null != toStyle.targetImages[2] && null != style.handleImage)
            ApplyImageStyle(style.handleStyle, style.handleImage.gameObject, toStyle.targetImages[2].gameObject);
    }

    /// <summary>
    /// Apply scrollbar configuration from a reference object to a given UI element.
    /// </summary>
    /// <param name="style">Style configuration containing which attributes should be copied</param>
    /// <param name="reference">Reference object</param>
    /// <param name="toStyle">UI element to be styled</param>
    public void ApplyScrollbarStyle(ScrollbarStyle style, GameObject reference, ApplyStyle toStyle)
    {
        if (null == reference || null == toStyle)
            return;
        Scrollbar targetSb = toStyle.GetComponent<Scrollbar>(),
            refSb = reference.GetComponent<Scrollbar>();
        if (null == targetSb || null == refSb)
            return;
        if (style.transition.setTransitionType)
            targetSb.transition = refSb.transition;
        if (style.transition.setColorBlock)
            targetSb.colors = refSb.colors;
        if (style.transition.setTransitionSprites)
            targetSb.spriteState = refSb.spriteState;
        if (style.transition.setAnimations)
            targetSb.animationTriggers = refSb.animationTriggers;

        if (null != toStyle.targetImages[0] && null != style.backgroundImage)
            ApplyImageStyle(style.backgroundStyle, style.backgroundImage.gameObject, toStyle.targetImages[0].gameObject);
        if(null != toStyle.targetImages[1] && null != style.handleImage)
            ApplyImageStyle(style.handleStyle, style.handleImage.gameObject, toStyle.targetImages[1].gameObject);
    }

    /// <summary>
    /// Apply scroll view configuration from a reference object to a given UI element.
    /// </summary>
    /// <param name="style">Style configuration containing which attributes should be copied</param>
    /// <param name="reference">Reference object</param>
    /// <param name="toStyle">UI element to be styled</param>
    public void ApplyScrollViewStyle(ScrollViewStyle style, GameObject reference, ApplyStyle toStyle)
    {
        if (null == reference || null == toStyle)
            return;
        ScrollRect targetSr = toStyle.GetComponent<ScrollRect>(),
            refSr = reference.GetComponent<ScrollRect>();
        if (null == targetSr || null == refSr)
            return;
        if (style.setHorizontal)
            targetSr.horizontal = refSr.horizontal;
        if (style.setVertical)
            targetSr.vertical = refSr.vertical;
        if (style.setInertia)
        {
            targetSr.inertia = refSr.inertia;
            targetSr.decelerationRate = refSr.decelerationRate;
        }
        if (style.setMovementType)
        {
            targetSr.movementType = refSr.movementType;
            if (refSr.movementType == ScrollRect.MovementType.Elastic)
                targetSr.elasticity = refSr.elasticity;
        }
        if (style.setHorizontalSpacing)
            targetSr.horizontalScrollbarSpacing = refSr.horizontalScrollbarSpacing;
        if (style.setHorizontalVisibilty)
            targetSr.horizontalScrollbarVisibility = refSr.horizontalScrollbarVisibility;
        if (style.setVerticalSpacing)
            targetSr.verticalScrollbarSpacing = refSr.verticalScrollbarSpacing;
        if (style.setVerticalVisibility)
            targetSr.verticalScrollbarVisibility = refSr.verticalScrollbarVisibility;
        if (style.setScrollSensitivity)
            targetSr.scrollSensitivity = refSr.scrollSensitivity;
        if(null != toStyle.targetImages[0] && null != style.backgroundImage)
            ApplyImageStyle(style.backgroundStyle, style.backgroundImage.gameObject, toStyle.targetImages[0].gameObject);
        if(null != toStyle.targetImages[1] && null != style.viewportImage)
            ApplyImageStyle(style.viewportStyle, style.viewportImage.gameObject, toStyle.targetImages[1].gameObject);
    }

    /// <summary>
    /// Apply dropdown configuration from a reference object to a given UI element.
    /// </summary>
    /// <param name="style">Style configuration containing which attributes should be copied</param>
    /// <param name="reference">Reference object</param>
    /// <param name="toStyle">UI element to be styled</param>
    public void ApplyDropdownStyle(DropdownStyle style, GameObject reference, ApplyStyle toStyle)
    {
        if (null == reference || null == toStyle)
            return;
#if !UniStyle_TMPPro
        Dropdown targetDrop = toStyle.GetComponent<Dropdown>(),
            refDrop = reference.GetComponent<Dropdown>();
#endif
#if UniStyle_TMPPro
        TMPro.TMP_Dropdown targetDrop = toStyle.GetComponent<TMPro.TMP_Dropdown>(),
            refDrop = reference.GetComponent<TMPro.TMP_Dropdown>();
#endif
        if (null == targetDrop || null == refDrop)
            return;
        if (style.transition.setTransitionType)
            targetDrop.transition = refDrop.transition;
        if (style.transition.setColorBlock)
            targetDrop.colors = refDrop.colors;
        if (style.transition.setTransitionSprites)
            targetDrop.spriteState = refDrop.spriteState;
        if (style.transition.setAnimations)
            targetDrop.animationTriggers = refDrop.animationTriggers;

        targetDrop.template.gameObject.SetActive(true);
        if(null != toStyle.targetImages[0] && null != style.backgroundImage)
            ApplyImageStyle(style.backgroundStyle, style.backgroundImage.gameObject, toStyle.targetImages[0].gameObject);
        if(null != toStyle.targetImages[1] && null != style.arrowImage)
            ApplyImageStyle(style.arrowStyle, style.arrowImage.gameObject, toStyle.targetImages[1].gameObject);
        if (null != toStyle.targetImages[2] && null != style.captionImage)
            ApplyImageStyle(style.captionStyle, style.captionImage.gameObject, toStyle.targetImages[2].gameObject);
        if (null != toStyle.targetImages[3] && null != style.itemImage)
            ApplyImageStyle(style.itemStyle, style.itemImage.gameObject, toStyle.targetImages[3].gameObject);
        if (null != toStyle.targetImages[4] && null != style.itemBackgroundImage)
            ApplyImageStyle(style.itemBackgroundStyle, style.itemBackgroundImage.gameObject, toStyle.targetImages[4].gameObject);
        if (null != toStyle.targetImages[5] && null != style.itemCheckmarkImage)
            ApplyImageStyle(style.itemCheckmarkStyle, style.itemCheckmarkImage.gameObject, toStyle.targetImages[5].gameObject);
        if (null != toStyle.targetTexts[0] && null != style.captionText)
            ApplyUGUITextStyle(style.captionTextStyle, style.captionText.gameObject, toStyle.targetTexts[0].gameObject);
        if (null != toStyle.targetTexts[1] && null != style.itemText)
            ApplyUGUITextStyle(style.itemTextStyle, style.itemText.gameObject, toStyle.targetTexts[1].gameObject);
#if UniStyle_TMPPro
        if (null != toStyle.targetTMPTexts[0] && null != style.captionTMPText)
            ApplyTMPTextStyle(style.captionTextStyle, style.captionTMPText.gameObject, toStyle.targetTMPTexts[0].gameObject);
        if (null != toStyle.targetTMPTexts[1] && null != style.itemTMPText)
            ApplyTMPTextStyle(style.itemTextStyle, style.itemTMPText.gameObject, toStyle.targetTMPTexts[1].gameObject);
#endif
        targetDrop.template.gameObject.SetActive(false);
    }

}