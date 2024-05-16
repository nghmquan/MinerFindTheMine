using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChooseLevel : MonoBehaviour
{
    [SerializeField] private Button btnEasy, btnMedium, btnHard;

    private void Start()
    {
        btnEasy.onClick.AddListener(StartEasyLevel);
        btnMedium.onClick.AddListener(StartMediumLevel);
        btnHard.onClick.AddListener(StartHardLevel);
    }

    private void StartLevel(GameDifficulty difficulty)
    {
        GameManager.Instance.SetCurrentDifficulty(difficulty);
        ScenceLoader.Instance.ChangeScence(ScenceName.GAME_PLAY);
    }

    public void StartEasyLevel()
    {
        StartLevel(GameDifficulty.EASY);
    }

    public void StartMediumLevel()
    {
        StartLevel(GameDifficulty.MEDIUM);
    }

    public void StartHardLevel()
    {
        StartLevel(GameDifficulty.HARD);
    }
}

public enum GameDifficulty
{
    EASY =0,
    MEDIUM =1,
    HARD =2,
}
