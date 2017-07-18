using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowerController : BaseEnemy {

    public GameObject Knife;

    public float cooldownTime;
    private float cooldownCounter;
    private bool coolDown;
    private SpriteRenderer Sprite;
    // Use this for initialization
    void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
        Set_Attack_Time(1.5f);
        Set_Attacking(true);
        anim = this.gameObject.GetComponent<Animator>();
        anim.SetBool("Attacking", true);
        Sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        Set_Direction();
        Flip();
        checkInvincible();
        Check_If_Dead();


        if (Get_Attack_Counter() >= Get_Attack_Time())
        {
            SpawnProjectile(Knife);
            Reset_Attack_Counter();
            anim.SetBool("Attacking", false);
            coolDown = true;
            Set_Attacking(false);
        }
        if(cooldownCounter == cooldownTime)
        {
            cooldownCounter = 0f;
            coolDown = false;
            anim.SetBool("Attacking", true);
            Set_Attacking(true);
        }
    }

    private void SpawnProjectile(GameObject Projectile)
    {
        GameObject Object = Instantiate(Projectile, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }


    private void Increment_Counters()
    {
        if (Get_Attacking())
        {
            Increase_Attack_Counter(.25f);
        }
        if(Get_Dying())
        {
            Increase_Death_Counter(.25f);
        }
        if (coolDown)
        {
            cooldownCounter += .25f;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           player_con.Deal_Damage(TouchDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_con.Deal_Damage(TouchDamage);
        }
    }

    private void Flip()
    {
        if(direction == 3)
        {
            if(Sprite.flipX)
            {
              
            }
            else
            {
                Sprite.flipX = true;
            }
        }
        if (direction == 1)
        {
            if (Sprite.flipX)
            {
                Sprite.flipX = false;
            }
            else
            {
                
            }
        }
    }
}
