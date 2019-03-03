using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    public GameObject dialogContainer;
    public GameObject dialogContent;

    public GameObject choiceContainer;
    public DialogUIChoice[] choices;

    public GameObject prefabBotTalk;
    public GameObject prefabPlayerTalk;
    public GameObject prefabChoice;
    public GameObject prefabTyping;

    public AudioSource uiSoundSource;
    public AudioClip botTalkClip;
    public AudioClip botTypeClip;
    public AudioClip playerTalkClip;

    private DialogItem currentDialogItem;
    private DialogItemType currentDialogItemType;
    private DialogItemType previousDialogItemType;
    private string previousDialogActor = "";

    private GameObject typingInstance;
 
    private int scrollToBottom = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        // Init choices
        choices = new DialogUIChoice[4];
        for (int i=0; i < 4; i++) {
            GameObject choice = GameObject.Instantiate(prefabChoice);
            choice.transform.SetParent(choiceContainer.transform);
            choice.SetActive(false);

            choices[i] = choice.GetComponent<DialogUIChoice>();
            choices[i].SetDialogUI(this);
        }
        choiceContainer.SetActive(false);

        // Init Typing instance
        typingInstance = GameObject.Instantiate(prefabTyping);
        typingInstance.SetActive(false);
        typingInstance.transform.SetParent(dialogContent.transform);
        typingInstance.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update() {}

    void LateUpdate() {
        UpdateDialogContainerBottom();

        if (scrollToBottom > 0) {
            scrollToBottom++;
            scrollToBottom %= 10;
            ScrollToBottom();
        }
    }

    private void ScrollToBottom() {
        float dialogContainerHeight = dialogContainer.GetComponent<RectTransform>().rect.height;
        float dialogContentHeight = dialogContent.GetComponent<RectTransform>().rect.height;

        if (dialogContainerHeight < dialogContentHeight)
            SetDialogContentY(dialogContentHeight - dialogContainerHeight);
    }

    private void SetDialogContentY(float y) {
        Vector3 dialogContentPosition = dialogContent.transform.localPosition;
        dialogContentPosition.y = y;
        dialogContent.transform.localPosition = dialogContentPosition;
    }

    private void UpdateDialogContainerBottom() {
        // Move the dialog container up to the size of the choices panel
        float choiceContainerHeight = choiceContainer.GetComponent<RectTransform>().rect.height;
        if (!choiceContainer.activeInHierarchy)
            choiceContainerHeight = 0f;
        
        SetDialogContainerBottom(choiceContainerHeight);
    }

    private void SetDialogContainerBottom(float y)
    {
        RectTransform rt = dialogContainer.GetComponent<RectTransform>();
        rt.offsetMin = new Vector2( rt.offsetMin.x, y );
    }

    public void LoadDialogItem(DialogItem dialogItem) {
        if (currentDialogItem) {
            previousDialogItemType = currentDialogItem.type;
        }

        if (previousDialogItemType == DialogItemType.Typing)
            HideTyping();
        
        currentDialogItem = dialogItem;
        currentDialogItemType = dialogItem.type;

        switch (dialogItem.type) {
            case DialogItemType.Talk:
                LoadTalk(dialogItem);
            break;

            case DialogItemType.Question:
                LoadQuestion(dialogItem);
            break;

            case DialogItemType.Typing:
                LoadTyping();
            break;
        }
    }

    private void LoadTalk(DialogItem dialogItem) {
        GameObject newDialog = GameObject.Instantiate(prefabBotTalk);
        newDialog.transform.SetParent(dialogContent.transform);
        newDialog.transform.localScale = Vector3.one;

        newDialog.GetComponent<DialogUITalk>().SetText(dialogItem.text);
        if (previousDialogActor != "bot")
            newDialog.GetComponent<DialogUITalk>().MakeFirstMessage();
        previousDialogActor = "bot";

        uiSoundSource.PlayOneShot(botTalkClip);

        scrollToBottom++;
    }

    private void LoadQuestion(DialogItem dialogItem) {
        LoadTalk(dialogItem);

        for (int i=0; i < dialogItem.answers.Length; i++) {
            choices[i].container.SetActive(true);
            choices[i].LoadAnswer(dialogItem.answers[i]);
        }
        choiceContainer.SetActive(true);

        scrollToBottom++;
    }

    

    private void LoadTyping() {
        typingInstance.SetActive(true);
        typingInstance.transform.SetAsLastSibling();

        uiSoundSource.PlayOneShot(botTypeClip);

        scrollToBottom++;
    }

    private void HideTyping() {
        typingInstance.SetActive(false);
    }

    public void HideChoices() {
        choiceContainer.SetActive(false);

        for (int i=0; i < choices.Length; i++) {
            choices[i].container.SetActive(false);
        }

        SetDialogContainerBottom(0);

        scrollToBottom++;
    }

    public void AddPlayerTalk(string text) {
        GameObject newDialog = GameObject.Instantiate(prefabPlayerTalk);
        newDialog.transform.SetParent(dialogContent.transform);
        newDialog.transform.localScale = Vector3.one;

        newDialog.GetComponent<DialogUITalk>().SetText(text);
        previousDialogActor = "player";

        uiSoundSource.PlayOneShot(playerTalkClip);

        scrollToBottom++;
    }
}
