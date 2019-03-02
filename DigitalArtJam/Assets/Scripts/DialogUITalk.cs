using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogUITalk : MonoBehaviour
{
    public TextMeshProUGUI text;

    public Sprite firstMessageSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set the text to the text
    public void SetText(string text) {
        this.text.text = text;
    }

    public void MakeFirstMessage() {
        this.GetComponent<Image>().sprite = firstMessageSprite;
    }
}
