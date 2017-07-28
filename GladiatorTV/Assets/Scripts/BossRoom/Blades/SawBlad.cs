using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlad : MonoBehaviour {
    public int damage;
    public float speed;
    public int direction;
    public Vector3 move;
    public float rotationSpeed;
	// Use this for initialization
	void Start () {
        SetVector();
	}
	
	// Update is called once per frame
	void Update () {
        MoveBlade();
        Rotate();
	}

    private void MoveBlade()
    {
        transform.position += move.normalized * speed * Time.deltaTime;
    }

    private void SetVector()
    {
        switch (direction)
        {
            case 0:
                move = new Vector3(0, 1, 0);
                break;
            case 1:
                move = new Vector3(1, 0, 0);
                break;
            case 2:
                move = new Vector3(0, -1, 0);
                break;
            case 3:
                move = new Vector3(-1, 0, 0);
                break;
        }

    }

    public void Rotate()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
