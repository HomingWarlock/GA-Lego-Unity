using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float move_speed = 6f;
    private float rotate_speed = 40f;
    private Rigidbody rb;
    public GameObject cam;
    public int cam_rotation;
    public string Mode;
    private Text ModeText;
    private bool ModeToggleDelay;
    private GameObject build_cube;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        build_cube = GameObject.Find("BuildCube");
        cam = GameObject.Find("Main Camera");
        cam.transform.SetParent(this.gameObject.transform);
        cam.transform.localPosition = new Vector3(-17, 5, 0);
        cam_rotation = 0;
        Cursor.lockState = CursorLockMode.Locked;

        Mode = "PlayMode";
        ModeText = GameObject.Find("ModeText").GetComponent<Text>();
        ModeText.text = "Play Mode";
        ModeToggleDelay = false;
    }

    void Update()
    {
        if (Mode == "PlayMode")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = transform.right * move_speed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = -transform.right * move_speed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = -transform.forward * move_speed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = transform.forward * move_speed;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rotate_speed, Space.World);
            }

            if (Input.GetKey(KeyCode.E))
            {

                transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotate_speed, Space.World);
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
                cam_rotation = 0;
                cam.transform.SetParent(build_cube.transform);
                cam.transform.localPosition = new Vector3(-17, 5, 0);
                cam.transform.localRotation = Quaternion.Euler(14, 90, 0);
                ModeToggleDelay = true;
                StartCoroutine(ModeToggleDelayReset());

            }
            else
            {
                Mode = "PlayMode";
                ModeText.text = "Play Mode";
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                cam.transform.SetParent(this.gameObject.transform);
                cam.transform.localPosition = new Vector3(-17, 5, 0);
                cam.transform.localRotation = Quaternion.Euler(14, 90, 0);
                ModeToggleDelay = true;
                StartCoroutine(ModeToggleDelayReset());
            }
        }
    }

    private IEnumerator ModeToggleDelayReset()
    {
        yield return new WaitForSeconds(0.5f);
        ModeToggleDelay = false;
    }
}
