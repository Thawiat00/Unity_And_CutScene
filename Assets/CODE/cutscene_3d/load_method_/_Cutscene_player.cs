using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class _Cutscene_player : MonoBehaviour
{
    [SerializeField]
    bool debug_animation;

    [SerializeField]
    Animator animator_player;

    [SerializeField]
    Animation _animation;

   // public List<string> keyList; // ?????? key ??????????????????????????

 //   public List<AnimationClip> allAnimationClips; // ?????????????


    public List<AnimationClip> _animationClips; //all clip here

    public List<AnimationClip> sortedClipsByKeys; // sort in key



    void OnEnable()
    {
        // Subscribe to event
        //cu.OnCountReady += HandleCountReady;
        //make for add order in datamakecopy

        CutsceneController_3d_cutscene.OnCountReady += play_quened_animation;
      
    }

    void OnDisable()
    {
        // Unsubscribe from event
        // SenderWithEvent.OnCountReady -= HandleCountReady;
        //make for add order in datamakecopy when disable is remove

        CutsceneController_3d_cutscene.OnCountReady -= play_quened_animation;
    }





    // [SerializeField]
    // AnimationClip _animationClip;

    // Start is called before the first frame update
    void Start()
    {
        animator_player = this.GetComponent<Animator>();

        _animation = this.GetComponent<Animation>();
        // Animation_2 = this.GetComponent<Animation>();

        //find list to add animation
        //load all clip with list and folder
        AnimationClip[] loadedClips = Resources.LoadAll<AnimationClip>("Animation_clip");
        _animationClips = new List<AnimationClip>(loadedClips);

        //load with list animation
  


        //  Animation_2.PlayQueued("Player_demo", QueueMode.CompleteOthers);
        //   Animation_2.PlayQueued("Player_rotate_left", QueueMode.CompleteOthers);


        animator_player.enabled = false;
    }


   public void play_quened_animation(List<string> list_key)
    {
        // ???????? allAnimationClips ??? list_key
        if (debug_animation == true)
            Debug.Log("use play_quened_animation(List<string> list_key)");

        sortedClipsByKeys = list_key
            .Select(key => _animationClips.FirstOrDefault(clip => clip.name == key))
            .Where(clip => clip != null) // ??????????????????? allAnimationClips
            .ToList();

        if(sortedClipsByKeys.Count >0)
        {
            if (debug_animation == true)
                Debug.Log("sortedClipsByKeys.Count >0");

            int i = 0;

            foreach (var clip in sortedClipsByKeys)
            {
                Debug.Log(clip.name);
                string safeclip = clip.name;

                //add clip animation
                _animation.AddClip(sortedClipsByKeys[i],safeclip);

                //create queued and play animation start to end and then play another animation later (1 -> 2 ->3)
                _animation.PlayQueued(safeclip, QueueMode.CompleteOthers);
                // _animation.AddClip(clip.Value.name).ToSafeString;
                // _animation.Add(clip.Value.name);

                i++;
            }
            //_animation.
        }


        // ????????????????????????????????????
       // StartCoroutine(PlayAnimationQueue(sortedClipsByKeys, key_clip));

    }

    /*
    private IEnumerator PlayAnimationQueue(List<AnimationClip> sortedClips, string key_clip)
    {
        foreach (var clip in sortedClips)
        {
            animator.Play(clip.name); // ????????????????????????????????
            yield return new WaitForSeconds(clip.length); // ?????????????????????????

            // ?????????????????????????????? key_clip
            if (clip.name == key_clip)
            {
                Debug.Log("Stopped at key_clip: " + key_clip);
                break;
            }
        }
    }
    */



    // Update is called once per frame
    void Update()
    {
      //  show_working_animator();
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
