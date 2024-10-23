using UnityEngine;

public class CutsceneLoader : MonoBehaviour
{
    public TextAsset jsonFile;
    public Root_cutscene_3d cutsceneData;

    void Start()
    {
        LoadCutsceneData();
    }

    void LoadCutsceneData()
    {
        if (jsonFile != null)
        {
            // ?????????? JSON ??? TextAsset ??????????????? C#
            cutsceneData = JsonUtility.FromJson<Root_cutscene_3d>(jsonFile.text);

            // ??????????????????????
            Debug.Log("Number of scenes: " + cutsceneData.scene.Length);
            foreach (var scene in cutsceneData.scene)
            {
                Debug.Log($"Scene ID: {scene.id}, Key: {scene.key}");
            }

            // ??????? combo acting
            Debug.Log("Number of combo acting: " + cutsceneData.combo_acting.Length);
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
        }
        else
        {
            Debug.LogError("No JSON file found!");
        }
    }
}
