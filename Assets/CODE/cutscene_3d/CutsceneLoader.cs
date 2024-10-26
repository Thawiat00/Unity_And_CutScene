using System.Collections.Generic;
using UnityEngine;

public class CutsceneLoader : MonoBehaviour
{
    [SerializeField]
    bool debug_data_cutscene_json;

    public TextAsset jsonFile;
    public Root_cutscene_3d cutsceneData;

    [SerializeField]
   public List<string> make_copy_key;


    private ICountChecker countChecker;

    void Start()
    {
        LoadCutsceneData();
    }




    public void SetCountChecker(ICountChecker checker)
    {
        countChecker = checker; // create class target to use interface
    }


    public void make_copy_list_animation_key(List<string> makecopy)
    {
        makecopy = make_copy_key;
    }

    void LoadCutsceneData()
    {
        if (jsonFile != null)
        {
            // ?????????? JSON ??? TextAsset ??????????????? C#
            cutsceneData = JsonUtility.FromJson<Root_cutscene_3d>(jsonFile.text);

            // ??????????????????????
            if(debug_data_cutscene_json == true)
            Debug.Log("Number of scenes: " + cutsceneData.scene.Length);


            foreach (var scene in cutsceneData.scene)
            {
                make_copy_key.Add(scene.key);

                if (debug_data_cutscene_json == true)
                    Debug.Log($"Scene ID: {scene.id}, Key: {scene.key}");
            }


            if (make_copy_key.Count > 0) // create check condition how to want
            {
                if (debug_data_cutscene_json == true)
                    Debug.Log("make call OnCountReached() on interface ");

                countChecker?.OnCountReached(); // call method in class target (OnCountReached() is class in interface)
            }

            // ??????? combo acting
            //  Debug.Log("Number of combo acting: " + cutsceneData.combo_acting.Length);

            /*
            foreach (var combo in cutsceneData.combo_acting)
            {
                Debug.Log($"Combo Key: {combo.key}, Data Acting Count: {combo.data_acting.Length}");
                foreach (var acting in combo.data_acting)
                {
                    if (acting.camera != null)
                    {
                        Debug.Log($"Camera Position: {acting.camera.position}, Action: {acting.camera.action}");
                    }
                    if (acting.player != null)
                    {
                        Debug.Log($"Player Action: {acting.player.action}, Distance: {acting.player.distance}");
                    }
                    if (acting.boss != null)
                    {
                        Debug.Log($"Boss Action: {acting.boss.action}, Distance: {acting.boss.distance}");
                    }
                    if (acting.ui != null)
                    {
                        Debug.Log($"UI Display: {acting.ui.display}");
                    }
                }
            }

            // ??????? boss data
            Debug.Log($"Boss ID: {cutsceneData.boss.id}, Name: {cutsceneData.boss.name}");
            foreach (var action in cutsceneData.boss.actions)
            {
                Debug.Log($"Boss Action: {action.action}, Distance: {action.distance}");
            }

            // ??????? player data
            Debug.Log($"Player ID: {cutsceneData.player.id}");
            foreach (var action in cutsceneData.player.actions)
            {
                Debug.Log($"Player Action: {action.action}, Distance: {action.distance}");
            }

            */
        }
        else
        {
            if (debug_data_cutscene_json == true)
                Debug.LogError("No JSON file found!");
        }
    }
}
public interface ICountChecker
{
    void OnCountReached(); // method will call if count is max 
}