using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Cutscene_player : MonoBehaviour
{
    [SerializeField]
    Animator animator_player;

    // Start is called before the first frame update
    void Start()
    {
        animator_player = this.GetComponent<Animator>();
        animator_player.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       // show_working_animator();
    }

    private void show_working_animator()
    {
        if(animator_player.enabled == false)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                animator_player.enabled = true;




            }
        }
    }
}
