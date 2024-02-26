using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName;
    public AppsFlyerObjectScript appsFlyerObject; // Ссылка на объект со скриптом AppsFlyerObjectScript

    public void OnButtonClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}