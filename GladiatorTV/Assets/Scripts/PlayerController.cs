using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private int health;
    

    // 0 U, 1 R, 2 D, 3 L
    public int direction;

    public float baseSpeed;
    private float playerSpeed;

    private bool invincible;

    private Animator anim;
    // Use this for initialization
    void Start () {
        playerSpeed = baseSpeed;
        anim = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        SetDirection();
	}

    private void FixedUpdate()
    {
        Move();   
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
    ///////////////////////////////////////////



    public void Deal_Damage(int val)
    {
        health -= val;
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

    private void SetDirection()
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

}
