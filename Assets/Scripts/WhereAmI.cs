using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhereAmI : MonoBehaviour
{
    public static WhereAmI instance;

    public int level = -1;
    public int usingBall = 0;

    [SerializeField]
    private GameObject gameManager, uiManager;
    private float orthoSize = 5;
    private float aspect = 1.75f;

    public void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }

        usingBall = PlayerPrefs.GetInt("using_ball");
  
        SceneManager.sceneLoaded += LevelVerify;
    }

    void LevelVerify(Scene scene, LoadSceneMode mode)
    {
        level = scene.buildIndex;

        if (level > 2) {
            Instantiate(gameManager);
            Instantiate(uiManager);
            // Buscar alternativa. Essa está bugando e fazendo sumir o canvas.
            //Camera.main.projectionMatrix = Matrix4x4.Ortho(
            //    - orthoSize * aspect,
            //    orthoSize * aspect,
            //    - orthoSize,
            //    orthoSize,
            //    Camera.main.nearClipPlane,
            //    Camera.main.farClipPlane
            //);
        }
    }
}
