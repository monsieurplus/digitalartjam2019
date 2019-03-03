using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatureSenseType { No, Eye, Ear, Mouth }

public class CreatureFinal : MonoBehaviour
{
    public GameObject eyes;
    public GameObject ears;
    public GameObject mouth;
    public GameObject legs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        HideAllSense();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseSenseType(CreatureSenseType senseType) {
        HideAllSense();

        switch (senseType) {
            case CreatureSenseType.No:
            break;

            case CreatureSenseType.Eye:
                eyes.SetActive(true);
            break;

            case CreatureSenseType.Ear:
                ears.SetActive(true);
            break;

            case CreatureSenseType.Mouth:
                mouth.SetActive(true);
            break;
        }
    } 

    private void HideAllSense() {
        eyes.SetActive(false);
        ears.SetActive(false);
        mouth.SetActive(false);
    }
}
