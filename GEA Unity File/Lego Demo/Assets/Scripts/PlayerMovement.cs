using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float move_speed = 6f;
    private Rigidbody rb;
    private GameObject cam;
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
                cam.transform.SetParent(build_cube.transform);
                cam.transform.localPosition = new Vector3(-17, 5, 0);
                ModeToggleDelay = true;
                StartCoroutine(ModeToggleDelayReset());

            }
            else
            {
                Mode = "PlayMode";
                ModeText.text = "Play Mode";
                cam.transform.SetParent(this.gameObject.transform);
                cam.transform.localPosition = new Vector3(-17, 5, 0);
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
