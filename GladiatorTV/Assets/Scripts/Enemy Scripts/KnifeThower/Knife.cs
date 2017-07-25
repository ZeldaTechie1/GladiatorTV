using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{

    public int damage;
    PlayerController playerCon;
    GameObject player;
    Vector3 targetLocation;

    public float speed;
    public float rotationSpeed;

    private bool reachedTarget;

    private float selfDestructTime = 1.5f;
    private float selfDestructCounter = 0f;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerCon = player.GetComponent<PlayerController>();
        Set_Target(player.transform.position);

        InvokeRepeating("IncrementCounters", 0f, .25f);
    }

    // Update is called once per frame
    void Update()
    {
        Check_Location();

        if (!reachedTarget)
            Rotate();

        Move_To_Location();

        if (selfDestructCounter == selfDestructTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !reachedTarget)
        {
            playerCon.Deal_Damage(damage);
            Destroy(this.gameObject);
        }
    }


    public void Set_Target(Vector3 val)
    {
        targetLocation = val;
    }

    public Vector3 Get_Target()
    {
        return targetLocation;
    }

    public void Move_To_Location()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
    }

    public void Rotate()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void Check_Location()
    {
        if (this.transform.position == targetLocation)
        {
            reachedTarget = true;
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void IncrementCounters()
    {
        if (reachedTarget)
            selfDestructCounter += .25f;
    }

    public void Set_Speed(float val)
    {
        speed = val;
    }

    public void Set_Damage(int val)
    {
        damage = val;
    }
}
