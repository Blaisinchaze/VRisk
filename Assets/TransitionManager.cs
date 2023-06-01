using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public enum scenes : int
    {
        
    }

    public FadeScreen fade_screen;

    public void GoToScene(int scene_index)
    {
        StartCoroutine(GoToSceneRoutine(scene_index));
    }

    IEnumerator GoToSceneRoutine(int scene_index)
    {
        fade_screen.FadeOut();
        yield return new WaitForSeconds(fade_screen.fade_duration);
    }
    
    public void GoToSceneAsync(int scene_index)
    {
        StartCoroutine(GoToSceneAsyncRoutine(scene_index));
    }

    IEnumerator GoToSceneAsyncRoutine(int scene_index)
    {
        fade_screen.FadeOut();

        AsyncOperation operation = SceneManager.LoadSceneAsync(scene_index);
        operation.allowSceneActivation = false;

        float timer = 0;
        while(timer <= fade_screen.fade_duration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }
}
