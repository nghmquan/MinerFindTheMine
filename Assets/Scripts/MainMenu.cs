using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnClickPlayGame()
    {
        ScenceLoader.Instance.ChangeScence(ScenceName.CHOOSE_LEVEL);
    }
}
