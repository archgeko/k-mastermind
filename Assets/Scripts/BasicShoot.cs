using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShoot : MonoBehaviour
{
    public Rigidbody2D projectile;

    public void Shoot()
    {
        projectile.velocity = Vector2.zero;
        projectile.transform.position = transform.position;
        projectile.AddForce(transform.up.normalized * 10f, ForceMode2D.Impulse);
    }

    void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawLine(transform.position,transform.position+ transform.up*5);
    }
}
