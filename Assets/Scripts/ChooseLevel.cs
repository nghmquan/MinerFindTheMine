using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChooseLevel : MonoBehaviour
{   

    public void StartEasyLevel()
    {
        // GameManager.Instance.CreateGameBoard(10, 10, 10);
        GameManager.Instance.SetCurrentDifficulty(GameDifficulty.EASY);
        ScenceLoader.Instance.ChangeScence(ScenceName.GAME_PLAY);
    }

    public void StartMediumLevel()
    {
        // gameManager.CreateGameBoard(18, 18, 40);
        GameManager.Instance.SetCurrentDifficulty(GameDifficulty.MEDIUM);
        ScenceLoader.Instance.ChangeScence(ScenceName.GAME_PLAY);
    }

    public void StartHardLevel()
    {
        // gameManager.CreateGameBoard(24, 24, 99);
        GameManager.Instance.SetCurrentDifficulty(GameDifficulty.HARD);
        ScenceLoader.Instance.ChangeScence(ScenceName.GAME_PLAY);
    }
}

public enum GameDifficulty
{
    EASY =0,
    MEDIUM =1,
    HARD =2,
}
