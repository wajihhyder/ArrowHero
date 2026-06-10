using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public SoundManager soundManager;
    public int life = 4;

    public AudioSource surfaceHit;

    public ScoreManager scoreManager;

    public PersistentData persistentData;
    public int damage;
    public Weapon weapon;
    private Vector2 storedVelocity; // Store the arrow's velocity when the game is paused
    private Vector2 direction;
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
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        weapon = GameObject.FindWithTag("Player").GetComponent<Weapon>();
        persistentData = GameObject.Find("PersistentData").GetComponent<PersistentData>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        damage = persistentData.getDamage();   
    }

    public void Shoot(Vector2 direction)
    {
        this.direction = direction;
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collider belongs to an enemy
        EnemyBody enemy = collision.collider.GetComponent<EnemyBody>();
        if (enemy == null)
        {
            // If enemy is null, try to get the EnemyBody component from the parent
            enemy = collision.collider.GetComponentInParent<EnemyBody>();
        }

        if (enemy != null)
        {
            // Apply damage and update score based on the hit object
            Vector2 destination = (Vector2)collision.collider.transform.position + new Vector2(Random.Range(0.1f, 0.3f), 0.3f);

            if (collision.collider.tag == "Head")
            {
                DynamicTextManager.CreateText2D(destination, "HEADSHOT!", DynamicTextManager.defaultData);
                scoreManager.AddScore(5);
                enemy.TakeDamage(100);
                weapon.addHeadshot();
                Debug.Log("Enemy hit: Headshot");
                soundManager.PlayEnemyHurtSfx();
            }
            else if (collision.collider.tag == "Enemy")
            {
                destination += new Vector2(0.8f, 0.8f);
                DynamicTextManager.CreateText2D(destination, damage.ToString(), DynamicTextManager.defaultData);
                scoreManager.AddScore(1);
                enemy.TakeDamage(damage);
                soundManager.PlayEnemyHurtSfx();
            }

            // Destroy the arrow gameObject
            Destroy(gameObject);
        }

        if (collision.collider.tag == "Wall" || collision.collider.tag == "Ground")
        {
            surfaceHit.Play();
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
