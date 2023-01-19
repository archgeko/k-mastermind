using System;
using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public BasicMovement basicMovement;
    public BasicShoot basicShoot;
    public InputUtil inputUtil;

    public Transform target;
    KControls controls;

    public Transform test;


    void Awake()
    {
        this.basicShoot = GetComponentInChildren<BasicShoot>();
        controls = new KControls();
        controls.InGame.Enable();
    }

    void OnEnable()
    {
        controls.InGame.Move.started += OnMove;
        controls.InGame.Move.performed += OnMove;
        controls.InGame.LockTarget.started += OnLockTarget;
    }



    void OnDisable()
    {
        controls.InGame.Move.started -= OnMove;
        controls.InGame.Move.performed -= OnMove;
        controls.InGame.LockTarget.started -= OnLockTarget;

    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            basicMovement.SetMovementTargetPosition(inputUtil.CursorInWorldSpace);
        }
    }
    private void OnLockTarget(InputAction.CallbackContext obj)
    {
        //EnemyClickableLogic

        basicMovement.SetRotationTargetPosition(this.target);
        this.basicShoot.Shoot();

    }

    public void SetTarget()
    {
        this.target = this.target;
    }


}
