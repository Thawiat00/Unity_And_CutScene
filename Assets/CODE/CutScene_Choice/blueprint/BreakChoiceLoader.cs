using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakChoiceLoader : MonoBehaviour
{
    [System.Serializable]
    public class OptionCall
    {
        public List<string> option_call_option_1;
        public List<string> option_call_option_2;
        public List<string> option_call_option_3;
    }

    [System.Serializable]
    public class BreakChoiceScene
    {
        public List<OptionCall> option_call_all;
    }

    [System.Serializable]
    public class BreakChoiceData
    {
        public List<BreakChoiceScene> break_choice_scene_1_1;
    }

    private BreakChoiceData breakChoiceSceneData;

    public Button button1; // Drag your Button 1 here in the inspector
    public Button button2; // Drag your Button 2 here in the inspector
    public Button button3; // Drag your Button 3 here in the inspector

    void Start()
    {
        LoadBreakChoiceScene();
        ShowOptionButtons();
    }

    void LoadBreakChoiceScene()
    {
        // Load your JSON data from the Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>("mock_break_choice_scene");
        if (jsonFile != null)
        {
            breakChoiceSceneData = JsonUtility.FromJson<BreakChoiceData>(jsonFile.text);
            Debug.Log("Break choice scene data loaded successfully.");
        }
        else
        {
            Debug.LogError("Failed to load break choice scene data.");
        }
    }

    void ShowOptionButtons()
    {
        if (breakChoiceSceneData != null && breakChoiceSceneData.break_choice_scene_1_1.Count > 0)
        {
            var optionCall = breakChoiceSceneData.break_choice_scene_1_1[0].option_call_all[0]; // Get the first set of options

            // Check if options are available and assign them to buttons
            if (optionCall.option_call_option_1.Count > 0)
                button1.GetComponentInChildren<Text>().text = optionCall.option_call_option_1[0];
            else
                Debug.LogWarning("No option_call_option_1 data found.");

            if (optionCall.option_call_option_2.Count > 0)
                button2.GetComponentInChildren<Text>().text = optionCall.option_call_option_2[0];
            else
                Debug.LogWarning("No option_call_option_2 data found.");

            if (optionCall.option_call_option_3.Count > 0)
                button3.GetComponentInChildren<Text>().text = optionCall.option_call_option_3[0];
            else
                Debug.LogWarning("No option_call_option_3 data found.");
        }
        else
        {
            Debug.LogWarning("No break_choice_scene_1_1 data found in breakChoiceSceneData.");
        }
    }
}
