using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Transform cam;
    private CinemachineFreeLook free_cam;
    private float move_speed = 6f;
    private float turn_smooth_time = 0.1f;
    float turn_smooth_velocity;

    public string Mode;
    private Text ModeText;
    private bool ModeToggleDelay;
    private GameObject build_cube;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = GameObject.Find("Main Camera").transform;
        free_cam = GameObject.Find("FreeLookCamera").GetComponent<CinemachineFreeLook>(); ;
        free_cam.LookAt = this.transform;
        free_cam.Follow = this.transform;
        build_cube = GameObject.Find("BuildCube");
        Cursor.lockState = CursorLockMode.Locked;

        Mode = "PlayMode";
        ModeText = GameObject.Find("ModeText").GetComponent<Text>(); ;
        ModeText.text = "Play Mode";
        ModeToggleDelay = false;
    }

    void Update()
    {
        if (Mode == "PlayMode")
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turn_smooth_velocity, turn_smooth_time);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * move_speed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.B) && !ModeToggleDelay)
        {            
            if (Mode == "PlayMode")
            {
                Mode = "BuildMode";
                ModeText.text = "Build Mode";
                ModeToggleDelay = true;
                StartCoroutine(ModeToggleDelayReset());
                free_cam.LookAt = build_cube.transform;
                free_cam.Follow = build_cube.transform;

            }
            else
            {
                Mode = "PlayMode";
                ModeText.text = "Play Mode";
                ModeToggleDelay = true;
                StartCoroutine(ModeToggleDelayReset());
                free_cam.LookAt = this.transform;
                free_cam.Follow = this.transform;
            }
        }
    }

    private IEnumerator ModeToggleDelayReset()
    {
        yield return new WaitForSeconds(0.5f);
        ModeToggleDelay = false;
    }
}
