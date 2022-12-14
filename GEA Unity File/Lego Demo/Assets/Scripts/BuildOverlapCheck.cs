using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildOverlapCheck : MonoBehaviour
{
    private BuildScript build_script;

    void Start()
    {
        build_script = GameObject.Find("BuildCube").GetComponent<BuildScript>();
    }

    private void OnTriggerStay(Collider col)
    {
        if (this.gameObject.name == "OverlapCheck")
        {
            if (col.tag == "Block")
            {

                build_script.is_overlapping = true;
            }
        }

        if (this.gameObject.name == "ConnectionChecks")
        {
            if (col.tag == "Block")
            {
                build_script.is_connected = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (this.gameObject.name == "OverlapCheck")
        {
            if (col.tag == "Block")
            {
                build_script.is_overlapping = false;
            }
        }

        if (this.gameObject.name == "ConnectionChecks")
        {
            if (col.tag == "Block")
            {
                build_script.is_connected = false;
            }
        }
    }
}
