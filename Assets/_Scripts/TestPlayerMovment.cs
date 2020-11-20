using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovment : MonoBehaviour
{
    public float xSpeedCap;
    public float xAcceleration;
    public float jumpCooldown;
    public float jumpStrength;
    Rigidbody2D body;
    SpriteRenderer spriteRen;
    float jumpTimer = 0;
    bool hitting;
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        spriteRen = this.GetComponent<SpriteRenderer>();
        jumpTimer = jumpCooldown;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKey("space") && body.velocity.y <= 0 && jumpTimer >= jumpCooldown)
        {
            body.velocity = new Vector2(body.velocity.x, jumpStrength);
            jumpTimer = 0;
        }

        hitting = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hitting = false;
    }

    void Update()
    {
        //print(hitting);
        if (Input.GetKey("d") && body.velocity.x <= xSpeedCap)
        {
            body.AddForce(new Vector2(xAcceleration * Time.deltaTime, 0));
            spriteRen.flipX = false;
        }
        else if (Input.GetKey("a") && body.velocity.x >= -xSpeedCap)
        {
            body.AddForce(new Vector2(-xAcceleration * Time.deltaTime, 0));
            spriteRen.flipX = true;
        }
        else if(hitting)
        {
            body.velocity = new Vector2(body.velocity.x / 1.1f, body.velocity.y);
        }
        jumpTimer += Time.deltaTime;
    }
}
