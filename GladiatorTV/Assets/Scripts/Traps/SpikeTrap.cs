using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {
    public int trapDamage;

    public float delayTime;
    public float activeTime;

    public bool triggered;
    public bool activated;

    public float counter;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
    }

    // Update is called once per frame
    void Update () {
        Check_Time();
	}

    private void Increment_Counters()
    {
        if (triggered || activated)
        {
            counter += .25f;
        }
    }

    private void Check_Time()
    {
        if(triggered && !activated)
        {
            if(counter >= delayTime)
            {
                triggered = false;
                activated = true;
                counter = 0;
            }
        }
        if(activated)
        {
            if (counter >= activeTime)
            {
                triggered = false;
                activated = false;
                counter = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && activated)
        {
            PlayerController player_con = collision.GetComponent<PlayerController>();
            player_con.Deal_Damage(trapDamage);
        }

        if (collision.gameObject.CompareTag("Player") && !activated)
        {
            triggered = true;
        }



        if (collision.gameObject.CompareTag("Enemy") && !activated)
        {
            triggered = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && activated)
        {
            PlayerController player_con = collision.GetComponent<PlayerController>();
            player_con.Deal_Damage(trapDamage);
        }

        if (collision.gameObject.CompareTag("Player") && !activated)
        {
            triggered = true;
        }


        if (collision.gameObject.CompareTag("Enemy") && !activated)
        {
            triggered = true;
        }
    }
}
