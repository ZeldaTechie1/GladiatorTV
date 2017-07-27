using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour {
    public int attackDamage;
    public float range;
    public float attackSpeed;

    PlayerController player_con;
    BoxCollider2D thisCollider;

    public int thisDirection;
	// Use this for initialization
	void Start () {
        player_con = this.gameObject.GetComponentInParent<PlayerController>();
        thisCollider = this.gameObject.GetComponent<BoxCollider2D>();
        thisCollider.enabled = false;
        Set_Attack_Values();
	}
	
	// Update is called once per frame
	void Update () {
		if(Check_Active())
        {
            Set_Attack_Values();
            this.thisCollider.enabled = true;
        }
        else
        {
            this.thisCollider.enabled = false;
        }
	}

    public void Set_Attack_Values()
    {
        attackDamage = player_con.Get_Attack();
        attackSpeed = player_con.Get_AttackSpeed();
        range = player_con.Get_Range();

        thisCollider.size = new Vector2(range, thisCollider.size.y);
    }

    private bool Check_Active()
    {
        if (player_con.attacking)
        {
            if (player_con.direction == thisDirection)
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<BaseEnemy>().Deal_Damage(attackDamage);
        }

        if (collision.gameObject.CompareTag("Objective"))
        {
            collision.GetComponent<Objective>().Deal_Damage(attackDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<BaseEnemy>().Deal_Damage(attackDamage);
        }

        if (collision.gameObject.CompareTag("Objective"))
        {
            collision.GetComponent<Objective>().Deal_Damage(attackDamage);
        }
    }
}
