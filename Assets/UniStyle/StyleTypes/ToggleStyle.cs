using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stores style configuration for a toggle UI element.
/// </summary>
public class ToggleStyle : MonoBehaviour
{

    public TransitionAttributes transition;
    public ImageAttributes backgroundStyle;
    public ImageAttributes checkmarkStyle;
    public TextAttributes labelStyle;
    public Image backgroundImage;
    public Image checkmarkImage;
    public Text labelText;
#if UniStyle_TMPPro
    public TMPro.TextMeshProUGUI labelTMPText;
#endif

}