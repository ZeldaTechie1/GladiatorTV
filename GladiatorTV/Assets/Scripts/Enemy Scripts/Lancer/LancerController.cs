using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerController : BaseEnemy {

    public float chargeSpeed;

    private bool charging = false;
    private float chargeTime = 1f;
    public float chargeCounter = 0f;

    private bool waiting = false;
    private float waitTime = 2f;
    public float waitCounter = 0f;

    public int damage = 20;

    public Vector3 targetLocation;
    private Vector3 moveVector;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
        Set_Attack_Time(1.5f);
        Set_Attacking(true);
    }
	
	// Update is called once per frame
	void Update () {

		if(Get_Attacking())
        {
            Prepare_Attack();
            if(chargeTime <= chargeCounter)
            {
                Attack_Player();
            }
        }

        if(waitTime <= waitCounter)
        {
            waitCounter = 0;
            waiting = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_con.Deal_Damage(damage);
            Move_Back();
            Attack_Complete();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Attack_Complete();
        }
    }
}
