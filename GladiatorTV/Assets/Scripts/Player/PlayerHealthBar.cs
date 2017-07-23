using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
    public GameObject Player;
    public PlayerController Player_con;
    public Slider HealthBar;
    
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
        Player_con = Player.GetComponent<PlayerController>();
        HealthBar = this.GetComponent<Slider>();
        Get_Player_Health();
    }
	
	// Update is called once per frame
	void Update () {
        Get_Player_Health();
    }

    private void Get_Player_Health()
    {
        HealthBar.value = Player_con.Get_Health();
    }
}
