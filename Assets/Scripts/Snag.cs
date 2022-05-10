using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snag : MonoBehaviour
{
    [SerializeField] [Range(1, 20)] int Health_Size;

    [SerializeField] Animator animmatior;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            animmatior.SetBool("Snag Take Damage", true);
            Health_Size--;
            Invoke("DisableAnimation", 0.04f);

            if (Health_Size <= 0)
            {
                Destroy(this.gameObject, 0.04f);
            }
        }
    }

    private void DisableAnimation()
    {
        animmatior.SetBool("Snag Take Damage", false);
    }
}
