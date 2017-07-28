using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{

    //basic variables

    public int health = 100;
    public int MediumHealth;
    public int HardHealth;

    public GameObject player;
    public PlayerController player_con;

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


    public GameObject ItemDrop;
    private void Awake()
    {
        player = GameObject.Find("Player");
        player_con = player.GetComponent<PlayerController>();
        SpriteRend = this.gameObject.GetComponent<SpriteRenderer>();
        gameBoard = GameObject.FindWithTag("BOARD");
        dooreventsystem = gameBoard.GetComponent(typeof(DoorEventSystem)) as DoorEventSystem;
    }
    // Update is called once per frame
    private void Update()
    {
        Check_HP();
        if (Check_if_Dying())
        {
            Die();
        }
        checkInvincible();
    }
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

    public void Set_Invincible_Time(float val)
    {
        invincibleTime = val;
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

    public void Die()
    {
        dooreventsystem.ObjectiveDestroyed();
        SpawnWeapon(ItemDrop);
        Destroy(this.gameObject);
    }
}
