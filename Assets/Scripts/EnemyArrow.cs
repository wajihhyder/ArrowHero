using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;

    public int life = 4;

    private Vector2 direction;
    private Vector2 storedVelocity; // Store the arrow's velocity

    void Start()
    {
        // Ensure the Rigidbody2D is assigned
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Set the initial velocity of the arrow
        rb.velocity = transform.right * speed;
        direction = rb.velocity.normalized;
    }

    public void Shoot(Vector2 direction)
    {
        this.direction = direction;
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collider belongs to a player
        if (collision.collider.tag == "Player")
        {
            Player player = collision.collider.GetComponent<Player>();
            // Apply damage and update score based on the hit object
            Vector2 destination = (Vector2)collision.collider.transform.position + new Vector2(Random.Range(0.1f, 0.3f), 0.3f);
            destination += new Vector2(0.8f, 0.8f);
            DynamicTextManager.CreateText2D(destination, "25", DynamicTextManager.defaultData);
            player.TakeDamage(25);

            // Destroy the arrow gameObject
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "Wall")
        {
            life--;
            if (life <= 0)
            {
                Destroy(gameObject);
            }
            // Reflect the direction based on the collision normal
            Vector2 normal = collision.contacts[0].normal;
            direction = Vector2.Reflect(direction, normal).normalized;

            // Update the arrow's velocity
            rb.velocity = direction * speed;

            // Update the arrow's rotation to match the new direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (collision.collider.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        // Restore the velocity when the game resumes
        if (rb != null)
        {
            rb.velocity = storedVelocity;
        }
    }

    void OnDisable()
    {
        // Store the velocity when the game is paused
        if (rb != null)
        {
            storedVelocity = rb.velocity;
        }
    }
}
