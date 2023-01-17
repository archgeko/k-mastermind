using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    KControls controls;

    void Awake()
    {
        controls = new KControls();
        controls.InGame.Enable();
    }

    void OnEnable()
    {
        controls.InGame.Move.started += OnMoveStarted;
        controls.InGame.Move.performed += OnMovePerfomed;
    }

    void OnDisable()
    {
        controls.InGame.Move.started -= OnMoveStarted;
        controls.InGame.Move.performed -= OnMovePerfomed;
    }

    private void OnMoveStarted(InputAction.CallbackContext obj)
    {
        Debug.Log("started");
    }
    private void OnMovePerfomed(InputAction.CallbackContext obj)
    {
        Debug.Log("performed");
    }


}
