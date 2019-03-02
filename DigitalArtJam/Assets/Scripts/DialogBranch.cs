using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBranch : MonoBehaviour
{
    public DialogItem[] dialogs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        // Take the DialogItem from the children objects
        dialogs = new DialogItem[this.transform.childCount];
        for (int i=0; i < this.transform.childCount; i++) {
            dialogs[i] = this.transform.GetChild(i).GetComponent<DialogItem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
