using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheController : BaseEnemy {

    public GameObject Weapon;
    public ScytheWeapon WeaponController;
    public bool startedSwing = false;

    public float MediumSpeed;
    public float HardSpeed;

    public float rotationSpeed;
    public float MediumRotationSpeed;
    public float HardRotationSpeed;

    public float pullSpeed;
    public float MediumPullSpeed;
    public float HardPullSpeed;
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
        if (!CheckDifficulty())
        {
            Set_Values(Get_Difficulty());
            Set_Current_Difficulty(Get_Difficulty());
        }

        checkInvincible();
        Check_If_Dead();

        if (!Get_Attacking())
        {
            Set_Direction();
            if(!Get_Stunned())
            {
                Move_To_Location(player.transform.position);
            }
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

    private void Set_Values(int val)
    {
        switch (val)
        {
            case 1:
                health = MediumHealth;
                AttackDamage = MediumAttackDamage;
                TouchDamage = MediumTouchDamage;
                Set_Speed(MediumSpeed);
                rotationSpeed = MediumRotationSpeed;
                pullSpeed = MediumPullSpeed;
                break;
            case 2:
                health = HardHealth;
                AttackDamage = HardAttackDamage;
                TouchDamage = HardTouchDamage;
                Set_Speed(HardSpeed);
                rotationSpeed = HardRotationSpeed;
                pullSpeed = HardPullSpeed;
                break;
        }
    }
}
