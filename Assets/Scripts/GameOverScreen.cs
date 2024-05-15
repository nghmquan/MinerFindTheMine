using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : Singleton<GameOverScreen>
{
    [SerializeField] GameObject gameoverPanel;

    public void SetUp()
    {
        gameoverPanel.SetActive(true);
    }

    public void RestartButton()
    {
        ScenceLoader.Instance.ChangeScence(ScenceName.CHOOSE_LEVEL);
    }
}
