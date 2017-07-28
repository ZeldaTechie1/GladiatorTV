using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy:MonoBehaviour
{

    //basic variables

    public int health = 100;
    public int MediumHealth;
    public int HardHealth;

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
    public int MediumAttackDamage;
    public int HardAttackDamage;

    public int TouchDamage;
    public int MediumTouchDamage;
    public int HardTouchDamage;

    //variables dealing with taking damage/getting hit
    private bool invincible;
    private bool hit = false;
    private bool invinCalled = false;
    private IEnumerator coroutine;
    private float invincibleTime = 1f;

    //Difficulty
    public enum Difficulty { Easy, Medium, Hard, Lunatic }
    public int thisDifficutly = 0;
    public int currentDifficulty = -1;

    //Death
    private bool dying = false;
    private float deathCounter = 0;
    private float deathTime = 1;
    private SpriteRenderer SpriteRend;
    public DoorEventSystem dooreventsystem;
    GameObject gameBoard;


    private bool stunned = false;


    public GameObject EasyDrop1;
    public GameObject EasyDrop2;
    public GameObject MediumDrop1;
    public GameObject MediumDrop2;
    public GameObject HardDrop1;
    public GameObject HardDrop2;

    public GameObject particles;
    public Rigidbody2D Rigid;

    public float pushBackAmount = 5f;
    public int pointsAmount = 2;
    ScoreSystem scoreSystem;

    private void Awake()
    {
        player = GameObject.Find("Player");
        player_con = player.GetComponent<PlayerController>();
        speed = baseSpeed;
        SpriteRend = this.gameObject.GetComponent<SpriteRenderer>();
        Rigid = this.gameObject.GetComponent<Rigidbody2D>();
        scoreSystem = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScoreSystem>();

        gameBoard = GameObject.FindWithTag("BOARD");
        dooreventsystem = gameBoard.GetComponent(typeof(DoorEventSystem)) as DoorEventSystem;
    }
    // Update is called once per frame

    public void Deal_Damage(int val)
    {
        if (!invincible)
        {
            health -= val;
            hit = true;
        }

        if (health <= 0)
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
        if (deathCounter == deathTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Check_HP()
    {
        if (health <= 0)
        {
            health = 0;
            dying = true;
        }
    }
    public bool Check_if_Dying()
    {
        return dying;
    }

    public void Move_To_Location(Vector3 Target)
    {
        //transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
        Rigid.MovePosition(transform.position + Target * Time.deltaTime * speed);
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

    public bool CheckDifficulty()
    {
        if (thisDifficutly == currentDifficulty)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Set_Current_Difficulty(int val)
    {
        currentDifficulty = val;
    }

    public void SpawnWeapon(GameObject Weapon)
    {
        GameObject Object = Instantiate(Weapon, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }

    public void SelectWeapon(int difficulty)
    {
        int WeaponType = (int)Random.Range(0, 1);

        switch(difficulty)
        {
            case 0:
                if(WeaponType == 0)
                {
                    SpawnWeapon(EasyDrop1);
                }
                else
                {
                    SpawnWeapon(EasyDrop2);
                }
                break;

            case 1:
                if (WeaponType == 0)
                {
                    SpawnWeapon(MediumDrop1);
                }
                else
                {
                    SpawnWeapon(MediumDrop2);
                }
                break;

            case 2:
                if (WeaponType == 0)
                {
                    SpawnWeapon(HardDrop1);
                }
                else
                {
                    SpawnWeapon(HardDrop2);
                }
                break;
        }
    }

    public void Die()
    {

        dooreventsystem.EnemiesDestroyed();
        SelectWeapon(currentDifficulty);
        scoreSystem.scoreEvent.Invoke(pointsAmount);
        GameObject Object = Instantiate(particles, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void BounceBack(int direction)
    {
        switch(direction)
        {
            case 0:
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - pushBackAmount, this.gameObject.transform.position.z);
                break;
            case 1:
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - pushBackAmount, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
                break;
            case 2:
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + pushBackAmount, this.gameObject.transform.position.z);
                break;
            case 3:
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + pushBackAmount, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
                break;
        }
    }
}
