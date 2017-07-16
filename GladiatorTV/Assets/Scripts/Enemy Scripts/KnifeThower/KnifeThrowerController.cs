using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowerController : BaseEnemy {

    public GameObject Knife;

    // Use this for initialization
    void Start () {
        InvokeRepeating("Increment_Counters", 0f, .25f);
        Set_Attack_Time(1.5f);
        Set_Attacking(true);
    }

    // Update is called once per frame
    void Update () {
        Set_Direction();
        checkInvincible();
        Check_If_Dead();

        if (Get_Attack_Counter() >= Get_Attack_Time())
        {
            SpawnProjectile(Knife);
            Reset_Attack_Counter();
        }
	}

    private void SpawnProjectile(GameObject Projectile)
    {
        GameObject Object = Instantiate(Projectile, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }


    private void Increment_Counters()
    {
        if (Get_Attacking())
        {
            Increase_Attack_Counter(.25f);
        }
        if(Get_Dying())
        {
            Increase_Death_Counter(.25f);
        }
    }
}
