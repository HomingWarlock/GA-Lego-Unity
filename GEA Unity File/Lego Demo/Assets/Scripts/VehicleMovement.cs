using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    private GameObject player_object;
    private PlayerMovement player_script;
    private Rigidbody car_rb;
    private float car_speed = 6f;
    private float car_rotate_speed = 40f;
    public Transform enter_pos;

    void Awake()
    {
        player_object = GameObject.Find("Player");
        player_script = GameObject.Find("Player").GetComponent<PlayerMovement>();
        car_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player_script.driving_vehicle)
        {
            if (Input.GetKey(KeyCode.W))
            {
                car_rb.velocity = transform.right * car_speed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                car_rb.velocity = -transform.right * car_speed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    car_rb.velocity = transform.right * car_speed;
                    transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * car_rotate_speed, Space.World);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    car_rb.velocity = -transform.right * car_speed;
                    transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * car_rotate_speed, Space.World);
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    car_rb.velocity = transform.right * car_speed;
                    transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * car_rotate_speed, Space.World);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    car_rb.velocity = -transform.right * car_speed;
                    transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * car_rotate_speed, Space.World);
                }
            }
        }
    }
}