using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    public float health;
    public float totalHealth = 100;

    public void TakeDamage (float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        health = totalHealth;
    }
}
