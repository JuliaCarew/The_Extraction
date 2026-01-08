using UnityEngine;
using System; 

public class ScoreSystem 
{
    private int maxScore = 100000; 

    private int Score; 

    public int score 
    {
        get { return Score; }
        set { Score = Math.Max(0, Math.Min(maxScore, value)); }
    }

    public void IncreaseScore(int iScore) 
    {
        score += iScore; 
    }

    public void SetMaxScore(int mScore) 
    {
        maxScore = mScore; 
    }

}
