using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour
{
    
    public void Execute()
    {
        LevelManager.LevelLoad(1);
    }
}
