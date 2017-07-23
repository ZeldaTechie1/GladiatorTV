using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheController : BaseEnemy
{

    public GameObject Weapon;
    public ScytheWeapon WeaponController;
    public bool startedSwing = false;
<<<<<<< HEAD

    public float MediumSpeed;
    public float HardSpeed;

    public float rotationSpeed;
    public float MediumRotationSpeed;
    public float HardRotationSpeed;

    public float pullSpeed;
    public float MediumPullSpeed;
    public float HardPullSpeed;
    // Use this for initialization
    void Start()
    {
=======
	// Use this for initialization
	void Start () {
>>>>>>> Room_Generation
        InvokeRepeating("Increment_Counters", 0f, .25f);
        Set_Attacking(false);
        WeaponController = Weapon.GetComponent<ScytheWeapon>();
        Weapon.SetActive(false);
        //Set_Attack_Time(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
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

        checkInvincible();


        if (!Get_Attacking())
        {
            Set_Direction();
<<<<<<< HEAD
            if (!Get_Stunned())
            {
                Move_To_Location(player.transform.position);
            }
            Weapon.SetActive(false);
        }

        if (Get_Attacking() && !startedSwing)
=======
            Move_To_Location(player.transform.position);
            Weapon.SetActive(false);
        }

        if(Get_Attacking() && !startedSwing)
>>>>>>> Room_Generation
        {
            startedSwing = true;
            Weapon.SetActive(true);
            WeaponController.SetRotation(direction);
            WeaponController.Set_currentDir(direction);
            WeaponController.Set_Swinging(true);
        }
<<<<<<< HEAD
        else if (Get_Attacking() && startedSwing)
        {
            if (WeaponController.Get_Swinging() == false)
=======
        else if(Get_Attacking() && startedSwing)
        {
            if(WeaponController.Get_Swinging() == false)
>>>>>>> Room_Generation
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
