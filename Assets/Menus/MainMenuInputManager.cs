using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainMenuInputManager : MonoBehaviour
{
    public GameData data;
    public TransitionManager transitionManager;

    public GameObject mainMenu;
    public GameObject SettingsMenu;
    public GameObject TutorialMenu;
    
    private MenuController mainMenuController;
    private MenuController pauseMenuController;
    private MenuController tutorialMenuController;

    [SerializeField] private Vector3 defaultPos;
    [SerializeField] private Vector3 slidePos;

    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float delay = 0.5f;

    private bool isOpen = false;
    private bool busy = false;
    
    private void Awake()
    {
        mainMenuController = mainMenu.GetComponent<MenuController>();
        pauseMenuController = SettingsMenu.GetComponent<MenuController>();
        tutorialMenuController = TutorialMenu.GetComponent<MenuController>();
    }

    private void Start()
    {
        defaultPos = mainMenu.transform.localPosition;
    }

    // Main Menu -------------------------------------------------------

    public void OpenCloseSettings()
    {
        //Ignore input if animation is not finished
        if (busy) return;
        StartCoroutine(OpenCloseCoroutine());
    }

    public void OpenCloseSettings(bool open)
    {
        if (busy) return;

        isOpen = !open;
        StartCoroutine(OpenCloseCoroutine());
    }
    
    public void StartSimulation()
    {
        
    }

    public void CloseSimulation()
    {
        //Simply closes the game
        Application.Quit();
    }
    
    // Tutorial Window -------------------------------------------------------

    public void SkipTutorial()
    {
        OpenCloseSettings(false);
        data.NextScene = (int)GameData.SceneIndex.SIMULATION;
        transitionManager.LoadNextScene();
    }


    // -------------------------------------------------------------------
    
    private IEnumerator OpenCloseCoroutine()
    {
        busy = true;
        
        //Closes/opens menu depending on the last action, uses easing for smooth motion
        if (isOpen)
        {
            pauseMenuController.CloseMenu();
            yield return new WaitForSeconds(delay);
            mainMenu.transform.LeanMoveLocal(defaultPos, animationDuration).setEaseInOutBack();
        }
        else
        {
            mainMenu.transform.LeanMoveLocal(slidePos, animationDuration).setEaseInOutBack();
            yield return new WaitForSeconds(delay);
            pauseMenuController.OpenMenu();
        }

        yield return new WaitForSeconds(delay);
        isOpen = !isOpen;
        busy = false;
    }
}
