using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : DamageableObject
{
    public float speed = 5;
    public int weaponIndex = 1;
    public Weapon[] weaponList;
    private Weapon weapon;
    public bool isAlive = true;

    // Start is called before the first frame update
    override public void Start()
    {
        EquipWeapon(1);
        base.Start();
        OnObjectDied += OnPlayerDeath;
    }

    void OnPlayerDeath()
    {
        isAlive = false;
        Debug.Log("You died!");
    }

    private void EquipWeapon(int index)
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }
        weaponIndex = index;
        weapon = Instantiate(weaponList[weaponIndex - 1], transform);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ChangeWeapon();
        Aim();
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

    void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, weapon.gunMuzzleTransform.position);
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && weaponIndex != 1)
        {
            EquipWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weaponIndex != 2)
        {
            EquipWeapon(2);
        }
    }

    public void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            weapon.Shoot();
        }
    }
}
