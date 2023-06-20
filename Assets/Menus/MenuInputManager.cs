using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputManager : MonoBehaviour
{
    public GameData data;
    public TransitionManager transitionManager;
    
    public GameObject mainMenu;
    public GameObject settingsMenu;
    
    private MenuController mainMenuController;
    private MenuController settingsMenuController;

    [SerializeField] private Vector3 defaultPos;
    [SerializeField] private Vector3 slidePos;

    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float delay = 0.5f;

    private bool isOpen = false;
    private bool busy = false;
    
    private void Awake()
    {
        mainMenuController = mainMenu.GetComponent<MenuController>();
        settingsMenuController = settingsMenu.GetComponent<MenuController>();
    }

    private void Start()
    {
        defaultPos = mainMenu.transform.localPosition;
    }

    public void StartSimulation()
    {
        data.NextScene = (int)GameData.SceneIndex.MIAN_MENU;
        transitionManager.LoadNextScene();
    }
    
    public void OpenCloseSettings()
    {
        //Ignore input if animation is not finished
        if (busy) return;

        StartCoroutine(OpenCloseCoroutine());
        isOpen = !isOpen;
    }

    public void CloseSimulation()
    {
        //Simply closes the game
        Application.Quit();
    }

    // --------------------------------------------------------------------

    private IEnumerator OpenCloseCoroutine()
    {
        busy = true;
        
        //Closes/opens menu depending on the last action, uses easing for smooth motion
        if (isOpen)
        {
            settingsMenuController.CloseMenu();
            yield return new WaitForSeconds(delay);
            mainMenu.transform.LeanMoveLocal(defaultPos, animationDuration).setEaseInOutBack();
        }
        else
        {
            mainMenu.transform.LeanMoveLocal(slidePos, animationDuration).setEaseInOutBack();
            yield return new WaitForSeconds(delay);
            settingsMenuController.OpenMenu();
        }

        yield return new WaitForSeconds(delay);
        busy = false;
    }
}
