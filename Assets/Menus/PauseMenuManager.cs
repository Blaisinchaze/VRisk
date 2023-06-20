using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    public MovementController movementController;
    public ViewController viewController;
    public ControllerRaycastHandler raycastHandler;
    public UIAnchor canvasAnchor;
    public PauseFadeManager pauseFadeManager;
    public MenuInputManager menuInputManager;

    public bool menuIsOpen = false;
    
    private InputAction pausePressed;

    private bool busy = false;
    
    private void Start()
    {
        pausePressed = GameManager.Instance.InputHandler.input_asset.InputActionMap.Pause;
    }
    
    void Update()
    {
        if (pausePressed.triggered && !busy)
        {
            StartCoroutine(OpenCloseMenuCoroutine());
        }
    }
    
    public void CloseMenu()
    {
        if (!busy)
        {
            menuIsOpen = true;
            StartCoroutine(OpenCloseMenuCoroutine());
        }
    }
    
    public void OpenMenu()
    {
        if (!busy)
        {
            menuIsOpen = false;
            StartCoroutine(OpenCloseMenuCoroutine());
        }
    }

    // --------------------------------------------------------------------

    private IEnumerator OpenCloseMenuCoroutine()
    {
        busy = true;

        if (!menuIsOpen)
        {
            pauseFadeManager.FadeIn();
            canvasAnchor.PopIn();
        }
        else
        {
            pauseFadeManager.FadeOut();
            canvasAnchor.PopOut();
            menuInputManager.CloseSettings();
        }
        
        if(menuIsOpen) yield return new WaitForSeconds(canvasAnchor.animation_time * 1.15f);
        
        //Stops movement and enables ray casting
        movementController.enabled = menuIsOpen;
        viewController.enabled = menuIsOpen;
        raycastHandler.ChangeState(!menuIsOpen);
        
        busy = false;
        menuIsOpen = !menuIsOpen;
    }
}
