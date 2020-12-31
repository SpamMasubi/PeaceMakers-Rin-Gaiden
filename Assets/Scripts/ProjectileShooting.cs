using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    public float speed = 3;
    public Player player;
    public int damage;
    private Rigidbody2D rb2d;
    private bool isFiring = true;
    void Start()
    {
        player = FindObjectOfType<Player>();
        rb2d = GetComponent<Rigidbody2D>();

        if(player.transform.position.x < transform.position.x)
        {
            speed = -speed;
            transform.localScale = new Vector2(1, 1);
        }else if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
    void Update()
    {
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        isFiring = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player _player = collision.GetComponent<Collider2D>().GetComponent<Player>();
        if (_player != null)
        {
            _player.DamagePlayer(damage);
        }
        isFiring = false;
        Destroy(gameObject);
    }
}
