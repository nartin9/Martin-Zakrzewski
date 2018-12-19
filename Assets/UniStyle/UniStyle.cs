using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static class to provide global access to the StyleManager and additional information.
/// </summary>
/// <see cref="StyleManager"/>
public static class UniStyle
{

    public const int CATEGORY_TEXT = 0;
    public const int CATEGORY_IMAGE = 1;
    public const int CATEGORY_BUTTON = 2;
    public const int CATEGORY_TOGGLE = 3;
    public const int CATEGORY_INPUTFIELD = 4;
    public const int CATEGORY_SLIDER = 5;
    public const int CATEGORY_SCROLLVIEW = 6;
    public const int CATEGORY_SCROLLBAR = 7;
    public const int CATEGORY_DROPDOWN = 8;

    //StyleManager component of the current scene
    public static StyleManager ActiveStyle;
    //Script that copies properties of reference elements to target elements
    public static SetStyleAttributes CopyAttributes = new SetStyleAttributes();

}