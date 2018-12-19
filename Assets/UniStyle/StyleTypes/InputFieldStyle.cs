using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stores style configuration for an input field UI element.
/// </summary>
public class InputFieldStyle : MonoBehaviour
{

    public TransitionAttributes transition;
    public bool setCaretBlinkRate = true;
    public bool setCaretWidth = true;
    public bool setCaretColor = true;
    public bool setSelectionColor = true;
    public bool setReadOnly = false;
    public ImageAttributes backgroundStyle;
    public Image backgroundImage;
    public TextAttributes placeholderStyle;
    public Text placeholderText;
#if UniStyle_TMPPro
    public TMPro.TextMeshProUGUI placeholderTMPText;
#endif
    public TextAttributes textfieldStyle;
    public Text textfieldText;
#if UniStyle_TMPPro
    public TMPro.TextMeshProUGUI textfieldTMPText;
#endif

}