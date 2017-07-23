using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheWeapon : MonoBehaviour {
    private float bottomLeft = 45f;
    private float topLeft = -45f;
    private float bottomRight = 135f;
    private float topRight = -135f;

    private float rotationAmount = 90f;
    public enum Direction {Up,Right,Down,Left}

    public float rotationSpeed = 30;

    private bool swinging;
    private int currentDirection;

    private bool pull = false;

    private ScytheController ScytheEnemy;
    private ScytheBlade Blade;
    public Vector3 targetLocation;
    public Vector3 moveVector;
    public float pullSpeed = 5;
    // Use this for initialization
    void Start () {
        ScytheEnemy = GetComponentInParent<ScytheController>();
        Blade = GetComponentInChildren<ScytheBlade>();
        rotationSpeed = ScytheEnemy.rotationSpeed;
        pullSpeed = ScytheEnemy.pullSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if(rotationSpeed != ScytheEnemy.rotationSpeed)
        {
            rotationSpeed = ScytheEnemy.rotationSpeed;
        }

        if (pullSpeed != ScytheEnemy.pullSpeed)
        {
            pullSpeed = ScytheEnemy.pullSpeed;
        }

        if (swinging)
        {
            //Debug.Log(this.gameObject.transform.eulerAngles.z);
            
            if(!pull)
            {
                Swing(currentDirection);
                if (Check_Swinging(currentDirection))
                {
                    swinging = false;
                }
            }
            else
            {
                Pull_Scythe();
                Move_With_Vector();
            }
        }
	}
    
    public void SetRotation(int Dir)
    {
        this.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        switch (Dir)
        {
            case 0:
                this.gameObject.transform.eulerAngles = new Vector3(0, 0, topLeft);
                break;
            case 1:
                this.gameObject.transform.eulerAngles = new Vector3(0, 0, topRight);
                break;

            case 2:
                this.gameObject.transform.eulerAngles = new Vector3(0, 0, bottomRight);
                break;

            case 3:
                this.gameObject.transform.eulerAngles = new Vector3(0, 0, bottomLeft);
                break;

        }
    }

    public void Swing(int Dir)
    {
        //Quaternion q = Quaternion.AngleAxis(topRight, Vector3.forward); ;
        //switch (Dir)
        //{
        //    case 0:
        //        q = Quaternion.AngleAxis(topRight, Vector3.forward);
        //        break;
        //    case 1:
        //        q = Quaternion.AngleAxis(bottomRight, Vector3.forward);
        //        break;

        //    case 2:
        //        q = Quaternion.AngleAxis(bottomLeft, Vector3.forward);
        //        break;

        //    case 3:
        //        q = Quaternion.AngleAxis(topLeft, Vector3.forward);
        //        break;

        //}

        //this.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        Vector3 Vec = new Vector3(0, 0, -rotationSpeed);
        this.gameObject.transform.Rotate(Vec * Time.deltaTime);
    }

    public void Set_Swinging(bool val)
    {
        swinging = val;
    }

    public bool Get_Swinging()
    {
        return swinging;
    }

    public void Set_Swing_Speed(float val)
    {
        rotationSpeed = val;
    }

    public void Set_currentDir(int val)
    {
        currentDirection = val;
    }

    private bool Check_Swinging(int Dir)
    {
        switch (Dir)
        {
            case 0:
                if(this.gameObject.transform.eulerAngles.z <= 225f)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 1:
                if (this.gameObject.transform.eulerAngles.z <= 135f)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 2:
                if (this.gameObject.transform.eulerAngles.z <= 45f)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 3:
                if (this.gameObject.transform.eulerAngles.z <= 315f && this.gameObject.transform.eulerAngles.z > 45f)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
        return false;
    }

    public void Pull_Scythe()
    {
        targetLocation = ScytheEnemy.gameObject.transform.position;
        moveVector = (targetLocation - Blade.gameObject.transform.position).normalized * pullSpeed;
    }

    public void Set_Pull(bool val)
    {
        pull = val;
    }

    public bool Get_Pull()
    {
        return pull;
    }

    private void Move_With_Vector()
    {
        this.gameObject.transform.position += moveVector * Time.deltaTime;
    }
}
