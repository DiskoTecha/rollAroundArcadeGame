using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Text currentScore;
    public Text highScore;
    public Text objectives;
    public Text level;

    public PlayerController playerController;
    public GameController gameController;
    public ScoreController scoreController;

    private string currentObjectives;
    private string totalObjectives;

    // Start is called before the first frame update
    void Start()
    {
        currentScore.text = scoreController.GetCurrentScore().ToString();
        highScore.text = scoreController.GetHighScore().ToString();
        currentObjectives = playerController.GetCurrentObjectiveAmount().ToString();
        totalObjectives = gameController.objectivesNeeded.ToString();
        objectives.text = currentObjectives + "/" + totalObjectives;
        level.text = gameController.GetCurrentLevel().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        string readCurrentScore = scoreController.GetCurrentScore().ToString();
        string readHighScore = scoreController.GetHighScore().ToString();
        string readCurrentObjectives = playerController.GetCurrentObjectiveAmount().ToString();
        string readTotalObjectives = gameController.objectivesNeeded.ToString();
        string readLevel = gameController.GetCurrentLevel().ToString();

        if (currentScore.text != readCurrentScore)
        {
            currentScore.text = readCurrentScore;
        }
        if (highScore.text != readHighScore)
        {
            highScore.text = readHighScore;
        }
        if (objectives.text != readCurrentObjectives + "/" + readTotalObjectives)
        {
            objectives.text = readCurrentObjectives + "/" + readTotalObjectives;
        }
        if (level.text != readLevel)
        {
            level.text = readLevel;
        }
    }
}
