using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DamageableObject
{
    public Transform playerTransform;
    private float distanceToPlayer;
    public float distanceToPlayerThreshold = 5.0f;
    public float speed = 2;
    public float damage = 10;

    public Transform gunMuzzleTransform;
    public Bullet bulletPrefab;
    public float timeBetweenShots = 2;
    private float timeForNextShot;
    public Transform bulletContarinerTransform;

    // Start is called before the first frame update
    public override void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        bulletContarinerTransform = GameObject.Find("BulletContainer").transform;
        timeForNextShot = Time.time;
        base.Start();
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > distanceToPlayerThreshold)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        transform.LookAt(playerTransform);

        Shoot();
    }

    public void Shoot()
    {
        if (Time.time > timeForNextShot)
        {
            transform.LookAt(playerTransform);
            Bullet bullet = Instantiate(
                bulletPrefab,
                gunMuzzleTransform.position,
                gunMuzzleTransform.rotation,
                bulletContarinerTransform
            );
            bullet.damage = damage;
            timeForNextShot = Time.time + timeBetweenShots;
        }
    }
}
