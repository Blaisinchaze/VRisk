using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TutorialManager : MonoBehaviour
{
    public GameObject playerRig;
    public GameObject menuAnchor;
    public GameObject standingPlane;
    public ViewController viewController;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 targetPos;

    [SerializeField] private float minScaleTime = 4.0f;
    [SerializeField] private float maxScaleTime = 6.0f;
    
    [SerializeField] private float increaseIntervalTime = 0.01f;
    [SerializeField] private float increaseIntervalSize = 0.04f;
    
    [SerializeField] private float yOffset = 2.0f;
    [SerializeField] private float delay = 3.0f;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.localScale = new Vector3(0, 0, 0);
        }
        
        StartCoroutine(PositionTutorial());
    }

    public void StartTutorial()
    {
        StartCoroutine(StartTutorialCoroutine());
    }

    private IEnumerator StartTutorialCoroutine()
    {
        StartCoroutine(SoftIncreaseCircle());

        yield return new WaitForSeconds(1f);
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            
            float popUpTime = Random.Range(minScaleTime, maxScaleTime);

            child.LeanScale(new Vector3(1, 1, 1), popUpTime).setEaseOutQuart();
        }

        yield return new WaitForSeconds(maxScaleTime);
        
        //Adds a rigidbody with the same stats as the one in the simulation
        var rigidBody = playerRig.AddComponent<Rigidbody>();
        rigidBody.mass = 75;
        rigidBody.drag = 0;
        rigidBody.angularDrag = 0.05f;
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
        rigidBody.freezeRotation = true;

        //Enables movement
        viewController.enabled = true;
        playerRig.GetComponent<MovementController>().enabled = true;

        yield return null;
    }

    private IEnumerator SoftIncreaseCircle()
    {
        Material planeMat = standingPlane.GetComponent<Renderer>().material;
        float currentSize = planeMat.GetFloat("_Radius");

        while (currentSize < 0.9f)
        {
            currentSize += increaseIntervalSize;
            
            planeMat.SetFloat("_Radius", currentSize);

            yield return new WaitForSeconds(increaseIntervalTime);
        }
    }

    private IEnumerator PositionTutorial()
    {
        yield return new WaitForSeconds(delay);

        Vector3 anchorPos = menuAnchor.transform.position;
        targetPos = new Vector3(anchorPos.x, anchorPos.y + yOffset, anchorPos.z);
        transform.position = new Vector3(targetPos.x, startPos.y, targetPos.z);
    }
}
