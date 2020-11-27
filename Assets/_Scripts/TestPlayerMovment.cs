using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayerMovment : MonoBehaviour
{
    // Player Controls
    public float xSpeedCap;
    public float xAcceleration;
    public float jumpCooldown;
    public float jumpStrength;
    public Sprite deathSprite;
    Rigidbody2D body;
    SpriteRenderer spriteRen;
    float jumpTimer = 0;
    bool hitting;
    bool dead = false;
    float deathTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        spriteRen = this.GetComponent<SpriteRenderer>();
        jumpTimer = jumpCooldown;
        deathTimer = 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Checks if player is colliding, not falling, and jump is not on cooldown; before jumping
        if (Input.GetKey("space") && body.velocity.y <= 0 && jumpTimer >= jumpCooldown && !dead)
        {
            body.velocity = new Vector2(body.velocity.x, jumpStrength);
            jumpTimer = 0;
        }

        hitting = true;
        print(collision.gameObject.tag);
        if (collision.transform.CompareTag("Enemy"))
        {
            dead = true;
        }
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
            // Right Movement
            body.AddForce(new Vector2(xAcceleration * Time.deltaTime, 0));
            spriteRen.flipX = false;
        }
        else if (Input.GetKey("a") && body.velocity.x >= -xSpeedCap)
        {
            // Left Movement
            body.AddForce(new Vector2(-xAcceleration * Time.deltaTime, 0));
            spriteRen.flipX = true;
        }
        else if (hitting)
        {
            // Slows the players velocity when on ground to make stopping faster.
            body.velocity = new Vector2(body.velocity.x / 1.1f, body.velocity.y);
        }
        jumpTimer += Time.deltaTime;
        if (dead)
        {
            body.velocity = new Vector2(body.velocity.x / 20f, body.velocity.y);
            transform.GetComponent<CapsuleCollider2D>().isTrigger = true;
            deathTimer += Time.deltaTime;
            spriteRen.flipY = true;
        }
        if (deathTimer > 1)
        {
            body.velocity = new Vector2(body.velocity.x / 20f, body.velocity.y/100);
        }
        if (deathTimer > 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
