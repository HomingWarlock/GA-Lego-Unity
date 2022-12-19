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
    public BuildScript build_script;
    private bool change_mode_once;
    private bool can_enter_vehicle;
    public bool driving_vehicle;
    private GameObject[] vehicle_parts;
    private int vehicle_pieces_near;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        build_cube = GameObject.Find("BuildCube");
        build_script = build_cube.GetComponent<BuildScript>();
        cam = GameObject.Find("Main Camera");
        cam.transform.SetParent(this.gameObject.transform);
        cam.transform.localPosition = new Vector3(-17, 5, 0);
        cam_rotation = 0;
        Cursor.lockState = CursorLockMode.Locked;

        Mode = "PlayMode";
        ModeText = GameObject.Find("ModeText").GetComponent<Text>();
        ModeText.text = "Play Mode";
        ModeToggleDelay = false;
        change_mode_once = false;
        can_enter_vehicle = false;
        driving_vehicle = false;
        vehicle_pieces_near = 0;
    }

    void Update()
    {
        if (Mode == "PlayMode" && !driving_vehicle)
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if (Input.GetKeyDown(KeyCode.B) && !ModeToggleDelay && !driving_vehicle)
        {            
            if (Mode == "PlayMode")
            {
                Mode = "BuildMode";
                ModeText.text = "Build Mode";
                build_cube.SetActive(true);
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
                build_cube.SetActive(false);
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                cam.transform.SetParent(this.gameObject.transform);
                cam.transform.localPosition = new Vector3(-17, 5, 0);
                cam.transform.localRotation = Quaternion.Euler(14, 90, 0);
                ModeToggleDelay = true;
                StartCoroutine(ModeToggleDelayReset());
            }
        }

        if (build_script.vehicle_built && !change_mode_once)
        {
            vehicle_parts = GameObject.FindGameObjectsWithTag("Block");
            Mode = "PlayMode";
            ModeText.text = "Play Mode";
            build_cube.SetActive(false);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            cam.transform.SetParent(this.gameObject.transform);
            cam.transform.localPosition = new Vector3(-17, 5, 0);
            cam.transform.localRotation = Quaternion.Euler(14, 90, 0);
            ModeToggleDelay = true;
            StartCoroutine(ModeToggleDelayReset());
            change_mode_once = true;
        }

        if (!driving_vehicle)
        {
            foreach (GameObject target in vehicle_parts)
            {
                if (Vector3.Distance(target.transform.position, this.transform.position) < 2)
                {
                    vehicle_pieces_near += 1;
                }
            }

            if (vehicle_pieces_near > 0)
            {
                can_enter_vehicle = true;
            }
            else if (vehicle_pieces_near == 0)
            {
                can_enter_vehicle = false;
            }

            vehicle_pieces_near = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Mode == "PlayMode" && can_enter_vehicle && !driving_vehicle)
        {
            can_enter_vehicle = false;
            driving_vehicle = true;
            cam.transform.SetParent(build_script.vehicle_parent.transform);
            cam.transform.localPosition = new Vector3(-17, 5, 0);
            cam.transform.localRotation = Quaternion.Euler(14, 90, 0);
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator ModeToggleDelayReset()
    {
        yield return new WaitForSeconds(0.5f);
        ModeToggleDelay = false;
    }
}
