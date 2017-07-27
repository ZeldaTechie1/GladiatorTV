using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {

    int value = 5200;

    public void SetMoneyValue(int amount)
    {
        value = amount;
    }

    public int GetMoney()
    {
        return value;
    }

}
