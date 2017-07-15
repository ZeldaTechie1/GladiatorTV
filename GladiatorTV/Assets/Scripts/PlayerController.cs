using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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
    // Use this for initialization
    void Start () {
        playerSpeed = baseSpeed;
        anim = this.gameObject.GetComponent<Animator>();
        Init_Vals();

        InvokeRepeating("IncrementCounters", 0f, .25f);
    }
	
	// Update is called once per frame
	void Update () {
        Set_Direction();
        Check_Health();
        checkInvincible();
        if(deathCounter >= deathTime)
        {
            dead = true;
            anim.SetBool("Dead", true);
        }
    }

    private void FixedUpdate()
    {
        Move();   
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
        if(!invincible)
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
        transform.position += move.normalized * playerSpeed * Time.deltaTime;
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
        if(dying)
        {
            deathCounter += .25f;
        }
    }

    private void Check_Health()
    {
        if(health <= 0)
        {
            health = 0;
            dying = true;
            anim.SetBool("Dying", true);
        }
    }
}
