using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheController : BaseEnemy {

    public GameObject Weapon;
    public ScytheWeapon WeaponController;
    public bool startedSwing = false;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
        Set_Attacking(false);
        WeaponController = Weapon.GetComponent<ScytheWeapon>();
        Weapon.SetActive(false);
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
            Weapon.SetActive(false);
        }

        if(Get_Attacking() && !startedSwing)
        {
            startedSwing = true;
            Weapon.SetActive(true);
            WeaponController.SetRotation(direction);
            WeaponController.Set_currentDir(direction);
            WeaponController.Set_Swinging(true);
        }
        else if(Get_Attacking() && startedSwing)
        {
            if(WeaponController.Get_Swinging() == false)
            {
                startedSwing = false;
                Weapon.SetActive(false);
                Set_Attacking(false);
                Set_Direction();
            }
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
