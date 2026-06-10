using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystickhandle : MonoBehaviour
{
    // Start is called before the first frame update
    public Joystick Joystick;
    private float speed = 5;
    private Rigidbody2D rb;
    bool facingRight = true;
    float direction = 0f;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        Joystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        
        direction = Joystick.Direction.x;
    }
    void FixedUpdate()
    {
        if(direction > 0.1f || direction < -0.1f)
        {
            rb.velocity = new Vector2(direction * speed, 0f);
            // if (direction > 0.1f && facingRight)
            // {
                
               
            //     Flip();
            // }
            // else if(direction < -0.1f && !facingRight)
            // {
               
              
            //     Flip();
            // }
        }
    }

    void Flip()
    {
        // Switch the way the player is facing
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
