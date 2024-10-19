using UnityEngine;

public class BreakChoiceLoader_dynamic : MonoBehaviour
{
    public BreakChoiceData_dynamic breakChoiceData; // ????????????????

    void Start()
    {
        LoadBreakChoiceScene();
    }

    void LoadBreakChoiceScene()
    {
        // ???????? JSON ??? Resources
        TextAsset jsonFile = Resources.Load<TextAsset>("mock_break_choice_scene");

        if (jsonFile != null)
        {
            string json = jsonFile.text;
            Debug.Log("Loaded JSON: " + json);  // ?????????? JSON ???????????

            // Deserialize JSON ???? BreakChoiceData_dynamic
            breakChoiceData = JsonUtility.FromJson<BreakChoiceData_dynamic>(json);

            if (breakChoiceData != null && breakChoiceData.break_choice_scene != null)
            {
                Debug.Log("Break choice scenes loaded successfully.");

                // ??????? break_choice_scene
                foreach (var scene in breakChoiceData.break_choice_scene)
                {
                    Debug.Log($"Scene Key: {scene.key}");

                    // ????????????? sceneData
                    foreach (var sceneData in scene.sceneData)
                    {
                        Debug.Log($"Option 1: {string.Join(", ", sceneData.option_call_all[0].option_call_option_1)}");
                    }
                }
            }
            else
            {
                Debug.LogError("No break choice scenes available in data.");
            }
        }
        else
        {
            Debug.LogError("Cannot find the JSON file in Resources.");
        }
    }
}
