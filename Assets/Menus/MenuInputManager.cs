using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputManager : MonoBehaviour
{
    public MenuController mainMenuController;
    public MenuController optionsMenuController;

    public void OpenSettings()
    {
        StartCoroutine(AnimationExecuter(mainMenuController, optionsMenuController));
    }

    public void CloseSettings()
    {
        StartCoroutine(AnimationExecuter(optionsMenuController, mainMenuController));
    }

    private IEnumerator AnimationExecuter(MenuController controller1, MenuController controller2)
    {
        controller1.CloseMenu();
        yield return new WaitForSeconds(controller1.animationTime);
        controller2.OpenMenu();
        
        yield return null;
    }
}
