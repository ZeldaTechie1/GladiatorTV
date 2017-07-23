using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockTrap : MonoBehaviour {
    public int trapDamage;

    public bool active;

    public float minTime;
    public float maxTime;
    public float currentTimer;

    public float counter;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
        Get_New_Time();
    }
	
	// Update is called once per frame
	void Update () {

		if(counter >= currentTimer)
        {
            Change_State();
            counter = 0;
            Get_New_Time();
        }

	}

    private void Increment_Counters()
    {
        counter += .25f;
    }

    private void Change_State()
    {
        if(active)
        {
            
        }
        else
        {

        }
        active = !active;
    }

    private void Get_New_Time()
    {
        currentTimer = (int)Random.Range(minTime, maxTime);
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
