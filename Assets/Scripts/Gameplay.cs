using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        CameraController.Instance.CameraChange();
        BoardController.Instance.Initialized();
    }
    
    public void ResetScene()
    {
        ScenceLoader.Instance.ChangeScence(ScenceName.GAME_PLAY);
    }
}
