using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int health = 100;


    // 0 U, 1 R, 2 D, 3 L
    public int direction;

    public float baseSpeed;
    private float playerSpeed;

    private bool invincible;

    private Animator anim;

    private bool dying;
    public bool dead;
    public float invincibleTime;

    private bool hit = false;
    private bool invinCalled = false;
    private IEnumerator coroutine;

    private float deathCounter;
    private float deathTime = 1f;

    private bool stunned;

    public int attackDamage;
    public float range;
    public float attackSpeed;


    public bool attacking;
    public float attackDuration;

    public float attackCounter;
    public bool attackCoolDown;

    public GameObject GameOver;
    public GameObject PauseMenu;

    public Rigidbody2D RB;

    public GameObject GameBoard;
    public GameBoard BoardScript;
    public bool spawned = false;
    public SpriteRenderer SpriteRend;

    //the pickup system stuff
    [SerializeField]
    string[] pickups;
    int pickupRadius = 200;
    string weaponEquipped = "hand";
    WeaponSpawner.weaponQuality weaponQuality = WeaponSpawner.weaponQuality.Crappy;
    [SerializeField]
    ScoreSystem score;

    // Use this for initialization
    void Start()
    {
        playerSpeed = baseSpeed;
        anim = this.gameObject.GetComponent<Animator>();
        Init_Vals();

        InvokeRepeating("IncrementCounters", 0f, .25f);
        RB = this.gameObject.GetComponent<Rigidbody2D>();

        GameBoard = GameObject.FindGameObjectWithTag("BOARD");
        BoardScript = GameBoard.GetComponent<GameBoard>();

        SpriteRend = this.gameObject.GetComponent<SpriteRenderer>();
        Go_To_Spawn(BoardScript.Board[BoardScript.roomLocation[0].x][BoardScript.roomLocation[0].y].roomCenter.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(!attackCoolDown)
            {
                if (!attacking)
                {
                    attacking = true;
                }
            }
        }

        if(attacking)
        {
            if(attackCounter >= attackDuration)
            {
                attacking = false;
                attackCoolDown = true;
                attackCounter = 0;
            }
        }

        if(attackCoolDown)
        {
            if(attackCounter >= attackSpeed)
            {
                attackCoolDown = false;
                attackCounter = 0;
            }
        }

        if(!attacking)
        {
            Set_Direction();
        }
        Check_Health();
        checkInvincible();
        if (deathCounter >= deathTime)
        {
            dead = true;
            anim.SetBool("Dead", true);
        }

        if(dead)
        {
            GameOver.SetActive(true);
            Time.timeScale = 0;
        }
        Flip();

        //this is for picking stuff up
        if(Input.GetKeyDown(KeyCode.F))//press the f key to pick stuff up
        {
            //Debug.Log("Trying to pick up ze shit");
            pickup();
        }

    }

    private void pickup()
    {
        Collider2D[] objectsInRadius = Physics2D.OverlapCircleAll(this.transform.position,pickupRadius);//these are all the things in range
        for(int count = 0; count<objectsInRadius.Length;count++)
        {
            GameObject objectInRadius = objectsInRadius[count].gameObject;
            string objectTag = objectInRadius.tag;
            //Debug.Log("Picking up this shit: " + objectTag);
            switch(objectTag)
            {
                case "Weapon":ApplyWeapon(objectInRadius);break;
                case "Money": MoneyPickedUp(objectInRadius);break;
            }
        }
    }

    void ApplyWeapon(GameObject weaponPickUp)
    {
        Weapon weapon = weaponPickUp.GetComponent<Weapon>();
        if(weapon != null)
        {
            range = weapon.GetRange();
            weaponEquipped = weapon.GetWeaponName();
            weaponQuality = weapon.GetQuality();
            attackSpeed = weapon.GetFireRate();
            attackDamage = (int)weapon.GetDamage();
            Destroy(weapon.gameObject);
        }
    }

    void MoneyPickedUp(GameObject moneyPickUp)
    {
        Money money = moneyPickUp.GetComponent<Money>();
        if(money!=null)
        {
            score.scoreEvent.Invoke(money.GetMoney());
            Destroy(money.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(!stunned)
        {
            Move();
        }
        if(stunned)
        {
            RB.velocity = new Vector3(0, 0, 0);
        }
    }


    private void Init_Vals()
    {
        health = 100;
    }


    ////////Get Methods///////////////////////
    public int Get_Health()
    {
        return health;
    }

    public float Get_Speed()
    {
        return playerSpeed;
    }

    public int Get_Direction()
    {
        return direction;
    }

    public bool Get_Invincible()
    {
        return invincible;
    }

    public bool Get_Dying()
    {
        return dying;
    }

    public bool Get_Dead()
    {
        return dead;
    }
    ///////////////////////////////////////////



    public void Deal_Damage(int val)
    {
        if (!invincible)
        {
            health -= val;
            hit = true;
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

    private void Move()
    {

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        //transform.position += move.normalized * playerSpeed * Time.deltaTime;
        RB.velocity= (move.normalized * playerSpeed);
    }

    private float Get_Mouse_Angle()
    {
        Vector2 mousePos;
        Vector3 screenPos;
        mousePos = Input.mousePosition;
        screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg;
        return angle;
    }

    private void Set_Direction()
    {
        float angle = Get_Mouse_Angle();

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

        anim.SetInteger("Direction", direction);
    }


    private void IncrementCounters()
    {
        if (dying)
        {
            deathCounter += .25f;
        }
        if(attackCoolDown || attacking)
        {
            attackCounter += .25f;
        }
    }

    private void Check_Health()
    {
        if (health <= 0)
        {
            health = 0;
            dying = true;
            anim.SetBool("Dying", true);
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

    public void Set_Attack(int val)
    {
        attackDamage = val;
    }

    public void Set_AttackSpeed(int val)
    {
        attackSpeed = val;
    }

    public void Set_Range(int val)
    {
        range = val;
    }

    public int Get_Attack()
    {
        return attackDamage;
    }

    public float Get_AttackSpeed()
    {
        return attackSpeed;
    }

    public float Get_Range()
    {
        return range;
    }

    public void Go_To_Spawn(Vector3 location)
    {
        location = new Vector3(location.x, location.y, -1);
        this.gameObject.transform.position = location;
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
}
