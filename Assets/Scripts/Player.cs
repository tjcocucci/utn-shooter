using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public Transform gunMuzzleTransform;
    public GameObject bulletPrefab;
    public float timeBetweenShots = 0.5f;
    private float timeForNextShot;
    public Transform bulletContarinerTransform;

    // Start is called before the first frame update
    void Start()
    {
        timeForNextShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Aim();
    }

    void Move () {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += new Vector3(x, 0, z) * speed * Time.deltaTime;
    }
    void Aim () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, gunMuzzleTransform.position);
        float distance;
        if (plane.Raycast(ray, out distance)) {
            Vector3 target = ray.GetPoint(distance);
            Debug.DrawLine(ray.origin, target, Color.red);
            transform.LookAt(target);
            Shoot();
        }
    }

    void Shoot () {
        if (Input.GetMouseButton(0) && Time.time > timeForNextShot) {
            GameObject bullet = Instantiate(bulletPrefab, gunMuzzleTransform.position, gunMuzzleTransform.rotation, bulletContarinerTransform);
            timeForNextShot = Time.time + timeBetweenShots;
        }
    }

}
