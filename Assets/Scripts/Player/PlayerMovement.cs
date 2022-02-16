using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool canMove;
    public float mouseSens;

    private AudioSource footstepSource;
    private GameObject playerCam;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;

        footstepSource = GetComponent<AudioSource>();

        playerCam = transform.GetChild(0).gameObject;
        playerCam.transform.rotation = Quaternion.identity;

        speed = 2;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {//Hide cursor for testing purposes
            if (Input.GetMouseButtonDown(0)) Cursor.visible = false;
            if (!Cursor.visible)
            {
                //Camera controls
                float yRot = Input.GetAxis("Mouse Y") * /*Time.deltaTime **/ mouseSens;
                float xRot = Input.GetAxis("Mouse X") * /*Time.deltaTime **/ mouseSens;
                //Rotate only cam on Y axis
                if (yRot > 0 && playerCam.transform.localRotation.x >= -0.4f) //Looking Up
                    playerCam.transform.Rotate(new Vector3(-yRot, 0, 0), Space.Self);
                if (yRot < 0 && playerCam.transform.localRotation.x <= 0.4f) //Looking Down
                    playerCam.transform.Rotate(new Vector3(-yRot, 0, 0), Space.Self);
                //Rotate player on X axis
                if (xRot != 0) //Looking Around
                    transform.Rotate(new Vector3(0, xRot, 0), Space.World);
            }



            //Movement controls
            Vector3 movement;
            if (Input.GetKey(KeyCode.W)) //Forward
            {
                movement = playerCam.transform.forward * Time.deltaTime * speed;
                movement.y = 0;
                transform.position += movement;
                if (!footstepSource.isPlaying)
                    footstepSource.Play(0);
            }
            if (Input.GetKey(KeyCode.S)) //Backward
            {
                movement = playerCam.transform.forward * Time.deltaTime * speed;
                movement.y = 0;
                transform.position -= movement;
                if (!footstepSource.isPlaying)
                    footstepSource.Play(0);
            }
            if (Input.GetKey(KeyCode.A)) //Left
            {
                movement = playerCam.transform.right * Time.deltaTime * speed;
                movement.y = 0;
                transform.position -= movement;
                if (!footstepSource.isPlaying)
                    footstepSource.Play(0);
            }
            if (Input.GetKey(KeyCode.D)) //Right
            {
                movement = playerCam.transform.right * Time.deltaTime * speed;
                movement.y = 0;
                transform.position += movement;
                if (!footstepSource.isPlaying)
                    footstepSource.Play(0);
            }
        }


    }
}
