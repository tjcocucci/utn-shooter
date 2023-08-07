using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WeaponController))]
public class Enemy : DamageableObject
{
    public Transform playerTransform;
    private float distanceToPlayer;
    public float distanceToPlayerThreshold = 5.0f;
    public float speed = 2;
    public float damage = 10;
    public WeaponController weaponController;
    public int weaponIndex = 0;

    private float timeForNextShot;
    public float timeBetweenShots = 0.5f;

    // Start is called before the first frame update
    public override void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        weaponController = GetComponent<WeaponController>();
        weaponController.EquipWeapon(weaponIndex);
        timeForNextShot = Time.time + weaponController.weapon.timeBetweenShots;
        base.Start();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer > distanceToPlayerThreshold)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            transform.LookAt(playerTransform);

            Shoot();
        }
    }

    public void Shoot()
    {
        if (Time.time > timeForNextShot)
        {
            transform.LookAt(playerTransform);
            weaponController.weapon.Shoot();
            timeForNextShot =
                Time.time + timeBetweenShots + (timeBetweenShots * 0.1f) * Random.Range(-1, 1);
        }
    }
}
