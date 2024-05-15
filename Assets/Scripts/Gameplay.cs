using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform tileHolder;
    void Start()
    {
        CameraController.Instance.CameraChange();
        GameManager.Instance.Initialized(tileHolder);
    }  
}
