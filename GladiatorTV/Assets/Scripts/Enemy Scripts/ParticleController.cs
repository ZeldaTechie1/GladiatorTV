using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public float seconds;
    // Use this for initialization
    void Start()
    {
        //ParticleSystem ps = GetComponent<ParticleSystem>();
        Destroy(this.gameObject, seconds);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
