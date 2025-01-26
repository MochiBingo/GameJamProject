using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSelector : MonoBehaviour
{
    public void OnPlayGame()
    {
        SceneManager.LoadScene("LevelSelectorScene");
    }
}
