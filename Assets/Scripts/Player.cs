using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WeaponController))]
public class Player : DamageableObject
{
    public float speed = 5;
    public int weaponIndex = 0;
    private bool won = false;
    public bool isAlive = true;
    public bool devMode = false;
    public WeaponController weaponController;

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        weaponController = GetComponent<WeaponController>();
        weaponController.EquipWeapon(1);
        OnObjectDied += OnPlayerDeath;
        LevelManager.Instance.OnWin += OnWin;
        if (devMode)
        {
            health = 100000;
        }
    }

    void OnPlayerDeath()
    {
        isAlive = false;
        Debug.Log("You died!");
    }

    // Update is called once per frame
    void Update()
    {
        if (!won)
        {
            Move();
            ChangeWeapon();
            Aim();
        }
        if (devMode)
        {
            devModeControls();
        }
    }

    void devModeControls()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(float.PositiveInfinity);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelManager.Instance.LoadLevel(LevelManager.Instance.currentLevelIndex + 1);
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += new Vector3(x, 0, z) * speed * Time.deltaTime;
        MoveCamera(x, z);
    }

    void MoveCamera(float x, float z)
    {
        if (x != 0 || z != 0)
        {
            Camera.main.transform.localPosition +=
                new Vector3(x, 0, z) * (speed / 4) * Time.deltaTime;
        }
    }

    public void OnWin()
    {
        won = true;
    }

    void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Plane plane = new Plane(Vector3.up, weaponController.weapon.gunMuzzleTransform.position);
        Plane plane = new Plane(Vector3.up, -0.5f);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Debug.DrawLine(ray.origin, target, Color.red);
            transform.LookAt(target);
            Shoot();
        }
    }

    void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && weaponIndex != 0)
        {
            weaponController.EquipWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weaponIndex != 1)
        {
            weaponController.EquipWeapon(1);
        }
    }

    public void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            weaponController.weapon.Shoot();
        }
    }

    public void resetPlayer()
    {
        isAlive = true;
        won = false;
        health = totalHealth;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        weaponController.EquipWeapon(1);
    }
}
