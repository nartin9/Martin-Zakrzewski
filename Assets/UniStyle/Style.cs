using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Definition of a UniStyle style with all related prefabs.
/// </summary>
public class Style : MonoBehaviour {

    public List<GameObject> texts;
    public List<GameObject> images;
    public List<GameObject> buttons;
    public List<GameObject> toggles;
    public List<GameObject> sliders;
    public List<GameObject> scrollViews;
    public List<GameObject> scrollBars;
    public List<GameObject> dropdowns;
    public List<GameObject> inputFields;

    /// <summary>
    /// Initialize style prefab lists when created.
    /// </summary>
    Style()
    {
        texts = new List<GameObject>();
        images = new List<GameObject>();
        buttons = new List<GameObject>();
        toggles = new List<GameObject>();
        sliders = new List<GameObject>();
        scrollViews = new List<GameObject>();
        scrollBars = new List<GameObject>();
        dropdowns = new List<GameObject>();
        inputFields = new List<GameObject>();
    }

}
