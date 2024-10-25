using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CutsceneController_3d_cutscene : MonoBehaviour
{
    [SerializeField]
    bool debug_playcutscene;

    private void Start()
    {
        PlayCutscene("walking player pose 1");
    }

    public void PlayCutscene(string key)
    {
        switch (key)
        {
            case "walking player pose 1":
                CameraFollowPlayer();
                PlayerWalk();
              
                break;
            case "camera zoom in":
                CameraZoomIn();
                break;
                // ????????? ???????? key ?? JSON
        }
    }

    private void PlayerWalk()
    {
        if(debug_playcutscene == true)
        {
            Debug.Log("PlayerWalk()");

            Debug.Log("PlayClip_walk_pose1");
        }




        // ??????????????????????????
    }

    private void CameraFollowPlayer()
    {
        if (debug_playcutscene == true)
        {
            Debug.Log("CameraFollowPlayer()");

            Debug.Log("SetCamera view thrid person");
        }

        // ??????????????????????????????
    }

    private void CameraZoomIn()
    {
        if (debug_playcutscene == true)
        {
            Debug.Log("CameraZoomIn()");
        }

        // ?????????????????
    }

    // Method ?????
}

