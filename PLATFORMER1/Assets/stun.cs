using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stun : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;
    private float timer;
    private bool hitEnemy = false;

    // Start is called before the first frame update
    public void Shoot(Vector2 shootDirection)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection.normalized * force;

        // Rotate the projectile to face the shooting direction
        float rot = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

    }

    // Update is called once per frame
    void Update()
    {
        if (hitEnemy)
        {
            timer += Time.deltaTime;

            if (timer >= 5)
            {
                Destroy(gameObject);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // If the projectile hits an enemy, stop their movement for 5 seconds
            EnemyPatroll enemyMovement = other.GetComponent<EnemyPatroll>();
            if (enemyMovement != null)
            {
                enemyMovement.StopMovementForSeconds(5);
            }

            // Set the flag to indicate that the enemy is hit
            hitEnemy = true;

            // Optionally, you can disable the collider and renderer of the projectile
            // to make it visually disappear after hitting the enemy
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            // If the projectile hits anything other than an enemy, destroy it
            Destroy(gameObject);
        }
    }
}
