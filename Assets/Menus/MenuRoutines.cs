using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRoutines : MonoBehaviour
{
    public GameData data;
    public TransitionManager transition_manager;

    public void StartButton()
    {
        data.NextScene = 0;
        transition_manager.GoToScene(1);
    }
}
