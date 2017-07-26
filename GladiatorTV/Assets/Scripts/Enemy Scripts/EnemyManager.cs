using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    BaseEnemy[] enemies;

    /*private void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            SpawnEnemy(Vector3.zero);
        }
    }*/
    public void SpawnEnemy(Vector3 spawnLocation)
    {
        SpawnEnemy(0, spawnLocation);
    }

    public void SpawnEnemy(int difficulty, Vector3 spawnLocation)
    {
        BaseEnemy enemyToSpawn = enemies[Random.Range(0, enemies.Length)];//selects a random enemy to be spawned
        SpawnEnemy(enemyToSpawn,difficulty, spawnLocation);
    }

    public void SpawnEnemy(BaseEnemy enemyToSpawn, int difficulty, Vector3 spawnLocation)
    {
        enemyToSpawn.Set_Difficulty(difficulty);//difficulty can be set to either 0,1,2 - easy, medium, hard respectively
        Instantiate(enemyToSpawn.gameObject, spawnLocation, Quaternion.identity);//spawns at 0,0
    }
}
