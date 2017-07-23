using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerController : BaseEnemy {

    public float chargeSpeed;
<<<<<<< HEAD
=======
    public float MediumchargeSpeed;
    public float HardchargeSpeed;
>>>>>>> Spector-Stuff

    private bool charging = false;
    private float chargeTime = 1f;
    public float chargeCounter = 0f;

    private bool waiting = false;
<<<<<<< HEAD
    private float waitTime = 2f;
=======
    public float waitTime = 2f;
    public float MediumWaitTime;
    public float HardWaitTime;
>>>>>>> Spector-Stuff
    public float waitCounter = 0f;

    public Vector3 targetLocation;
    private Vector3 moveVector;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
        Set_Attack_Time(1.5f);
        Set_Attacking(true);
<<<<<<< HEAD
=======
        anim = this.gameObject.GetComponent<Animator>();
>>>>>>> Spector-Stuff
    }
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD

        if(!charging && !waiting)
        {
            Set_Direction();
=======
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
            Flip();
>>>>>>> Spector-Stuff
        }
        checkInvincible();
        Check_If_Dead();


<<<<<<< HEAD
        if (Get_Attacking())
=======
        if (Get_Attacking() && !Get_Stunned())
>>>>>>> Spector-Stuff
        {
            Prepare_Attack2();
            if(chargeTime <= chargeCounter)
            {
<<<<<<< HEAD
                Attack_Player();
            }
=======
                anim.SetBool("Attacking", true);
                anim.SetBool("Charging", false);
                Attack_Player();
            }
            else if(!waiting)
            {
                anim.SetBool("Charging", true);
            }
>>>>>>> Spector-Stuff
        }

        if(waitTime <= waitCounter)
        {
<<<<<<< HEAD
            waitCounter = 0;
            waiting = false;
        }
	}

    public void SetValues(int val)
    {
        Set_Difficulty(val);
        switch (val)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
        }
    }

=======
            if(!Get_Stunned())
            {
                anim.SetBool("Stunned", false);
                waitCounter = 0;
                waiting = false;
            }
        }
	}

>>>>>>> Spector-Stuff
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
<<<<<<< HEAD
=======
            anim.SetBool("Stunned", true);
            anim.SetBool("Attacking", false);
>>>>>>> Spector-Stuff
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
<<<<<<< HEAD
=======

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
>>>>>>> Spector-Stuff
}
