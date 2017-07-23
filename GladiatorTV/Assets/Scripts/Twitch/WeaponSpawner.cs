using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour {

    public TwitchChatController chatcontroller;
    public Weapon[] crappyWeapons;
    public Weapon[] mediocreWeapons;
    public Weapon[] fantasticWeapons;
    public enum weaponQuality
    {
        Fantastic,
        Mediocre,
        Crappy
    };

    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            SpawnWeapon();
        }
    }

    void SpawnWeapon()
    {
        float fame = chatcontroller.fame;
        weaponQuality quality = ValidateQuality(fame);
        spawnRandomWeapon(quality);
    }

    weaponQuality ValidateQuality(float fame)
    {
        weaponQuality finalQuality;
        if(fame < (25 - Mathf.Epsilon))//if fame is below 25%
        {
            finalQuality = weaponQuality.Crappy;
        }
        else if(fame >= 25 && fame < (75 - Mathf.Epsilon))//if fame is from 25% to 74.999999999%
        {
            finalQuality = weaponQuality.Mediocre;
        }
        else
        {
            finalQuality = weaponQuality.Fantastic;
        }
        return finalQuality;
    }
    void spawnRandomWeapon(weaponQuality quality)
    {
        Weapon weaponToSpawn = getWeapon(quality);//grabs weapon based on quality
        /*Debug.Log("Name: " + weaponToSpawn.GetWeaponName());
        Debug.Log("Quality " + weaponToSpawn.GetQuality());
        Debug.Log("Fire Rate: " + weaponToSpawn.GetFireRate());
        Debug.Log("Range: " + weaponToSpawn.GetRange());*/
    }

    Weapon getWeapon(weaponQuality quality)
    {
        Weapon weapon;
        if(quality == weaponQuality.Crappy)
        {
            weapon = crappyWeapons[Random.Range(0, crappyWeapons.Length)];//grabs random crappy weapon
        }
        else if (quality == weaponQuality.Mediocre)
        {
            weapon = mediocreWeapons[Random.Range(0, mediocreWeapons.Length)];//grabs random mediocre weapon
        }
        else
        {
            weapon = fantasticWeapons[Random.Range(0, fantasticWeapons.Length)];//grabs random fantastic weapon
        }
        return weapon;
    }
}
