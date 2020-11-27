using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float jumpStrength;
    public float jumpCooldown;
    Rigidbody2D body;
    float timer = 0;
    bool hitting;
    float deathTimer = 0;
    void Start()
    {
        body = transform.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitting = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > jumpCooldown && hitting)
        {
            body.velocity = new Vector2(-jumpStrength, jumpStrength);
            timer = 0;
        }
        timer += Time.deltaTime;
        if (deathTimer > 20f)
        {
            Destroy(gameObject);
        }
        deathTimer += Time.deltaTime;
    }
}
