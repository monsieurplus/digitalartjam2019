using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public DialogUI dialogUI;
    public DialogBranch branch;
    
    private int currentItemIndex;
    private float currentItemStart;
    private DialogItem currentItem;

    // Start is called before the first frame update
    void Start()
    {
        currentItemIndex = 0;
        LoadCurrentItem();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current item is finished
        switch(currentItem.type) {
            case DialogItemType.Talk:
            case DialogItemType.Image:
            case DialogItemType.Typing:
            case DialogItemType.Waiting:
                if (IsCurrentItemFinished())
                    LoadNextItem();
            break;
        }
    }

    public void LoadCurrentItem() {
        currentItem = branch.dialogs[currentItemIndex];
        currentItemStart = Time.time;

        if (currentItem.type == DialogItemType.Action) {
            if (currentItem.answers.Length > 0) {
                currentItem.answers[0].ClickHandler();
            }
        }
        else
            dialogUI.LoadDialogItem(currentItem);
    }

    public void LoadNextItem() {
        currentItemIndex++;
        LoadCurrentItem();
    }

    public void LoadDialogBranch(DialogBranch branch) {
        this.branch = branch;
        currentItemIndex = 0;
        LoadCurrentItem();
    }

    public void ChangeDialogBranch(DialogBranch branch) {
        this.branch = branch;
        currentItemIndex = 0;
    }

    public void ChangeItemIndex(int itemIndex) {
        currentItemIndex = itemIndex;
    }
    
    public DialogItem GetCurrentItem() {
        return currentItem;
    }

    public bool IsCurrentItemFinished() {
        return (Time.time - currentItemStart) > currentItem.duration;
    }
}
