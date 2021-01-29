using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField, Tooltip("The gameobject that is where the camera will be moved too")] private GameObject[] m_cameraLocations = null;
    [SerializeField, Tooltip("The time it takes for the camera to change rooms")] private float m_cameraMoveTime = 0.0f;

    //Camera
    private Camera m_mainCamera = null;

    //Camera Location
    private Vector3 m_cameraStartLocation = Vector3.zero;
    private int m_currentRoomTracker = 0;

    //Camera Movement
    private bool m_movingCamera = false;
    private float m_distanceMoved = 0.0f;

    private void Start()
    {
        //Initalizes varables
        m_currentRoomTracker = 0;
        m_mainCamera = Camera.main;
        m_mainCamera.transform.position = m_cameraLocations[m_currentRoomTracker].transform.position;

        //Makes only the current room active
        ActiveCurrentRoom();
    }

    private void FixedUpdate()
    {
        //Checks if the camera is moving
        if (m_movingCamera)
        {
            //Increases the distance
            m_distanceMoved += Time.deltaTime / m_cameraMoveTime;

            //Moves the camera towards the target
            m_mainCamera.transform.position = Vector3.Lerp(m_cameraStartLocation, m_cameraLocations[m_currentRoomTracker].transform.position, m_distanceMoved);

            //Checks to see if the camera has reached its target
            if (m_mainCamera.transform.position == m_cameraLocations[m_currentRoomTracker].transform.position)
            {
                m_movingCamera = false;
            }
        }
    }

    public void CameraMove(int advancedRoom)
    {
        //Increases/Decreases index
        m_currentRoomTracker = advancedRoom;
        ActiveCurrentRoom();

        //Stops bad indexing
        if (m_currentRoomTracker > m_cameraLocations.Length - 1)
        {
            m_currentRoomTracker = m_cameraLocations.Length - 1;
        }
        else if (m_currentRoomTracker < 0)
        {
            m_currentRoomTracker = 0;
        }

        //Sets up the start of the camera movement
        m_cameraStartLocation = m_mainCamera.transform.position;
        m_movingCamera = true;
        m_distanceMoved = 0.0f;
    }


    private void ActiveCurrentRoom()
    {
        //Goes through all locations
        for (int i = 0; i < m_cameraLocations.Length; i++)
        {
            //Checks if the location is the current room
            if (m_currentRoomTracker == i)
            {
                //Current room so makes active
                m_cameraLocations[i].SetActive(true);
            }
            else
            {
                //Not current room so makes deactive
                m_cameraLocations[i].SetActive(false);
            }
        }
    }
}
