
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    CharacterController controller;
    AudioSource audio;
    bool isMoving = false;
    public float speed = 1f;
    public float soundWaitTime = 1;
    float tempSoundWaitTime;
    float animVert, animHorz;
    public Animator animatior;

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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0, vertical);
        
        
        switch(CameraRoomMover.instance.DegreesRotatedClockwise)
        {
            case 0:

                break;
            case 1:
                {
                    float tempX = move.x;
                    float tempZ = move.z;

                    move.x = tempZ;
                    move.z = -tempX;
                }
                break;
            case 2:
                move *= -1;
                break;
            case 3:
                {
                    float tempX = move.x;
                    float tempZ = move.z;

                    move.x = -tempZ;
                    move.z = tempX;
                }
                break;
        }

        /*
        if (vertical > 0)
        {
            animVert = 0;
            animHorz = 1;
        }
        else if (vertical < 0)
        {
            animVert = 0;
            animHorz = -1;
        }

        if (horizontal > 0)
        {
            animHorz = 0;
            animVert = 1;
        }
        else if (horizontal < 0)
        {
            animHorz = 0;
            animVert = -1;
        }
        */

        animatior.SetFloat("Blend_Side", animVert);
        animatior.SetFloat("Blend_ForwradsBack", animHorz);
        animatior.SetBool("Bool_Walkable", (horizontal != 0 || vertical != 0) ? true : false);


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
