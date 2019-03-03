using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    public CreatureShape roundCreature;
    public CreatureShape squareCreature;

    private CreatureShape currentShape;
    private CreatureFinal currentFinal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        roundCreature.gameObject.SetActive(false);
        squareCreature.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseRound() {
        currentShape = roundCreature;
        currentShape.gameObject.SetActive(true);
    }

    public void ChooseSquare() {
        currentShape = squareCreature;
        currentShape.gameObject.SetActive(true);
    }

    public void ChooseLegType(int legType) {
        currentFinal = currentShape.ChooseLegType((CreatureLegType)legType);
    }

    public void ChooseSenseType(int senseType) {
        currentFinal.ChooseSenseType((CreatureSenseType)senseType);
    }
}
