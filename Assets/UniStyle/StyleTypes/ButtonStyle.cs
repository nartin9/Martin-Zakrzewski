using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stores style configuration for a button UI element.
/// </summary>
public class ButtonStyle : MonoBehaviour
{
    public TransitionAttributes transition;
    public ImageAttributes imageStyle;
    public TextAttributes textStyle;
    public Text buttonText;
#if UniStyle_TMPPro
    public TMPro.TextMeshProUGUI buttonTMPText;
#endif
    public Image buttonImage;

}