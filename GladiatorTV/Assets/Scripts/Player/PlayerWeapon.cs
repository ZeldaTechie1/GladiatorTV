using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    GameObject Player;
    PlayerController playercon;

    public int direction;
    Vector3 location = new Vector3(0, 0, 0);

    public Sprite fist;
    public Sprite Lance;
    public Sprite Knife;
    public Sprite Scythe;
    SpriteRenderer rend;

    public string currentweapon;
	// Use this for initialization
	void Start () {
        Player = transform.parent.gameObject;
        playercon = Player.GetComponent<PlayerController>();
        rend = this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        direction = playercon.direction;
        currentweapon = playercon.weaponEquipped;
        GetLocation();
        GetWeapon(currentweapon);


        this.gameObject.transform.localPosition = location;
	}

    private void GetLocation()
    {
        switch (direction)
        {
            case 0:
                location = new Vector3(9.1f, -0.5f, 0);
                if (rend.flipX)
                {

                }
                else
                {
                    rend.flipX = true;
                }
                rend.sortingOrder = 0;
                break;
            case 1:
                location = new Vector3(9.1f, -0.5f, 0);
                rend.sortingOrder = 1;
                if (rend.flipX)
                {
                    rend.flipX = false;
                }
                else
                {

                }
                break;
            case 2:
                location = new Vector3(-7.6f, -0.5f, 0);
                rend.sortingOrder = 1;
                if (rend.flipX)
                {
                    rend.flipX = false;
                }
                else
                {

                }
                break;
            case 3:
                location = new Vector3(-7.6f, -0.5f, 0);
                if(rend.flipX)
                {

                }
                else
                {
                    rend.flipX = true;
                }
                break;
        }
    }

    private void GetWeapon(string name)
    {
        switch (name)
        {
            case "fist":
                rend.sprite = fist;
                break;
            case "knife":
                rend.sprite = Knife;
                break;
            case "lance":
                rend.sprite = Lance;
                break;
            case "scythe":
                rend.sprite = Scythe;
                break;
        }
    }
}
