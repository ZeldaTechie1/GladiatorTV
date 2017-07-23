using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheBlade : MonoBehaviour
{

    PlayerController player_con;
    ScytheController Scythe;
    public int bladeDamage;
    ScytheWeapon Weapon;
    // Use this for initialization
    void Start()
    {
        player_con = GameObject.Find("Player").GetComponent<PlayerController>();
        Scythe = GetComponentInParent<ScytheController>();
        Set_Blade_Damage(Scythe.AttackDamage);
        Weapon = GetComponentInParent<ScytheWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bladeDamage != Scythe.AttackDamage)
        {
            Set_Blade_Damage(Scythe.AttackDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_con.Deal_Damage(bladeDamage);
        }

        if (collision.gameObject.name == "Scythe")
        {
            if (Weapon.Get_Pull())
            {
                Weapon.Set_Pull(false);
                Weapon.Set_Swinging(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_con.Deal_Damage(bladeDamage);
        }

        if (collision.gameObject.name == "Scythe")
        {
            if (Weapon.Get_Pull())
            {
                Weapon.Set_Pull(false);
                Weapon.Set_Swinging(false);
            }
        }
    }

    public void Set_Blade_Damage(int val)
    {
        bladeDamage = val;
    }
}
