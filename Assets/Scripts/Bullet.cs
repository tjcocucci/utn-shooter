using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public float timeToDestroy = 3.0f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Bullet Shot!");
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }
}
