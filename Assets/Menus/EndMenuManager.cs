using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenuManager : MonoBehaviour
{
    public GameData data;
    public TransitionManager transitionManager;

    void ExitApplication()
    {
    }

    void BackToMainMenu()
    {
        data.NextScene = (int)GameData.SceneIndex.SIMULATION;
        transitionManager.LoadNextScene();
    }
}
