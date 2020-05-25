using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Rigidbody2D rb;
    System.Random rand = new System.Random();
    float rotateSpeed = 0f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rotateSpeed = rand.Next(-100, 100) / 20f;
    }

    private void FixedUpdate()
    {
        rb.rotation += rotateSpeed;
    }

}
