using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheAttackDetection : MonoBehaviour {

    ScytheController Scythe;

	// Use this for initialization
	void Start () {
        Scythe = GetComponentInParent<ScytheController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Scythe.Set_Attacking(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Scythe.Set_Attacking(true);
        }
    }
}
