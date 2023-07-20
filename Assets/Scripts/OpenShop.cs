using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenShop : MonoBehaviour
{
    public void Execute()
    {
        LevelManager.LevelLoad(2);
    }
}
