
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    CharacterController controller;
    AudioSource audio;
    bool isMoving = false;
    public float speed = 1f;
    public float soundWaitTime = 1;
    float tempSoundWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
        tempSoundWaitTime = soundWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (!isMoving && move != Vector3.zero)
		{
            audio.Play();
            isMoving = true;
		}
        else if ((tempSoundWaitTime -= Time.deltaTime) <= 0 || move == Vector3.zero)
        {
            tempSoundWaitTime = soundWaitTime;
            isMoving = false;
        }
        controller.Move(move * Time.deltaTime * speed);
    }
}
