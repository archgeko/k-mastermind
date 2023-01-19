using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public BasicMovement basicMovement;
    public PlayerController playerController;

    void Start()
    {
        SetTarget();
    }

    [Button]
    public void SetTarget(){
        basicMovement.SetRotationTargetPosition(playerController.transform);
    }
}
