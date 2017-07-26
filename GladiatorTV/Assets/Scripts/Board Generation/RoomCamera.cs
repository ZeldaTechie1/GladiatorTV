using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCamera : MonoBehaviour {
    public DoorEventSystem dooreventsystem;
    bool OnPoint = true;
    public GameBoard board;
    Camera camera;
    public Transform currentposition;
    Vector3 centerposition;
    Transform newPosition;

    // Use this for initialization
    public static float pixlesToUnits = 1f;// Ratio of Pixles to number of units 
    public static float scale = 1f;
    public Vector2 nativeReselution = new Vector2(240, 160); //Intended Resolution

    void Awake()
    {

        camera = GetComponent<Camera>();

        if (camera.orthographic)
        {
            scale = Screen.height / nativeReselution.y;// Scale is equal to the screen's height divided by the Native Resolution's height
            pixlesToUnits += scale;// Used to set a refrence point for resolution changes

            camera.orthographicSize = (351f);//Changing the size of orthagraphic camera
        }

    }
    void Start ()
    {
       

        currentposition = dooreventsystem.CurrentRoom.roomCenter.transform;

        centerposition = currentposition.position;
        centerposition.z = -10f;

        camera = GetComponent<Camera>();
        camera.transform.position = (centerposition);
        OnPoint = true;
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(!OnPoint)
        {

            camera.transform.position = Vector3.Lerp(camera.transform.position, centerposition, 0.1f);
            if(camera.transform.position==centerposition)
            {
                OnPoint = true;
            }


        }


		
	}

    public void CameraUpdate(Transform newSpot)
    {

        newPosition = newSpot;

        centerposition.x = newPosition.transform.position.x / 2;
        centerposition.y = newPosition.transform.position.y / 2;
        centerposition.z = 10;

        OnPoint = false;

    }
}
