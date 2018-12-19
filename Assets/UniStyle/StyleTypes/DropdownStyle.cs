using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stores style configuration for a dropdown UI element.
/// </summary>
public class DropdownStyle : MonoBehaviour
{

    public TransitionAttributes transition;
    public ImageAttributes backgroundStyle;
    public Image backgroundImage;
    public TextAttributes captionTextStyle;
    public Text captionText;
#if UniStyle_TMPPro
    public TMPro.TextMeshProUGUI captionTMPText;
#endif
    public TextAttributes itemTextStyle;
    public Text itemText;
#if UniStyle_TMPPro
    public TMPro.TextMeshProUGUI itemTMPText;
#endif
    public ImageAttributes arrowStyle;
    public Image arrowImage;
    public ImageAttributes captionStyle;
    public Image captionImage;
    public ImageAttributes itemStyle;
    public Image itemImage;
    public ImageAttributes itemBackgroundStyle;
    public Image itemBackgroundImage;
    public ImageAttributes itemCheckmarkStyle;
    public Image itemCheckmarkImage;

}