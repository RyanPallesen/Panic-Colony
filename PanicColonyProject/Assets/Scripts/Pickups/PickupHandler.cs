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

    public Transform hubRoomRespawnPoint;

    public Image keyUISlot0;
    public Image keyUISlot1;
    public Image keyUISlot2;
    public Image keyUISlot3;

    bool m_shouldRespawnPlayer = false;
    Transform m_playerTransform;

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
                return keyUISlot0.enabled;
            case 1:
                return keyUISlot1.enabled;
            case 2:
                return keyUISlot2.enabled;
            case 3:
                return keyUISlot3.enabled;
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
            keyUISlot0.enabled == true &&
            keyUISlot1.enabled == true &&
            keyUISlot2.enabled == true &&
            keyUISlot3.enabled == true
            );
    }

    public void OnKeyPicked(int keyIndex, Transform playerTransform)
    {
        m_shouldRespawnPlayer = true;
        m_playerTransform = playerTransform;

        switch (keyIndex)
        {
            case 0:
                keyUISlot0.gameObject.SetActive(true);
                break;
            case 1:
                keyUISlot1.gameObject.SetActive(true);
                break;
            case 2:
                keyUISlot2.gameObject.SetActive(true);
                break;
            case 3:
                keyUISlot3.gameObject.SetActive(true);
                break;
            default:
                throw new Exception("Key index not found, Why are you like this?");
        }
    }

}
