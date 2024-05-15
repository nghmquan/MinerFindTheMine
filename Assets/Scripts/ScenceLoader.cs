using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceLoader : Singleton<ScenceLoader>
{
    private string GetScenceName(ScenceName scenceName)
    {   
        switch(scenceName){
            case ScenceName.MAIN_MENU:
                return "MainMenu";
            case ScenceName.CHOOSE_LEVEL:
                return "LevelSelect";
            case ScenceName.GAME_PLAY:
                return "Gameplay";               
        }
        return "";
    }
    public void ChangeScence(ScenceName scenceName)
    {
        string sceneNameStr = GetScenceName(scenceName);
        if(sceneNameStr == "")
        {
            Debug.LogError("Sai ten Scene");
            return;
        }
        SceneManager.LoadScene(sceneNameStr);
    }

    
}

public enum ScenceName
{
    NONE = -1,
    MAIN_MENU = 0,
    CHOOSE_LEVEL = 1,
    GAME_PLAY = 2,
}
