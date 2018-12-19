using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stores style configuration for a slider UI element.
/// </summary>
public class SliderStyle : MonoBehaviour
{

    public TransitionAttributes transition;
    public bool setWidth = false;
    public bool setHeight = true;
    public ImageAttributes backgroundStyle;
    public Image backgroundImage;
    public ImageAttributes fillStyle;
    public Image fillImage;
    public ImageAttributes handleStyle;
    public Image handleImage;
}