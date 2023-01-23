using System;
using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public BasicMovement basicMovement;
    public BasicShoot basicShoot;
    public EnemyManager enemyManager;
    public InputUtil inputUtil;

    public Transform target;
    KControls controls;

    public Transform test;


    void Awake()
    {
        this.basicShoot = GetComponentInChildren<BasicShoot>();
        this.enemyManager = FindObjectOfType<EnemyManager>();
        controls = new KControls();
        controls.InGame.Enable();
    }

    void OnEnable()
    {
        controls.InGame.Move.started += OnMove;
        controls.InGame.Move.performed += OnMove;
        controls.InGame.NextTarget.started += OnNextTarget;
        controls.InGame.BasicShoot.started += OnBasicShoot;
        controls.InGame.SetLock.started += SetLock;
    }



    void OnDisable()
    {
        controls.InGame.Move.started -= OnMove;
        controls.InGame.Move.performed -= OnMove;
        controls.InGame.NextTarget.started -= OnNextTarget;
        controls.InGame.BasicShoot.started -= OnBasicShoot;
        controls.InGame.SetLock.started -= SetLock;
    }
    private void SetLock(InputAction.CallbackContext obj)
    {
        this.basicMovement.SetActiveLock(false);
    }
    private void OnBasicShoot(InputAction.CallbackContext obj)
    {
        this.basicShoot.Shoot();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            basicMovement.SetMovementTargetPosition(inputUtil.CursorInWorldSpace);
        }
    }
    private void OnNextTarget(InputAction.CallbackContext obj)
    {
        // basicMovement.SetRotationTargetPosition(this.target);
        basicMovement.SetActiveLock(true);
        basicMovement.SetRotationTargetPosition(enemyManager.GetNextEnemy().transform);
        // this.basicShoot.Shoot();
    }

    public void SetTarget()
    {
        this.target = this.target;
    }


}
