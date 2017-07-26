using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBlades : MonoBehaviour {

    public bool spawn = false;
    public int damage;
    public float speed;
    public int direction;
    // Use this for initialization
    private void Awake()
    {
        SetValues();
    }

    // Update is called once per frame
    void Update () {
		if(spawn)
        {
            SpawnBlades();
        }
	}

    public void SpawnBlades()
    {
        spawn = false;
        foreach (Transform child in transform)
        {
            child.GetComponent<BladeSpawner>().spawnBlade();
        }
    }

    public void SetValues()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<BladeSpawner>().speed = speed;
            child.GetComponent<BladeSpawner>().damage = damage;
            child.GetComponent<BladeSpawner>().direction = direction;
        }
    }
}
