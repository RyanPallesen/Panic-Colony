using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PickupHandler : MonoBehaviour
{
    public static PickupHandler instance;

    [SerializeField]
    private Transform hubRoomRespawnPoint;

    private bool keyUISlot0;
    private bool keyUISlot1;
    private bool keyUISlot2;
    private bool keyUISlot3;

    private bool m_shouldRespawnPlayer = false;
    private Transform m_playerTransform;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(instance.gameObject);
            }
        }
        else
        {
            instance = this;
        }
    }

    void LateUpdate()
    {
        if (m_shouldRespawnPlayer)
        {
            m_playerTransform.position = hubRoomRespawnPoint.position;
            print("warped player");
            m_shouldRespawnPlayer = false;
        }
    }


    /// <summary>
    /// Returns true if the key at index is enabled
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool HasKeyWithIndex(int index)
    {
        switch (index)
        {
            case 0:
                return keyUISlot0;
            case 1:
                return keyUISlot1;
            case 2:
                return keyUISlot2;
            case 3:
                return keyUISlot3;
            default:
                throw new Exception("Key index not found, Why are you like this?");
        }
    }


    /// <summary>
    /// returns true if all keys are present
    /// </summary>
    /// <returns></returns>
    public bool HasAllKeys()
    {
        return
            (
            keyUISlot0 == true &&
            keyUISlot1 == true &&
            keyUISlot2 == true &&
            keyUISlot3 == true
            );
    }

    public void OnKeyPicked(int keyIndex, Transform playerTransform)
    {
        m_shouldRespawnPlayer = true;
        m_playerTransform = playerTransform;

        switch (keyIndex)
        {
            case 0:
                keyUISlot0 = true;
                break;
            case 1:
                keyUISlot1 = true;
                break;
            case 2:
                keyUISlot2 = true;
                break;
            case 3:
                keyUISlot3 = true;
                break;
            default:
                throw new Exception("Key index not found, Why are you like this?");
        }
    }

}
