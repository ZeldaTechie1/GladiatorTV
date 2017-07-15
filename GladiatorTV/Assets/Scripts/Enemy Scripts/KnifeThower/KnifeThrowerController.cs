using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowerController : MonoBehaviour {

    public GameObject Knife;

    public int health = 100;

    public int direction;

    public float baseSpeed;

    private bool invincible;

    private Animator anim;


    private bool dying;
    private Vector3 targetLocation;
    public bool attacking;

    public float attackTime;
    private float attackCounter = 0;
    // Use this for initialization
    void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
    }

    // Update is called once per frame
    void Update () {
        if(attackCounter == attackTime)
        {
            SpawnProjectile(Knife);
            attackCounter = 0;
        }
	}

    private void SpawnProjectile(GameObject Projectile)
    {
        GameObject Venom = Instantiate(Projectile, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }

    private void Get_Random_Location()
    {

    }

    private void Increment_Counters()
    {
        if (attacking)
        {
            attackCounter += .25f;
        }
    }

}
