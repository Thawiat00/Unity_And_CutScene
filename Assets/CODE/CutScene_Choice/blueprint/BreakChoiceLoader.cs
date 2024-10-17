using UnityEngine;
using System.Collections.Generic;


public class BreakChoiceLoader : MonoBehaviour
{
    private BreakChoiceData breakChoiceData;

    void Start()
    {
        LoadBreakChoiceScene();
    }

    void LoadBreakChoiceScene()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("mock_break_choice_scene");

        if (jsonFile != null)
        {
            breakChoiceData = JsonUtility.FromJson<BreakChoiceData>(jsonFile.text);
            Debug.Log("Data loaded successfully!");
            ShowOptionCallAll();
        }
        else
        {
            Debug.LogError("Failed to load JSON! Please check the file name and Resources folder.");
        }
    }

    void ShowOptionCallAll()
    {
        if (breakChoiceData.break_choice_scene_1_1.Count > 0)
        {
            var scene1 = breakChoiceData.break_choice_scene_1_1[0];
            foreach (var option in scene1.option_call_all)
            {
                Debug.Log("Option Call Option 1: " + string.Join(", ", option.option_call_option_1));
                Debug.Log("Option Call Option 2: " + string.Join(", ", option.option_call_option_2));
                Debug.Log("Option Call Option 3: " + string.Join(", ", option.option_call_option_3));
            }

            foreach (var answer in scene1.option_call_all_answer)
            {
                Debug.Log("Answer Option 1: " + string.Join(", ", answer.option_answer_option_1));
                Debug.Log("Answer Option 2: " + string.Join(", ", answer.option_answer_option_2));
                Debug.Log("Answer Option 3: " + string.Join(", ", answer.option_answer_option_3));
            }
        }
        else
        {
            Debug.LogWarning("No break_choice_scene_1_1 data found.");
        }
    }
}

