using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameDifficulty currentDifficulty;

    private BoardData _boardData;

    public void SetCurrentDifficulty(GameDifficulty diff)
    {
        currentDifficulty = diff;
        _boardData = new BoardData();
        switch (currentDifficulty)
        {
            case GameDifficulty.EASY:
                SetBoardData(10, 10, 10);
                break;
            case GameDifficulty.MEDIUM:
                SetBoardData(18 , 18, 40);
                break;
            case GameDifficulty.HARD:
                SetBoardData(24, 24, 99);
                break;
        }
    }

    public GameDifficulty CurrentDifficulty()
    {
        return currentDifficulty;
    }

    public BoardData GetBoardData => _boardData;

   
   
    void SetBoardData(int width, int height, int nunMines)
    {
        _boardData.width = width;
        _boardData.height = height;
        _boardData.numMines = nunMines;
    }  
   
    public void ShowWinGame()
    {
        GameOverScreen.Instance.SetUp();
    }

    public void ShowLoseGame()
    {
        GameOverScreen.Instance.SetUp();
    }
}

public struct BoardData
{
    public int width;
    public int height;
    public int numMines;
}