using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Cutscene_player : MonoBehaviour
{
    [SerializeField]
    bool debug_animation;

    [SerializeField]
    Animator animator_player;

    [SerializeField]
    Animation Animation_2;

    // Start is called before the first frame update
    void Start()
    {
        animator_player = this.GetComponent<Animator>();

        Animation_2 = this.GetComponent<Animation>();


        // Animation_2.playAutomatically = true;

        Animation_2.PlayQueued("Player_demo", QueueMode.CompleteOthers);
        Animation_2.PlayQueued("Player_rotate_left", QueueMode.CompleteOthers);


        animator_player.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        show_working_animator();
    }

    private void show_working_animator()
    {
        if (debug_animation == true)
            Debug.Log("use show_working_animator()");

        if (animator_player.enabled == false)
        {
           

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (debug_animation == true)
                Debug.Log("animator_player.enabled = true");

              //  _animation.Play("Player_demo");

                // animator_player.enabled = true;

                //
                //animator_player.Play("player_demo");

                // 
                //Queues each of these animations to be played one after the other
                //  animator_player.PlayQueued("player_demo", QueueMode.CompleteOthers);
                // animator_player.playg.PlayQueued("player_rotate_left", QueueMode.CompleteOthers);

                // _animation.PlayQueued("player_demo", QueueMode.CompleteOthers);






            }
        }
    }
}
