using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] Camera cameraController;

    [Range(1, 15)]
    [SerializeField] private float sizeEasy, sizeMedium, sizeHard;

    public void CameraChange()
    {
        GameDifficulty gameDifficulty = GameManager.Instance.CurrentDifficulty();

        switch (gameDifficulty)
        {
            case GameDifficulty.EASY:
                cameraController.orthographicSize = sizeEasy;
                break;
            case GameDifficulty.MEDIUM:
                cameraController.orthographicSize = sizeMedium;
                break;
            case GameDifficulty.HARD:
                cameraController.orthographicSize = sizeHard;
                break;
        }
        
    }
}
