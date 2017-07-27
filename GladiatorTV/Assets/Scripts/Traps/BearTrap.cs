﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour {
    public int trapDamage;

    public float activeTime;
    public bool active;
    public float counter;

    public bool disabled;


    public bool released;
    public float cooldownTime;

    public int targetType;
    public GameObject target;

    public Animator anim;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
        anim = this.gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Check_Time();
	}

    private void Check_Time()
    {
        if(active)
        {
            if(counter >= activeTime)
            {
                release();
            }
        }
        if(released)
        {
            if(counter >= cooldownTime)
            {
                released = false;
                counter = 0;
            }
        }
    }
    private void Increment_Counters()
    {
        if (active || released)
        {
            counter += .25f;
        }
    }

    private void release()
    {
        active = false;
        anim.SetBool("Activated", false);
        if (targetType == 0)
        {
            target.GetComponent<PlayerController>().Set_Stunned(false);
        }
        else if(targetType == 1)
        {
            target.GetComponent<BaseEnemy>().Set_Stunned(false);
        }
        released = true;
        counter = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !active && !released)
        {
            target = collision.gameObject;
            PlayerController player_con = collision.GetComponent<PlayerController>();
            player_con.Deal_Damage(trapDamage);
            active = true;
            anim.SetBool("Activated", true);
            targetType = 0;
            player_con.Set_Stunned(true);            
        }


        if (collision.gameObject.CompareTag("Enemy") && !active && !released)
        {
            target = collision.gameObject;
            BaseEnemy enemy_con = collision.GetComponent<BaseEnemy>();
            enemy_con.Deal_Damage(trapDamage);
            active = true;
            anim.SetBool("Activated", true);
            targetType = 1;
            enemy_con.Set_Stunned(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !active && !released)
        {
            target = collision.gameObject;
            PlayerController player_con = collision.GetComponent<PlayerController>();
            player_con.Deal_Damage(trapDamage);
            active = true;
            anim.SetBool("Activated", true);
            targetType = 0;
            player_con.Set_Stunned(true);
        }
        if (collision.gameObject.CompareTag("Enemy") && !active && !released)
        {
            target = collision.gameObject;
            BaseEnemy enemy_con = collision.GetComponent<BaseEnemy>();
            enemy_con.Deal_Damage(trapDamage);
            active = true;
            anim.SetBool("Activated", true);
            targetType = 1;
            enemy_con.Set_Stunned(true);
        }
    }
}
