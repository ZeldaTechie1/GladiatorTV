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

    private GameObject player;

    public bool hit = false;
    private bool invinCalled = false;
    private IEnumerator coroutine;
    public float invincibleTime;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        InvokeRepeating("Increment_Counters", 0f, .25f);
    }

    // Update is called once per frame
    void Update () {
        Set_Direction();
        checkInvincible();

        if (attackCounter >= attackTime)
        {
            SpawnProjectile(Knife);
            attackCounter = 0;
        }
	}

    public void Deal_Damage(int val)
    {
        if (!invincible)
        {
            health -= val;
            hit = true;
        }
    }

    private IEnumerator WasDamaged()
    {
        while (hit)
        {
            invinCalled = true;

            GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            yield return new WaitForSeconds(invincibleTime);
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);

            hit = false;
            invincible = false;
            invinCalled = false;
        }
    }

    private void checkInvincible()
    {
        if (hit && !invinCalled)
        {
            invincible = true;
            coroutine = WasDamaged();
            StartCoroutine(coroutine);
        }
    }

    private void SpawnProjectile(GameObject Projectile)
    {
        GameObject Object = Instantiate(Projectile, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
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

    private float Get_Angle_to_Player()
    { 
        float angle = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg;
        return angle;
    }

    private void Set_Direction()
    {
        float angle = Get_Angle_to_Player();

        if (angle >= -45 && angle < 45)
        {
            direction = 1;
        }
        if (angle >= 45 && angle < 135)
        {
            direction = 0;
        }
        if (angle >= 135 || angle < -135)
        {
            direction = 3;
        }
        if (angle >= -135 && angle < -45)
        {
            direction = 2;
        }

        //anim.SetInteger("Direction", direction);
    }
}
