﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

    BoxCollider2D trigger;

	void Start ()
    {
        trigger = gameObject.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
       if(collider.CompareTag("Player"))
        {
            Doors door = GetComponentInParent<Doors>();
            door.RoomTriggered();   
        }
        
    }

    
}
