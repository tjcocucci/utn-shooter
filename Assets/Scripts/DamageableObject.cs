using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    public float health;
    public float totalHealth = 100;
    public event System.Action OnObjectDied;

    public void TakeDamage (float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
            if (OnObjectDied != null) {
                OnObjectDied();
            }
        }
    }
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        health = totalHealth;
    }
}
