using System;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicMovement : MonoBehaviour
{
    public bool isRotating;
    public bool isMoving;
    public bool canRotate;
    public bool canMove;
    public bool shouldBrake;

    private Rigidbody2D rb;

    #region Movement
    public Vector3? movementTargetPosition;
    public float movementSpeed;
    public float movementBurstSpeed;
    public float movementTurboSpeed;
    public float stopMovementDistance;
    [Range(0.0f, 1.0f)] public float brakeMovementTreshold;
    private Vector2 currentDirection;
    private float totalDistanceFromTarget;
    private float currentDistanceFromTarget;

    #endregion

    public Transform target;
    public float rotationSpeed;
    private float rotationTreshold;
    private Quaternion rotationToAchieve;
    private Quaternion nextRotation;
    public Color gizmoColor;


    void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        SetupMovementVariables();
    }

    private void SetupMovementVariables()
    {
        // throw new NotImplementedException();
    }

    public void MoveTo(Vector3 destination)
    {
        rb.velocity = Vector2.zero;
        this.currentDirection = (destination - transform.position).normalized;
        rb.AddForce(this.currentDirection * this.movementSpeed, ForceMode2D.Impulse);
        rb.AddForce(this.currentDirection * this.movementBurstSpeed, ForceMode2D.Force);
        this.isMoving = true;
        this.shouldBrake = true;
    }

    public void AimTarget()
    {
        SetRotationToAchieve(this.target.position);
        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                (Quaternion)this.rotationToAchieve,
                .7f * this.rotationSpeed * Time.deltaTime
            );
    }

    public void SetRotationTargetPosition(Transform target)
    {
        Debug.Log($"My target is at: {target}");
        this.target = target;
    }
    public void SetMovementTargetPosition(Vector3? target)
    {
        this.movementTargetPosition = target;
        this.isMoving = false;
        this.shouldBrake = false;
        if (target.HasValue)
        {
            this.totalDistanceFromTarget = Vector3.Distance(transform.position, target.Value);
        }
    }

    public void SetRotationToAchieve(Vector3 target)
    {
        var dir = (target - transform.position).normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        this.rotationToAchieve = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (this.movementTargetPosition.HasValue)
            {
                if (this.isMoving)
                {
                    SetDistanceAndCheckForBrake(
                        this.movementTargetPosition.Value,
                        this.stopMovementDistance,
                        this.brakeMovementTreshold,
                        this.movementBurstSpeed
                    );
                }
                else
                {
                    MoveTo(this.movementTargetPosition.Value);
                }
            }
            else
            {
                // SetNewRandomPosition();
            }
        }
        if (canRotate)
        {
            if (target != null)
            {
                AimTarget();
            }
        }
    }

    private void SetNewRandomPosition()
    {
        float randomNumber = UnityEngine.Random.Range(0.0f, .05f);
        this.SetMovementTargetPosition(
            this.transform.position 
            +
             new Vector3(randomNumber, randomNumber, 0));
    }

    private void SetDistanceAndCheckForBrake(Vector3 destination, float stopMovementDistance, float brakeMovementTreshold, float brakeSpeed)
    {
        this.currentDistanceFromTarget = Vector3.Distance(transform.position, destination);
        if (this.currentDistanceFromTarget < stopMovementDistance)
        {
            Debug.Log("shtoppp");
            rb.velocity = rb.velocity * 0.08f;
            SetMovementTargetPosition(null);
            this.isMoving = false;
        }
        else if (
            (this.currentDistanceFromTarget < brakeMovementTreshold * this.totalDistanceFromTarget)
            &
            (this.shouldBrake)
        )
        {
            rb.AddForce(this.rb.velocity.normalized * -1 * brakeSpeed, ForceMode2D.Force);
            this.shouldBrake = false;
        }
    }

    void OnDrawGizmos()
    {
        if (this.movementTargetPosition.HasValue)
        {
            Gizmos.color = this.gizmoColor;
            Vector3 position = this.movementTargetPosition.Value;
            Gizmos.DrawWireSphere(position, 0.5f);
        }
    }
}
