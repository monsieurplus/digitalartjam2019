using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogUIChoice : MonoBehaviour
{
    public GameObject container;
    public TextMeshProUGUI text;
    public DialogAnswer answer;
    private DialogUI dialogUI;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDialogUI(DialogUI dialogUI) {
        this.dialogUI = dialogUI;
    }

    public void ClickHandler() {
        dialogUI.HideChoices();
        dialogUI.AddPlayerTalk(answer.text);

        answer.ClickHandler();
    }

    public void LoadAnswer(DialogAnswer answer) {
        this.answer = answer;
        text.text = answer.text;
    }
}
