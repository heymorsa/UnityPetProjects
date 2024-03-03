using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void FreezeGame() {
        Time.timeScale = 0;
    }
    public void UnfreezeGame() {
        Time.timeScale = 1;
    }
    public void CloseGame() {
        TransformToSceneNum(0);
    }

    public void TransformToSceneNum(int value) {
        if (value >= SceneManager.sceneCountInBuildSettings) value = 0;
        SceneManager.LoadScene(value);
    }

}
