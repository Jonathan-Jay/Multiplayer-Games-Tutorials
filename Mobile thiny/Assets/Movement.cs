using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 15;
    public float rotSpeed = 50;
    public float jumpStrength = 10;

    private float angleX = 0;
    private float angleY = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        movement.x = Input.GetAxis("Horizontal") * speed;
        movement.z = Input.GetAxis("Vertical") * speed;

        movement = Quaternion.AngleAxis(angleX, Vector3.up) * movement;

        movement.y = GetComponent<Rigidbody>().velocity.y;
        if (Input.GetAxis("Jump") > 0)
            movement.y = jumpStrength;
        GetComponent<Rigidbody>().velocity = movement;

        angleX += Input.GetAxis("Mouse X") * Time.deltaTime * rotSpeed * 2;
        angleY -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotSpeed;

        angleY = Mathf.Clamp(angleY, -80, 80);

        if (angleX > 360)   angleX -= 360;
        if (angleX < 0)   angleX += 360;

        transform.rotation = Quaternion.Euler(angleY, angleX, 0);
    }
}
