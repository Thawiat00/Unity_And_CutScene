using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public class OptionAnswer
    {
        public List<string> option_answer_option_1;
        public List<string> option_answer_option_2;
        public List<string> option_answer_option_3;
    }

    [System.Serializable]
    public class BreakChoiceScene
    {
        public List<OptionCall> option_call_all;
        public List<OptionAnswer> option_call_all_answer; // Added to hold answers
    }

    [System.Serializable]
    public class BreakChoiceData
    {
        public List<BreakChoiceScene> break_choice_scene_1_1;
    }

    private BreakChoiceData breakChoiceSceneData;

    public TextMeshProUGUI dialogText;
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
            var optionAnswers = breakChoiceSceneData.break_choice_scene_1_1[0].option_call_all_answer[0]; // Get the first set of answers

            // Set button texts and add listeners
            SetButton(button1, optionCall.option_call_option_1, optionAnswers.option_answer_option_1);
            SetButton(button2, optionCall.option_call_option_2, optionAnswers.option_answer_option_2);
            SetButton(button3, optionCall.option_call_option_3, optionAnswers.option_answer_option_3);
        }
        else
        {
            Debug.LogWarning("No break_choice_scene_1_1 data found in breakChoiceSceneData.");
        }
    }

    void SetButton(Button button, List<string> optionCalls, List<string> optionAnswers)
    {
        if (optionCalls.Count > 0 && optionAnswers.Count > 0)
        {
            button.GetComponentInChildren<Text>().text = optionCalls[0]; // Show the first option
            button.onClick.RemoveAllListeners(); // Remove existing listeners to avoid duplicates
            button.onClick.AddListener(() => ShowResponse(optionAnswers[0])); // Set response
        }
        else
        {
            Debug.LogWarning($"No data found for button: {button.name}.");
        }
    }

    void ShowResponse(string answer)
    {
        dialogText.text = answer; // Show the selected answer in the dialog text
        Debug.Log($"Response displayed: {dialogText.text}");
    }
}
