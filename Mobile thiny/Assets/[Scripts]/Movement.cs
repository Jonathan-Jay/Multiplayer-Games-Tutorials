using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class Movement : MonoBehaviour
{
	public Joystick movementController;
	public Joystick cameraController;
	public Joystick jumpButton;

    public float speed = 15;
    public float rotSpeed = 50;
    public float jumpStrength = 10;

    private float angleX = 0;
    private float angleY = 0;

    // Start is called before the first frame update
    //void Start()
    //{
        //Cursor.lockState = CursorLockMode.Locked;
    //}

	public Transform head;
	public PhotonView view;

    // Update is called once per frame
    void Update()
    {
		if(!view.IsMine)	return;

        Vector3 movement = Vector3.zero;
        // movement.x = Input.GetAxis("Horizontal") * speed;
        // movement.z = Input.GetAxis("Vertical") * speed;
        movement.x = movementController.Horizontal * speed;
        movement.z = movementController.Vertical * speed;

        movement = Quaternion.AngleAxis(angleX, Vector3.up) * movement;

        movement.y = GetComponent<Rigidbody>().velocity.y;
        //if (Input.GetAxis("Jump") > 0)
        if (jumpButton.Vertical != 0f)
            movement.y = jumpStrength;
        GetComponent<Rigidbody>().velocity = movement;

        // angleX += Input.GetAxis("Mouse X") * Time.deltaTime * rotSpeed * 2;
        // angleY -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotSpeed;
        angleX += cameraController.Horizontal * Time.deltaTime * rotSpeed * 2;
        angleY -= cameraController.Vertical * Time.deltaTime * rotSpeed;

        angleY = Mathf.Clamp(angleY, -80, 80);

        if (angleX > 360)   angleX -= 360;
        if (angleX < 0)   angleX += 360;

        transform.rotation = Quaternion.Euler(angleY, angleX, 0);
    }

	public void SetData(GameObject cameraParent) {
		Joystick[] joys = cameraParent.GetComponentsInChildren<Joystick>();
		foreach (Joystick joy in joys)
		{
			if (joy.name == "MovementC")
				movementController = joy;
			else if (joy.name == "CameraC")
				cameraController = joy;
			else if (joy.name == "JumpB")
				jumpButton = joy;
		}
		CinemachineVirtualCamera brain = cameraParent.GetComponentInChildren<CinemachineVirtualCamera>();
		brain.LookAt = head;
		brain.Follow = transform;
	}
}
