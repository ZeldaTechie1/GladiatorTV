using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerController : BaseEnemy {

    public float chargeSpeed;
    public float MediumchargeSpeed;
    public float HardchargeSpeed;

    private bool charging = false;
    private float chargeTime = 1f;
    public float chargeCounter = 0f;

    private bool waiting = false;
    public float waitTime = 2f;
    public float MediumWaitTime;
    public float HardWaitTime;
    public float waitCounter = 0f;

    public Vector3 targetLocation;
    private Vector3 moveVector;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
        Set_Attack_Time(1.5f);
        Set_Attacking(true);
        anim = this.gameObject.GetComponent<Animator>();
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

        if (Get_Stunned())
        {
            anim.SetBool("Stunned", true);
            Attack_Complete();
        }

        if(!charging && !waiting && !Get_Stunned())
        {
            Set_Direction();
            anim.SetInteger("Direction", direction);
            Flip();
        }
        FlipY();
        checkInvincible();



        if (Get_Attacking() && !Get_Stunned())
        {
            Prepare_Attack2();
            if(chargeTime <= chargeCounter)
            {
                anim.SetBool("Attacking", true);
                anim.SetBool("Charging", false);
                Attack_Player();
            }
            else if(!waiting)
            {
                anim.SetBool("Charging", true);
            }
        }

        if(waitTime <= waitCounter)
        {
            if(!Get_Stunned())
            {
                anim.SetBool("Stunned", false);
                waitCounter = 0;
                waiting = false;
            }
        }
	}

    private void Increment_Counters()
    {
        if(charging)
        {
            chargeCounter += .25f;
        }

        if(waiting)
        {
            waitCounter += .25f;
        }

        if (Get_Dying())
        {
            Increase_Death_Counter(.25f);
        }
    }

    private void Prepare_Attack()
    {
        if(!waiting && !Get_Dying() && !charging)
        {
            targetLocation = player.transform.position;
            moveVector = (targetLocation - this.gameObject.transform.position).normalized * chargeSpeed;

            charging = true;
        }
    }

    private void Prepare_Attack2()
    {
        if (!waiting && !Get_Dying() && !charging)
        {
            switch(direction)
            {
                case 0:
                    targetLocation = player.transform.position;
                    moveVector = (targetLocation - this.gameObject.transform.position).normalized * chargeSpeed;
                    moveVector.x = 0;
                    break;

                case 1:
                    targetLocation = player.transform.position;
                    moveVector = (targetLocation - this.gameObject.transform.position).normalized * chargeSpeed;
                    moveVector.y = 0;
                    break;

                case 2:
                    targetLocation = player.transform.position;
                    moveVector = (targetLocation - this.gameObject.transform.position).normalized * chargeSpeed;
                    moveVector.x = 0;
                    break;

                case 3:
                    targetLocation = player.transform.position;
                    moveVector = (targetLocation - this.gameObject.transform.position).normalized * chargeSpeed;
                    moveVector.y = 0;
                    break;
            }
            charging = true;
        }
    }

    private void Attack_Player()
    {
        Set_Speed(chargeSpeed);
        //Move_To_Location(targetLocation);
        Move_With_Vector();
    }

    private void Move_With_Vector()
    {
        this.gameObject.transform.position += moveVector * Time.deltaTime;
    }

    private void Attack_Complete()
    {
        Set_Speed(baseSpeed);
        charging = false;
        waiting = true;
        chargeCounter = 0;
    }

    private void Move_Back()
    {
        this.gameObject.transform.position -= moveVector * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (charging)
            {
                player_con.Deal_Damage(AttackDamage);
            }
            else
            {
                player_con.Deal_Damage(TouchDamage);
            }
            //Attack_Complete();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Attack_Complete();
            anim.SetBool("Stunned", true);
            anim.SetBool("Attacking", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (charging)
            {
                player_con.Deal_Damage(AttackDamage);
            }
            else
            {
                player_con.Deal_Damage(TouchDamage);
            }
            //Attack_Complete();
        }
    }

    private void Set_Values(int val)
    {
        switch (val)
        {
            case 1:
                health = MediumHealth;
                AttackDamage = MediumAttackDamage;
                TouchDamage = MediumTouchDamage;
                waitTime = MediumWaitTime;
                chargeSpeed = MediumchargeSpeed;
                break;
            case 2:
                health = HardHealth;
                AttackDamage = HardAttackDamage;
                TouchDamage = HardTouchDamage;
                waitTime = HardWaitTime;
                chargeSpeed = HardchargeSpeed;
                break;
        }
    }

    private void FlipY()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("StunnedLancer"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }
        else if (direction == 0)
        {
            if (this.gameObject.GetComponent<SpriteRenderer>().flipY)
            {

            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        else if (direction == 2)
        {
            if (this.gameObject.GetComponent<SpriteRenderer>().flipY)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipY = false;
            }
            else
            {

            }
        }
    }
}
