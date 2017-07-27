using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will be the parent of any and all weapons
public class Weapon : MonoBehaviour {

    [SerializeField]
    string weaponName;
    [SerializeField]
    float fireRate;
    [SerializeField]
    float range;
    [SerializeField]
    WeaponSpawner.weaponQuality quality;
    [SerializeField]
    float damage;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetWeaponName()
    {
        return weaponName;
    }
    public float GetFireRate()
    {
        return fireRate;
    }
    public float GetRange()
    {
        return range;
    }
    public WeaponSpawner.weaponQuality GetQuality()
    {
        return quality;
    }
    public float GetDamage()
    {
        return damage;
    }
}
