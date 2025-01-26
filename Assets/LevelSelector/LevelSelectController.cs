using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    private int cursorMovement;

    // Update is called once per frame
    void LateUpdate()
    {
        TutorialLevelMethod();
        SugarLevelMethod();
        BallLevelMethod();
        TeaLevelMethod();
    }

    void TutorialLevelMethod()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Delete the string and replace it with the actual level's name :)
            SceneManager.LoadScene(2);
        }
    }

    void SugarLevelMethod()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Delete the string and replace it with the actual level's name :)
            SceneManager.LoadScene(5);
        }
    }

    void BallLevelMethod()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Delete the string and replace it with the actual level's name :)
            SceneManager.LoadScene(3);
        }
    }

    void TeaLevelMethod()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Delete the string and replace it with the actual level's name :)
            SceneManager.LoadScene(4);
        }
    }
}
