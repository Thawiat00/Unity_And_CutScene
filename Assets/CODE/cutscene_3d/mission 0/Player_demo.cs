using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player_demo : MonoBehaviour
{
    [SerializeField]
    bool debug_component_player;

    public enum status_character
    {
        disable,
        working
    }

    [SerializeField]
    status_character _status_Character;

    [SerializeField]
    CapsuleCollider capsule_player;

    [SerializeField]
    CharacterController character_player;

    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;


    void change_status_character_disable()
    {
        if (debug_component_player == true)
            Debug.Log("set  change_status_character_disable()");

        _status_Character = status_character.disable;

    }

    void change_status_character_working()
    {
        if (debug_component_player == true)
            Debug.Log("set  change_status_character_working()");


        _status_Character = status_character.working;

    }



    private void Start()
    {
        set_up_turn_off_component();

        change_status_character_disable();

       // change_status_character_working();
    }

    private void set_up_turn_off_component()
    {
        if (debug_component_player == true)
            Debug.Log("set  capsule_player , character_player");

        capsule_player = GetComponent<CapsuleCollider>();

        character_player = GetComponent<CharacterController>();

        capsule_player.enabled = false;

        character_player.enabled = false;


        if (debug_component_player == true)
            Debug.Log("turn off , capsule_player.enabled = false; , character_player.enabled = false; ");




    }

    void Update()
    {
        character_working();
    }

    void character_working()
    {
        if(_status_Character == status_character.working)
        {
           

            // Rotate around y - axis
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            // Move forward / backward
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = speed * Input.GetAxis("Vertical");
            character_player.SimpleMove(forward * curSpeed);


        }
    }
}
