﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowerController : BaseEnemy {

    public GameObject Knife;

    public float cooldownTime;
    private float cooldownCounter;
    private bool coolDown;
    //private SpriteRenderer Sprite;

    public float minTeleportTime;
    public float maxTeleportTime;
    public float currentTeleportTime;
    public bool teleporting = false;
    public float teleportCounter;

    public Vector3 teleportLocation;
    public bool routineCalled = false;

    public float knifeSpeed;
    public float MediumknifeSpeed;
    public float HardknifeSpeed;

    // Use this for initialization
    void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
        Set_Attack_Time(1.5f);
        Set_Attacking(true);
        anim = this.gameObject.GetComponent<Animator>();
        anim.SetBool("Attacking", true);
        //Sprite = this.gameObject.GetComponent<SpriteRenderer>();
        New_Teleport_Time();
    }

    // Update is called once per frame
    void Update () {
        Check_HP();
        if (Check_if_Dying())
        {
            Die();
        }

        if (!CheckDifficulty())
        {
            Set_Values(Get_Difficulty());
            Set_Current_Difficulty(Get_Difficulty());
        }
        Set_Direction();
        Flip();
        checkInvincible();


        if (teleportCounter >= currentTeleportTime && !Get_Attacking())
        {
            if(!Get_Stunned())
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    Reset_Attack_Counter();
                    cooldownCounter = 0f;
                    Set_Attacking(false);
                    if (!routineCalled)
                    {
                        StartCoroutine(Start_Teleport());
                        routineCalled = true;
                    }

                }
            }
        }
        else if(!teleporting)
        {
            if (Get_Attack_Counter() >= Get_Attack_Time())
            {
                SpawnProjectile(Knife);
                Reset_Attack_Counter();
                anim.SetBool("Attacking", false);
                coolDown = true;
                Set_Attacking(false);
            }
            if (cooldownCounter == cooldownTime)
            {
                cooldownCounter = 0f;
                coolDown = false;
                anim.SetBool("Attacking", true);
                Set_Attacking(true);
            }
        }
    }

    private void SpawnProjectile(GameObject Projectile)
    {
        GameObject Object = Instantiate(Projectile, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Object.GetComponent<Knife>().Set_Damage(AttackDamage);
        Object.GetComponent<Knife>().Set_Speed(knifeSpeed);

    }


    private void Increment_Counters()
    {
        if (Get_Attacking() && !teleporting)
        {
            Increase_Attack_Counter(.25f);
        }
        if(Get_Dying())
        {
            Increase_Death_Counter(.25f);
        }
        if (coolDown && !teleporting)
        {
            cooldownCounter += .25f;
        }
        if(!teleporting)
        {
            teleportCounter += .25f;
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

    //private void Flip()
    //{
    //    if(direction == 3)
    //    {
    //        if(Sprite.flipX)
    //        {
              
    //        }
    //        else
    //        {
    //            Sprite.flipX = true;
    //        }
    //    }
    //    if (direction == 1)
    //    {
    //        if (Sprite.flipX)
    //        {
    //            Sprite.flipX = false;
    //        }
    //        else
    //        {
                
    //        }
    //    }
    //}

    private void New_Teleport_Time()
    {
        currentTeleportTime = (int)Random.Range(minTeleportTime, maxTeleportTime);
    }

    private IEnumerator Start_Teleport()
    {
        Set_Attacking(false);
        teleporting = true;
        anim.SetBool("Teleporting", true);
        Reset_Attack_Counter();
        teleportCounter = 0f;
        yield return new WaitForSeconds(.75f);
        Teleport_To_Location(this.gameObject.transform.position);
        anim.SetBool("Teleporting", false);
        yield return new WaitForSeconds(1f);
        teleporting = false;
        routineCalled = false;
    }

    private void Teleport_To_Location(Vector3 point)
    {
        this.gameObject.transform.position = point + new Vector3(1,0,0);
    }

    private void Set_Values(int val)
    {
        switch(val)
        {
            case 1:
                health = MediumHealth;
                AttackDamage = MediumAttackDamage;
                TouchDamage = MediumTouchDamage;
                knifeSpeed = MediumknifeSpeed;
                break;
            case 2:
                health = HardHealth;
                AttackDamage = HardAttackDamage;
                TouchDamage = HardTouchDamage;
                knifeSpeed = HardknifeSpeed;
                break;
        }
    }
}
