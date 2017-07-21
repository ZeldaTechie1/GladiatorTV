using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {
    
    //basic variables
    public int health = 100;
    public int direction;
    public float baseSpeed;
    private float speed;
    public Animator anim;
    public GameObject player;
    public PlayerController player_con;

    //Variables dealing with attacking
    private bool attacking;
    private float attackTime;
    private float attackCounter = 0;
    public int AttackDamage;
    public int TouchDamage;

    //variables dealing with taking damage/getting hit
    private bool invincible;
    private bool hit = false;
    private bool invinCalled = false;
    private IEnumerator coroutine;
    private float invincibleTime = 1f;

    //Difficulty
    public enum Difficulty { Easy, Medium, Hard, Lunatic}
    private int thisDifficutly;

    //Death
    private bool dying = false;
    private float deathCounter = 0;
    private float deathTime = 1;
    private SpriteRenderer SpriteRend;

    private bool stunned = false;
    private void Awake()
    {
        player = GameObject.Find("Player");
        player_con = player.GetComponent<PlayerController>();
        speed = baseSpeed;
        SpriteRend = this.gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame

    public void Deal_Damage(int val)
    {
        if (!invincible)
        {
            health -= val;
            hit = true;
        }
        
        if(health <= 0)
        {
            health = 0;
            dying = true;
        }
    }

    public IEnumerator WasDamaged()
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

    public void checkInvincible()
    {
        if (hit && !invinCalled)
        {
            invincible = true;
            coroutine = WasDamaged();
            StartCoroutine(coroutine);
        }
    }

    public float Get_Angle_to_Player()
    {
        float angle = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg;
        return angle;
    }

    public void Set_Direction()
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

    public bool Get_Invincible()
    {
        return invincible;
    }

    public int Get_Difficulty()
    {
        return thisDifficutly;
    }

    public void Set_Difficulty(int val)
    {
        thisDifficutly = val;
    }

    public bool Get_Attacking()
    {
        return attacking;
    }

    public void Set_Attacking(bool val)
    {
        attacking = val;
    }

    public float Get_Attack_Time()
    {
        return attackTime;
    }

    public void Set_Attack_Time(float val)
    {
        attackTime = val;
    }

    public void Increase_Attack_Counter(float val)
    {
        attackCounter += val;
    }

    public void Reset_Attack_Counter()
    {
        attackCounter = 0;
    }

    public float Get_Attack_Counter()
    {
        return attackCounter;
    }

    public void Set_Dying(bool val)
    {
        dying = val;
    }

    public bool Get_Dying()
    {
        return dying;
    }

    public float Get_Death_Time()
    {
        return deathTime;
    }

    public void Set_Death_Time(float val)
    {
        deathTime = val;
    }

    public void Increase_Death_Counter(float val)
    {
        deathCounter += val;
    }

    public bool Check_If_Dead()
    {
        if(deathCounter == deathTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Move_To_Location(Vector3 Target)
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
    }

    public void Set_Invincible_Time(float val)
    {
        invincibleTime = val;
    }

    public void Set_Speed(float val)
    {
        speed = val;
    }

    public void Flip()
    {
        if (direction == 3)
        {
            if (SpriteRend.flipX)
            {

            }
            else
            {
                SpriteRend.flipX = true;
            }
        }
        if (direction == 1)
        {
            if (SpriteRend.flipX)
            {
                SpriteRend.flipX = false;
            }
            else
            {

            }
        }
    }

    public void Set_Stunned(bool val)
    {
        stunned = val;
    }

    public bool Get_Stunned()
    {
        return stunned;
    }
}
