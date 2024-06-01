using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDaggerProjectile : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 5;
    private bool hitDetected = false;

    // Method to set the direction of the projectile
    public void SetDirection(float dir_x, float dir_y)
    {
        direction = new Vector3(dir_x, dir_y, 0f).normalized;  // Normalize to ensure consistent speed

        // Flip the projectile if moving left
        if (dir_x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void Update()
    {
        // Move the projectile in the set direction
        transform.position += direction * speed * Time.deltaTime;

        // Only check for collisions every 6 frames to improve performance
        if (Time.frameCount % 6 == 0)
        {
            CheckForCollisions();
        }
    }

    private void CheckForCollisions()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.7f);
        foreach (Collider2D col in hitColliders)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                hitDetected = true;
                break;
            }
        }

        // Destroy the projectile if a hit is detected
        if (hitDetected)
        {
            Destroy(gameObject);
        }
    }

    // Optional: Add a gizmo to visualize the collision detection radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.7f);
    }
}
