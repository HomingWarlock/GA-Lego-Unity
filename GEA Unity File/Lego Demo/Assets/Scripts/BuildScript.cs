using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    private PlayerMovement player_script;
    private bool move_delay;
    [SerializeField] private GameObject basic_block;

    void Start()
    {
        player_script = GameObject.Find("Player").GetComponent<PlayerMovement>();
        move_delay = false;
        basic_block = GameObject.Find("BasicBlock");
        basic_block.SetActive(false);
    }

    void Update()
    {
        if (player_script.Mode == "BuildMode")
        {
            if (!move_delay)
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

                if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    move_delay = true;
                    StartCoroutine(MoveDelayReset());
                }

                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                    move_delay = true;
                    StartCoroutine(MoveDelayReset());
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject new_block = Instantiate(basic_block);
                new_block.transform.name = "Basic_Block";
                new_block.SetActive(true);
                new_block.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }
    }

    private IEnumerator MoveDelayReset()
    {
        yield return new WaitForSeconds(0.2f);
        move_delay = false;
    }
}
