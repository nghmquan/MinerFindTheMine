using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChooseLevel : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Tìm GameManager trong scene và gán vào gameManager
    }

    public void StartEasyLevel()
    {
        SceneManager.LoadScene(2);
        gameManager.CreateGameBoard(10, 10, 10);
    }

    public void StartMediumLevel()
    {
        SceneManager.LoadScene(3);
        gameManager.CreateGameBoard(18, 18, 40);
    }

    public void StartHardLevel()
    {
        SceneManager.LoadScene(4);
        gameManager.CreateGameBoard(24, 24, 99);
    }
}
