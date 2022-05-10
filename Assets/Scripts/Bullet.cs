using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] [Range(0f, 50f)] float Bullet_Speed;
    [SerializeField] [Range(0f, 5f)] float Bullet_Lifetime;

    [SerializeField] Rigidbody2D RB;
    [SerializeField] BoxCollider2D BC;
    [SerializeField] SpriteRenderer SR;
    [SerializeField] ParticleSystem PS;

    private void Awake()
    {
        Destroy(this.gameObject, Bullet_Lifetime);
    }

    private void FixedUpdate()
    {
        RB.velocity = transform.right * Bullet_Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet_Speed = 0;
        SR.enabled = false;
        BC.enabled = false;
        PS.Play();
        Destroy(this.gameObject, 1);
    }
}
