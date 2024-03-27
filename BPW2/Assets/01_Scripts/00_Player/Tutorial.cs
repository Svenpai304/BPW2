using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private string[] tutorials;
    [SerializeField] private TMP_Text tutorialText;

    public static Tutorial instance;
    private int currentStage = 0;
    private bool finished;


    private void Start()
    {
        instance = this;
        tutorialText.text = tutorials[currentStage];
    }
    public void AdvanceTutorial(int stage)
    {
        if (finished || currentStage != stage) return;

        currentStage++;
        if (currentStage < tutorials.Length)
        {
            tutorialText.text = tutorials[currentStage];
        }
        else
        {
            gameObject.SetActive(false);
            currentStage = 1000;
        }
    }
}
