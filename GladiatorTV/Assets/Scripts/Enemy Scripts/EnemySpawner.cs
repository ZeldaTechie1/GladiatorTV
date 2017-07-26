using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;

    public float SpawnTime;
    public float counter;

    public int difficulty;
	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("Increment_Counters", 0f, .25f);
    }
	
	// Update is called once per frame
	void Update () {

		if(counter >= SpawnTime)
        {
            SpawnEnemy();
            counter = 0;
        }

	}

    private void Increment_Counters()
    {
        counter += .25f;
    }

    private void SpawnEnemy()
    {
        int type = (int)Random.Range(0, 3);

        switch (type)
        {
            case 0:
                GameObject Object = Instantiate(Enemy1, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                Object.GetComponent<BaseEnemy>().Set_Difficulty(difficulty);
                break;
            case 1:
                GameObject Object2 = Instantiate(Enemy2, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                Object2.GetComponent<BaseEnemy>().Set_Difficulty(difficulty);
                break;
            case 2:
                GameObject Object3 = Instantiate(Enemy3, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                Object3.GetComponent<BaseEnemy>().Set_Difficulty(difficulty);
                break;
        }
    }
}
