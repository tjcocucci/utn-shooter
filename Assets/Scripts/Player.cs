using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : DamageableObject, IShooter
{
    public float speed = 5;
    private int WeaponIndex = 1;
    public Weapon[] weaponList;
    private Weapon weapon;

    // Start is called before the first frame update
    override public void Start()
    {
        EquipWeapon(1);
        base.Start();
    }

    private void EquipWeapon (int index) {
        if (weapon != null) {
            Destroy(weapon.gameObject);
        }
        WeaponIndex = index;
        weapon = Instantiate(weaponList[WeaponIndex - 1], transform);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ChangeWeapon();
        Aim();
    }

    void Move () {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += new Vector3(x, 0, z) * speed * Time.deltaTime;
    }
    void Aim () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, weapon.gunMuzzleTransform.position);
        float distance;
        if (plane.Raycast(ray, out distance)) {
            Vector3 target = ray.GetPoint(distance);
            Debug.DrawLine(ray.origin, target, Color.red);
            transform.LookAt(target);
            Shoot();
        }
    }

    void ChangeWeapon () {
        if (Input.GetKeyDown(KeyCode.Alpha1) && WeaponIndex != 1) {
            EquipWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && WeaponIndex != 2) {
            EquipWeapon(2);
        }
    }

    public void Shoot () {
        if (Input.GetMouseButton(0)) {
            weapon.Shoot();
        }
    }

}
