using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject playerRig;
    public GameObject menuAnchor;
    public ViewController viewController;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 targetPos;

    [SerializeField] private float yOffset = 2.0f;
    [SerializeField] private float delay = 3.0f;
    [SerializeField] private float moveTime = 3.5f;
    [SerializeField] private float scaleTime = 3.5f;

    private void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(PositionTutorial());
    }

    public void StartTutorial()
    {
        StartCoroutine(StartTutorialCoroutine());
    }

    private IEnumerator StartTutorialCoroutine()
    {
        transform.LeanScale(new Vector3(1, 1, 1), scaleTime).setEaseOutQuart();
        transform.LeanMoveLocal(targetPos, moveTime).setEaseOutQuart();

        yield return new WaitForSeconds(moveTime > scaleTime ? moveTime : scaleTime);
        
        //Adds a rigidbody with the same stats as the one in the simulation
        var rigidBody = playerRig.AddComponent<Rigidbody>();
        rigidBody.mass = 75;
        rigidBody.drag = 0;
        rigidBody.angularDrag = 0.05f;
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
        rigidBody.freezeRotation = true;

        //Enables movement
        playerRig.AddComponent<MovementController>();
        viewController.enabled = true;

        yield return null;
    }

    private IEnumerator PositionTutorial()
    {
        yield return new WaitForSeconds(delay);

        Vector3 anchorPos = menuAnchor.transform.position;
        targetPos = new Vector3(anchorPos.x, anchorPos.y + yOffset, anchorPos.z);
        transform.position = new Vector3(targetPos.x, startPos.y, targetPos.z);
    }
}
