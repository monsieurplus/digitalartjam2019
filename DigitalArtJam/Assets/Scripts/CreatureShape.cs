using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatureLegType { No, Four, Tall, Small };

public class CreatureShape : MonoBehaviour
{
    public CreatureFinal notLegged;
    public CreatureFinal fourLegged;
    public CreatureFinal tallLegged;
    public CreatureFinal smallLegged;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        ChooseLegType(CreatureLegType.No);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CreatureFinal ChooseLegType(CreatureLegType legType) {
        Debug.Log(legType);

        CreatureFinal final = GetLegType(legType);

        notLegged.gameObject.SetActive(false);
        Debug.Log(notLegged.gameObject);
        final.gameObject.SetActive(true);
        Debug.Log(final.gameObject);

        return final;
    }

    public CreatureFinal GetLegType(CreatureLegType legType) {
        switch (legType) {
            case CreatureLegType.No:
                return notLegged;

            case CreatureLegType.Four:
                return fourLegged;

            case CreatureLegType.Tall:
                return tallLegged;

            case CreatureLegType.Small:
                return smallLegged;
        }

        return notLegged;
    } 
}
