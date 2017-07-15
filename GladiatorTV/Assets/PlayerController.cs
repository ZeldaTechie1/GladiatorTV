using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private int health;


    // 0 N, 1 E, 2 S, 3 W
    public int direction;

    public float playerSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SetDirection();
	}

    private void FixedUpdate()
    {
        Move();   
    }

    public int Get_Health()
    {
        return health;
    }

    public void Deal_Damage(int val)
    {
        health -= val;
    }

    public int Get_Direction()
    {
        return direction;
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
            direction = 2;
        }
        if (angle >= -135 && angle < -45)
        {
            direction = 3;
        }
    }
}
