using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBeam : MonoBehaviour
{
    public Rigidbody2D rb;
    void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        this.rb.AddForce(transform.up*12.0f, ForceMode2D.Force);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }
}
