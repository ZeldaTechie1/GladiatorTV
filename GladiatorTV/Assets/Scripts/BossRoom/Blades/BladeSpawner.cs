using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSpawner : MonoBehaviour {

    public GameObject blade;
    public int damage;
    public float speed;
    public int direction;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnBlade()
    {
        GameObject Object = Instantiate(blade, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Object.GetComponent<SawBlad>().speed = speed;
        Object.GetComponent<SawBlad>().damage = damage;
        Object.GetComponent<SawBlad>().direction = direction;
    }
}
