using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stores style configuration for a scroll view UI element.
/// </summary>
public class ScrollViewStyle : MonoBehaviour
{

    public bool setHorizontal = true;
    public bool setVertical = true;
    public bool setMovementType = true;
    public bool setInertia = true;
    public bool setScrollSensitivity = true;
    public bool setHorizontalVisibilty = true;
    public bool setHorizontalSpacing = true;
    public bool setVerticalVisibility = true;
    public bool setVerticalSpacing = true;
    public ImageAttributes backgroundStyle;
    public Image backgroundImage;
    public ImageAttributes viewportStyle;
    public Image viewportImage;

}