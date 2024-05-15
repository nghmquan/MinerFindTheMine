using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] Camera camera;

    [Range(1, 15)]
    [SerializeField] private float sizeEasy, sizeMedium, sizeHard;

    public void CameraChange()
    {
        GameDifficulty gameDifficulty = GameManager.Instance.CurrentDifficulty();

        switch (gameDifficulty)
        {
            case GameDifficulty.EASY:
                camera.orthographicSize = sizeEasy;
                break;
            case GameDifficulty.MEDIUM:
                camera.orthographicSize = sizeMedium;
                break;
            case GameDifficulty.HARD:
                camera.orthographicSize = sizeHard;
                break;
        }
        
    }
}
