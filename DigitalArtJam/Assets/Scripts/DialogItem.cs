using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable]
public struct DialogAnswer {
    [TextArea]
    public string text;
    public UnityEvent myUnityEvent;

    public void ClickHandler() {
        myUnityEvent.Invoke();
    }
}

public enum DialogItemType { Talk, Image, Question, Waiting, Typing, Action };

public class DialogItem : MonoBehaviour
{
    public DialogItemType type;

    [TextArea]
    public string text;
    public DialogAnswer[] answers;
    public Sprite image;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
