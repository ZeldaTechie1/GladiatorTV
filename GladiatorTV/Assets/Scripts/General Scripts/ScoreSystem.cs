using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{

    public class ScoreEvent : UnityEngine.Events.UnityEvent<int> { };
    public ScoreEvent scoreEvent = new ScoreEvent();
    int score = 0;

    // Use this for initialization
    void Start()
    {
        scoreEvent.AddListener(addScore);
    }

    void addScore(int scoreToAdd)
    {
        this.score += scoreToAdd;
    }
}
