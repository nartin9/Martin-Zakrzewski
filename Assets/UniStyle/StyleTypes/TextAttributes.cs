using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores style configuration for all text elements.
/// </summary>
[System.Serializable]
public class TextAttributes
{

    public bool setTextSize = true;
    public bool setAutoSize = false;
    public bool setAutoSizeMax = false;
    public bool setAutoSizeMin = false;
    public bool setTextFont = true;
    public bool setTextColor = true;
    public bool setTextStyle = true;
    public bool setAlignment = true;
    public bool setHeight = false;
    public bool setWidth = false;
    public bool setAnchors = false;

}