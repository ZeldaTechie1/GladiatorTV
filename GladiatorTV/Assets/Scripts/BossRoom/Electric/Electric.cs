using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : MonoBehaviour {
    public int trapDamage;

    public bool active;
    public float currentTimer;

    public float counter;
    SpriteRenderer thisRend;
    Animator anim;
    // Use this for initialization
    void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
        thisRend = this.gameObject.GetComponent<SpriteRenderer>();
        anim = this.gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (counter >= currentTimer)
        {
            Change_State();
            counter = 0;
        }
        if (active)
        {
            anim.SetBool("Active", active);
        }
        else
        {
            anim.SetBool("Active", active);
        }
    }

    private void Change_State()
    {
        active = !active;
    }

    public void Increment_Counters()
    {
        if(active)
        {
            counter += .25f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            PlayerController player_con = collision.GetComponent<PlayerController>();
            player_con.Deal_Damage(trapDamage);
        }

        if (collision.gameObject.CompareTag("Enemy") && active)
        {
            BaseEnemy enemyController = collision.GetComponent<BaseEnemy>();
            enemyController.Deal_Damage(trapDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            PlayerController player_con = collision.GetComponent<PlayerController>();
            player_con.Deal_Damage(trapDamage);
        }

        if (collision.gameObject.CompareTag("Enemy") && active)
        {
            BaseEnemy enemyController = collision.GetComponent<BaseEnemy>();
            enemyController.Deal_Damage(trapDamage);
        }
    }
}
