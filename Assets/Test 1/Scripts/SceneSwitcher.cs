using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SwitchToScene(string sceneName)
    {
        StaticData.textData = text.text;
        SceneManager.LoadScene(sceneName);
    }
}
