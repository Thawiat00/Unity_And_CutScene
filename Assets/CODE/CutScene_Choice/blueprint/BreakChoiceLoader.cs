using System.Collections;
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
        public List<OptionAnswer> option_call_all_answer;
    }

    [System.Serializable]
    public class BreakChoiceData
    {
        public List<BreakChoiceScene> break_choice_scene_1_1;
        public List<BreakChoiceScene> break_choice_scene_1_2;
    }

    private BreakChoiceData breakChoiceSceneData;

    public TextMeshProUGUI dialogText;
    public Button button1;
    public Button button2;
    public Button button3;

    private OptionCall currentOptionCall;
    private OptionAnswer currentOptionAnswer;

    private int currentSentenceIndex = 1; // Start at the second sentence
    private int currentAnswerIndex = 0; // Track the current answer index
    private bool showingAnswer = false; // Track whether we are showing answers
    private bool showingOptionCalls = false; // Track whether we are showing option calls
    private int selectedOption = 0; // Track the selected option

    private int currentScene = 1; // Track the current scene (1 or 2)

    void Start()
    {
        LoadBreakChoiceScene();
        ShowOptionButtons();
    }

    void LoadBreakChoiceScene()
    {
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
        if (currentScene == 1 && breakChoiceSceneData != null && breakChoiceSceneData.break_choice_scene_1_1.Count > 0)
        {
            currentOptionCall = breakChoiceSceneData.break_choice_scene_1_1[0].option_call_all[0];
            currentOptionAnswer = breakChoiceSceneData.break_choice_scene_1_1[0].option_call_all_answer[0];
        }
        else if (currentScene == 2 && breakChoiceSceneData != null && breakChoiceSceneData.break_choice_scene_1_2.Count > 0)
        {
            currentOptionCall = breakChoiceSceneData.break_choice_scene_1_2[0].option_call_all[0];
            currentOptionAnswer = breakChoiceSceneData.break_choice_scene_1_2[0].option_call_all_answer[0];
        }
        else
        {
            Debug.LogWarning("No break choice scene data found.");
            return;
        }

        // Show buttons again and set text
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(true);

        button1.GetComponentInChildren<Text>().text = currentOptionCall.option_call_option_1[0];
        button1.onClick.RemoveAllListeners(); // Ensure old listeners are cleared
        button1.onClick.AddListener(() => HandleOptionSelected(1));

        button2.GetComponentInChildren<Text>().text = currentOptionCall.option_call_option_2[0];
        button2.onClick.RemoveAllListeners();
        button2.onClick.AddListener(() => HandleOptionSelected(2));

        button3.GetComponentInChildren<Text>().text = currentOptionCall.option_call_option_3[0];
        button3.onClick.RemoveAllListeners();
        button3.onClick.AddListener(() => HandleOptionSelected(3));
    }

    void HandleOptionSelected(int optionNumber)
    {
        selectedOption = optionNumber;
        currentSentenceIndex = 1;
        showingAnswer = false;
        showingOptionCalls = true;
        ShowSecondSentence(optionNumber);
        HideButtons();
    }

    void ShowSecondSentence(int optionNumber)
    {
        string secondSentence = "";

        switch (optionNumber)
        {
            case 1:
                if (currentOptionCall.option_call_option_1.Count > 1)
                {
                    secondSentence = currentOptionCall.option_call_option_1[1];
                }
                break;
            case 2:
                if (currentOptionCall.option_call_option_2.Count > 1)
                {
                    secondSentence = currentOptionCall.option_call_option_2[1];
                }
                break;
            case 3:
                if (currentOptionCall.option_call_option_3.Count > 1)
                {
                    secondSentence = currentOptionCall.option_call_option_3[1];
                }
                break;
        }

        dialogText.text = secondSentence;
        Debug.Log($"Second sentence displayed: {secondSentence}");
        currentSentenceIndex = 2;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (showingOptionCalls)
            {
                ShowNextSentence();
            }
            else if (showingAnswer)
            {
                ShowAnswer();
            }
        }
    }

    void ShowNextSentence()
    {
        string nextSentence = "";
        bool optionCallsFinished = false;

        switch (selectedOption)
        {
            case 1:
                if (currentSentenceIndex < currentOptionCall.option_call_option_1.Count)
                {
                    nextSentence = currentOptionCall.option_call_option_1[currentSentenceIndex];
                }
                else
                {
                    optionCallsFinished = true;
                }
                break;
            case 2:
                if (currentSentenceIndex < currentOptionCall.option_call_option_2.Count)
                {
                    nextSentence = currentOptionCall.option_call_option_2[currentSentenceIndex];
                }
                else
                {
                    optionCallsFinished = true;
                }
                break;
            case 3:
                if (currentSentenceIndex < currentOptionCall.option_call_option_3.Count)
                {
                    nextSentence = currentOptionCall.option_call_option_3[currentSentenceIndex];
                }
                else
                {
                    optionCallsFinished = true;
                }
                break;
        }

        if (!string.IsNullOrEmpty(nextSentence))
        {
            dialogText.text = nextSentence;
            currentSentenceIndex++;
            Debug.Log($"Next sentence displayed: {nextSentence}");
        }
        else if (optionCallsFinished)
        {
            showingOptionCalls = false;
            currentSentenceIndex = 0;
            ShowAnswer();
        }
        else
        {
            Debug.Log("No more sentences to display.");
        }
    }

    void ShowAnswer()
    {
        string answer = "";

        switch (selectedOption)
        {
            case 1:
                if (currentAnswerIndex < currentOptionAnswer.option_answer_option_1.Count)
                {
                    answer = currentOptionAnswer.option_answer_option_1[currentAnswerIndex];
                }
                break;
            case 2:
                if (currentAnswerIndex < currentOptionAnswer.option_answer_option_2.Count)
                {
                    answer = currentOptionAnswer.option_answer_option_2[currentAnswerIndex];
                }
                break;
            case 3:
                if (currentAnswerIndex < currentOptionAnswer.option_answer_option_3.Count)
                {
                    answer = currentOptionAnswer.option_answer_option_3[currentAnswerIndex];
                }
                break;
        }

        if (!string.IsNullOrEmpty(answer))
        {
            dialogText.text = answer;
            showingAnswer = true;
            Debug.Log($"Response displayed: {answer}");
            currentAnswerIndex++;
        }
        else
        {
            Debug.Log("No more answers to display.");
            // Check if it's the end of the current scene, if so switch to the next scene
            if (currentScene == 1)
            {
                SwitchToNextScene();
            }
        }
    }

    void SwitchToNextScene()
    {
        Debug.Log("Switching to the next scene...");
        currentScene = 2;  // Move to scene 2
        currentSentenceIndex = 1;
        currentAnswerIndex = 0;
        selectedOption = 0;
        showingAnswer = false;
        showingOptionCalls = false;

        ShowOptionButtons(); // Show buttons for the next scene
        Debug.Log($"Switched to scene {currentScene}");
    }

    void HideButtons()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        Debug.Log("Buttons are now hidden.");
    }
}
