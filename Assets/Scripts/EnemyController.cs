using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public BasicMovement basicMovement;
    public PlayerController playerController;
    public GameObject crosshair;
    public BasicEnemyBeam beam;
    public Transform shootingPoint;
    public Transform SpawnedBullets;

    void Start()
    {
        SetTarget();
        SetAsSelected(false);
        StartCoroutine(Shoot());
    }

    [Button]
    public void SetTarget()
    {
        basicMovement.SetRotationTargetPosition(playerController.transform);
    }

    public void SetAsSelected(bool isSelected)
    {
        crosshair.SetActive(isSelected);
    }

    public IEnumerator Shoot()
    {
        while (true)
        {
            Instantiate(beam, this.shootingPoint.position, this.shootingPoint.rotation, this.SpawnedBullets);
            yield return new WaitForSeconds(3.0f);
        }
    }
}
