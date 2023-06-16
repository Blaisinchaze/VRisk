using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    
    private MenuController mainMenuController;
    private MenuController settingsMenuController;

    [SerializeField] private Vector3 defaultPos;
    [SerializeField] private Vector3 slidePos;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private float lerpSpeed = 3.5f;
    
    [SerializeField] private float openDelay = 0.5f;
    [SerializeField] private float closeDelay = 0.5f;

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

    public void OpenCloseSettings()
    {
        if (busy) return;

        StartCoroutine(OpenCloseCoroutine());
        isOpen = !isOpen;
    }

    private void Update()
    {
        if (!VectorHelper.ApproximatelyEqual(mainMenu.transform.localPosition, targetPos, 0.01f))
        {
            mainMenu.transform.localPosition =
                Vector3.Lerp(mainMenu.transform.localPosition, targetPos, lerpSpeed * Time.deltaTime);
            busy = true;
        }
        else
        {
            busy = false;
        }
    }

    private IEnumerator OpenCloseCoroutine()
    {
        if (isOpen)
        {
            settingsMenuController.CloseMenu();
            yield return new WaitForSeconds(closeDelay);
            targetPos = defaultPos;
        }
        else
        {
            targetPos = slidePos;
            yield return new WaitForSeconds(openDelay);
            settingsMenuController.OpenMenu();
        }
    }
}
