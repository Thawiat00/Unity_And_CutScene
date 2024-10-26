using System.Collections.Generic;
using UnityEngine;

public class CutsceneController_3d_cutscene : MonoBehaviour, ICountChecker
{


    [SerializeField]
    bool debug_playcutscene;

    [SerializeField]
    CutsceneLoader cutsceneLoader;

    [SerializeField]
    ICountChecker_cutscene_player _countChecker_Cutscene_Player;


    // ?????? delegate type
    public delegate void CountReadyHandler(List<string> list);

    // ?????? event
    public static event CountReadyHandler OnCountReady;

    //  [SerializeField]
    //   List<string> keep_list_key_animation;

    private void Start()
    {
        //load data json
        //load json key
        //create variable list
        //keep list to save animation list
        //


        Get_component();

        OnCountReached();

        OnCountReady?.Invoke(cutsceneLoader.make_copy_key);

        //   loop_list_string();

        // PlayCutscene("walking player pose 1");
    }




    public void OnCountReached()
    {
        if (debug_playcutscene == true)
            Debug.Log("Count reached the required number! use loop_list_string() method for load list ");

        loop_list_string();
        // ?????????????????????????????? count ???
    }

    // make   void OnCount_Ready(List<string> list_string); work with list cutscene_player and list CutsceneController_3d_cutscene 
    //

    void Get_component()
    {
        if (debug_playcutscene == true)
            Debug.Log("debug Get_component() ");


        cutsceneLoader = GetComponent<CutsceneLoader>();
    }

    void loop_list_string()
    {
        if (cutsceneLoader.make_copy_key.Count != 0)
        {
            if (debug_playcutscene == true)
                Debug.Log("cutsceneLoader.make_copy_key have count");

            foreach (var scene in cutsceneLoader.cutsceneData.scene)
            {
                //  make_copy_key.Add(scene.key);

                if (debug_playcutscene == true)
                    Debug.Log($"Scene ID: {scene.id}, Key: {scene.key}");

                PlayCutscene(scene.key);
              // PlayCutscene()
            }
            
            //_countChecker_Cutscene_Player.OnCount_Ready(cutsceneLoader.make_copy_key);
            //add for cutscene
        }
        else
        {
            if (debug_playcutscene == true)
                Debug.Log("cutsceneLoader.make_copy_key  count = 0");

        }


        /*
        foreach (string key in cutsceneLoader.make_copy_key)
        {
            Debug.Log("loop_list_string():" + key);
        }
        */

        //cutsceneLoader.make_copy_key
    }




    public void PlayCutscene(string key)
    {
        switch (key)
        {
            case "walking player pose 1":
               // CameraFollowPlayer();
                PlayerWalk();
                
                break;

            case "Rotate left":
                Rotate_left();
                break;
                // ????????? ???????? key ?? JSON
        }
    }


    private void Rotate_left()
    {
        if (debug_playcutscene == true)
        {
            //   Debug.Log("PlayerWalk()");

            Debug.Log("PlayClip_Rotate_left() ");
        }

    }

    private void PlayerWalk()
    {
        if (debug_playcutscene == true)
        {
         //   Debug.Log("PlayerWalk()");

            Debug.Log("PlayClip_walk_pose1  ( " );
        }




        // ??????????????????????????
    }

    private void CameraFollowPlayer()
    {
        if (debug_playcutscene == true)
        {
            Debug.Log("CameraFollowPlayer()");

          //  Debug.Log("SetCamera view thrid person");
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

public interface ICountChecker_cutscene_player
{
   // void OnCount_Ready(List<string> get_count_ready); // method will call if count is max 
    void OnCount_Ready(List<string> list_string);
}