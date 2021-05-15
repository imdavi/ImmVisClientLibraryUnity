using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuVisibilityBehaviour : MonoBehaviour
{
    [SerializeField]
    private InputActionReference inputActionReference;

    [SerializeField]
    private GameObject reference;

    [SerializeField]
    [Range(5f, 10f)]
    private float distanceFromReference = 5.0f;

    void Awake()
    {
        inputActionReference.action.started += ToggleMenuVisibility;
    }

    void Destroy() 
    {
        inputActionReference.action.started -= ToggleMenuVisibility;
    }

    void Start()
    {
        ShowMenu();
    }

    private void ShowMenu()
    {
        var newPosition = reference.transform.position + (reference.transform.forward * distanceFromReference);

        gameObject.transform.position = new Vector3(newPosition.x, gameObject.transform.position.y, newPosition.z);
        // gameObject.transform.LookAt(gameObject.transform.position + reference.transform.rotation * Vector3.forward, reference.transform.rotation * Vector3.up);
        gameObject.transform.rotation =  Quaternion.LookRotation(gameObject.transform.position - reference.transform.position);
        gameObject.SetActive(true);
    }

    private void HideMenu()
    {
        gameObject.SetActive(false);
    }


    private void ToggleMenuVisibility(InputAction.CallbackContext context)
    {
        if (gameObject.activeSelf)
        {
            HideMenu();
        }
        else
        {
            ShowMenu();
        }
    }

}
