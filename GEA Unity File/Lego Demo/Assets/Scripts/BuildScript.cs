using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildScript : MonoBehaviour
{
    private PlayerMovement player_script;
    private bool move_delay;
    private GameObject basic_block;
    public bool is_overlapping;
    private GameObject overlap_object;
    private Text overlap_text;
    private bool ui_delay;
    public bool is_connected;
    private bool first_block;
    public GameObject vehicle_parent;
    public Rigidbody vehicle_rb;
    public bool vehicle_exists;
    public bool vehicle_built;

    void Awake()
    {
        player_script = GameObject.Find("Player").GetComponent<PlayerMovement>();
        move_delay = false;
        basic_block = GameObject.Find("BasicBlock");
        basic_block.SetActive(false);
        is_overlapping = false;
        overlap_object = GameObject.Find("OverlapText");
        overlap_object.SetActive(false);
        overlap_text = overlap_object.GetComponent<Text>();
        overlap_text.text = "";
        ui_delay = false;
        is_connected = false;
        first_block = false;
        vehicle_parent = GameObject.Find("VehicleParent");
        vehicle_rb = vehicle_parent.GetComponent<Rigidbody>();
        vehicle_rb.useGravity = false;
        this.gameObject.SetActive(false);
        vehicle_exists = false;
        vehicle_built = false;
    }

    void Update()
    {
        if (player_script.Mode == "BuildMode")
        {
            if (!move_delay)
            {
                if (player_script.cam_rotation == 0)
                {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }

                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }

                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }

                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                }
                else if (player_script.cam_rotation == 90)
                {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }

                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }

                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }

                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                }
                else if (player_script.cam_rotation == 180)
                {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }

                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }

                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }

                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                }
                else if (player_script.cam_rotation == 270)
                {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }

                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }

                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }

                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                }

                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    move_delay = true;
                    StartCoroutine(MoveDelayReset());
                }

                if (transform.position.y != 4)
                {
                    if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (player_script.cam_rotation == 0)
                    {
                        player_script.cam_rotation = 90;
                        player_script.cam.transform.localPosition = new Vector3(0, 5, 17);
                        player_script.cam.transform.localRotation = Quaternion.Euler(14, -180, 0);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                    else if (player_script.cam_rotation == 90)
                    {
                        player_script.cam_rotation = 180;
                        player_script.cam.transform.localPosition = new Vector3(17, 5, 0);
                        player_script.cam.transform.localRotation = Quaternion.Euler(14, -90, 0);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                    else if (player_script.cam_rotation == 180)
                    {
                        player_script.cam_rotation = 270;
                        player_script.cam.transform.localPosition = new Vector3(0, 5, -17);
                        player_script.cam.transform.localRotation = Quaternion.Euler(14, 0, 0);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                    else if (player_script.cam_rotation == 270)
                    {
                        player_script.cam_rotation = 0;
                        player_script.cam.transform.localPosition = new Vector3(-17, 5, 0);
                        player_script.cam.transform.localRotation = Quaternion.Euler(14, -270, 0);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (player_script.cam_rotation == 0)
                    {
                        player_script.cam_rotation = 270;
                        player_script.cam.transform.localPosition = new Vector3(0, 5, -17);
                        player_script.cam.transform.localRotation = Quaternion.Euler(14, 0, 0);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                    else if (player_script.cam_rotation == 270)
                    {
                        player_script.cam_rotation = 180;
                        player_script.cam.transform.localPosition = new Vector3(17, 5, 0);
                        player_script.cam.transform.localRotation = Quaternion.Euler(14, -90, 0);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                    else if (player_script.cam_rotation == 180)
                    {
                        player_script.cam_rotation = 90;
                        player_script.cam.transform.localPosition = new Vector3(0, 5, 17);
                        player_script.cam.transform.localRotation = Quaternion.Euler(14, -180, 0);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                    else if (player_script.cam_rotation == 90)
                    {
                        player_script.cam_rotation = 0;
                        player_script.cam.transform.localPosition = new Vector3(-17, 5, 0);
                        player_script.cam.transform.localRotation = Quaternion.Euler(14, -270, 0);
                        move_delay = true;
                        StartCoroutine(MoveDelayReset());
                    }
                }
            }

            if (!first_block)
            {
                is_connected = true;
            }

            if (!vehicle_exists)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !is_overlapping && is_connected)
                {
                    GameObject new_block = Instantiate(basic_block);
                    new_block.transform.name = "Basic_Block";
                    new_block.SetActive(true);
                    new_block.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    new_block.transform.SetParent(vehicle_parent.transform);
                    vehicle_rb.mass += 5;

                    if (!first_block)
                    {
                        first_block = true;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Space) && is_overlapping && !ui_delay)
                {
                    overlap_object.SetActive(true);
                    overlap_text.text = "Block cannot place, Block Overlap";
                    ui_delay = true;
                    StartCoroutine(UiDelayReset(1f));
                }
                else if (Input.GetKeyDown(KeyCode.Space) && !is_connected && !ui_delay)
                {
                    overlap_object.SetActive(true);
                    overlap_text.text = "Block cannot place, Connecting Block too Far";
                    ui_delay = true;
                    StartCoroutine(UiDelayReset(1f));
                }

            }
            else if (vehicle_exists)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !ui_delay)
                {
                    overlap_object.SetActive(true);
                    overlap_text.text = "Block cannot place, New Vehicle needed \n Delete function not included";
                    ui_delay = true;
                    StartCoroutine(UiDelayReset(2f));
                }
            }

            if (Input.GetKeyDown(KeyCode.N) && !vehicle_exists && first_block)
            {
                vehicle_rb.useGravity = true;
                vehicle_exists = true;
                vehicle_built = true;
            }
            else if (Input.GetKeyDown(KeyCode.N) && vehicle_exists)
            {
                overlap_object.SetActive(true);
                overlap_text.text = "Vehicle already Exists, Delete function not included";
                ui_delay = true;
                StartCoroutine(UiDelayReset(1.2f));
            }
            else if (Input.GetKeyDown(KeyCode.N) && !vehicle_exists && !first_block)
            {
                overlap_object.SetActive(true);
                overlap_text.text = "Empty Build, Nothing to convert to Vehicle";
                ui_delay = true;
                StartCoroutine(UiDelayReset(1f));
            }
        }
    }

    private IEnumerator MoveDelayReset()
    {
        yield return new WaitForSeconds(0.2f);
        move_delay = false;
    }

    private IEnumerator UiDelayReset(float wait_seconds)
    {
        yield return new WaitForSeconds(wait_seconds);
        ui_delay = false;
        overlap_object.SetActive(false);
    }
}
