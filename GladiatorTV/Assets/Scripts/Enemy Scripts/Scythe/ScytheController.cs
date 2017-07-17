using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheController : BaseEnemy {

	// Use this for initialization
	void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
        Set_Attacking(false);
        //Set_Attack_Time(1.5f);
    }
	
	// Update is called once per frame
	void Update () {    
        checkInvincible();
        Check_If_Dead();

        if (!Get_Attacking())
        {
            Set_Direction();
            Move_To_Location(player.transform.position);
        }
	}

    private void Increment_Counters()
    {
        if (Get_Dying())
        {
            Increase_Death_Counter(.25f);
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
}
