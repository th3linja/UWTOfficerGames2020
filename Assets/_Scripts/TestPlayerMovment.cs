using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovment : MonoBehaviour
{
    public float xSpeedCap;
    public float xAcceleration;
    public float jumpCooldown;
    Rigidbody2D body;
    bool hitting = false;
    bool jumpLocked = false;
    float jumpTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        jumpTimer = jumpCooldown;
    }

    void OnTriggerEnter2D()
    {
        hitting = true;
    }

    void OnTriggerExit2D()
    {
        hitting = false;
        jumpLocked = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        jumpLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        print(jumpLocked);
        if (Input.GetKey("d") && body.velocity.x <= xSpeedCap)
        {
            body.AddForce(new Vector2(xAcceleration * Time.deltaTime, 0));
        }
        if (Input.GetKey("a") && body.velocity.x >= -xSpeedCap)
        {
            body.AddForce(new Vector2(-xAcceleration * Time.deltaTime, 0));
        }
        if (Input.GetKey("space") && body.velocity.y <= 0 && hitting && jumpTimer >= jumpCooldown)
        {
            body.velocity = new Vector2(body.velocity.x, 5);
            jumpTimer = 0;
            jumpLocked = true;
        }
        jumpTimer += Time.deltaTime;
    }
}
