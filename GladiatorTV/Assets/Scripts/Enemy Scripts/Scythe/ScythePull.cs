using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScythePull : MonoBehaviour {

    private ScytheWeapon Weapon;
	// Use this for initialization
	void Start () {
        Weapon = GetComponentInParent<ScytheWeapon>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Weapon.Set_Pull(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Weapon.Set_Pull(true);
        }
    }
}
