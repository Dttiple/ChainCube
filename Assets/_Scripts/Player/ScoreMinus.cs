using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ScoreMinus : MonoBehaviour
{
    public int points = 20;
    [Inject]
    private Player player;
    [Inject]
    public void ScoreSubtrack(int points)
    {
        player.CurrentScore.Value -= points;
    }
}

