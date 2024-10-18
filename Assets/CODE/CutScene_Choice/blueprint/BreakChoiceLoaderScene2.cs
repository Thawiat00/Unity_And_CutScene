using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreakChoiceLoaderScene2 : MonoBehaviour
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
            Debug.Log("Break choice scene 1_2 data loaded successfully.");
        }
        else
        {
            Debug.LogError("Failed to load break choice scene 1_2 data.");
        }
    }

    void ShowOptionButtons()
    {
        if (breakChoiceSceneData != null && breakChoiceSceneData.break_choice_scene_1_2.Count > 0)
        {
            currentOptionCall = breakChoiceSceneData.break_choice_scene_1_2[0].option_call_all[0];
            currentOptionAnswer = breakChoiceSceneData.break_choice_scene_1_2[0].option_call_all_answer[0];

            button1.GetComponentInChildren<Text>().text = currentOptionCall.option_call_option_1[0];
            button1.onClick.AddListener(() => HandleOptionSelected(1));
            button2.GetComponentInChildren<Text>().text = currentOptionCall.option_call_option_2[0];
            button2.onClick.AddListener(() => HandleOptionSelected(2));
            button3.GetComponentInChildren<Text>().text = currentOptionCall.option_call_option_3[0];
            button3.onClick.AddListener(() => HandleOptionSelected(3));
        }
        else
        {
            Debug.LogWarning("No break_choice_scene_1_2 data found in breakChoiceSceneData.");
        }
    }

    void HandleOptionSelected(int optionNumber)
    {
        selectedOption = optionNumber; // Store the selected option
        currentSentenceIndex = 1; // Reset to start from the second sentence
        showingAnswer = false; // Reset answer showing state
        showingOptionCalls = true; // Mark as showing option calls
        ShowSecondSentence(optionNumber);
        HideButtons(); // Hide buttons after showing the response
    }

    void ShowSecondSentence(int optionNumber)
    {
        string secondSentence = "";

        switch (optionNumber)
        {
            case 1:
                if (currentOptionCall.option_call_option_1.Count > 1)
                {
                    secondSentence = currentOptionCall.option_call_option_1[1]; // Show second sentence
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

        dialogText.text = secondSentence; // Display the second sentence
        Debug.Log($"Second sentence displayed: {secondSentence}");
        currentSentenceIndex = 2; // Set the index to the next one for the next click
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (showingOptionCalls)
            {
                ShowNextSentence(); // Show the next sentence in the current option call
            }
            else if (showingAnswer)
            {
                ShowAnswer(); // Show the next answer if it's not already shown
            }
        }
    }

    void ShowNextSentence()
    {
        string nextSentence = "";
        bool optionCallsFinished = false; // Track if we finished showing all option calls

        switch (selectedOption)
        {
            case 1:
                if (currentSentenceIndex < currentOptionCall.option_call_option_1.Count)
                {
                    nextSentence = currentOptionCall.option_call_option_1[currentSentenceIndex];
                }
                else
                {
                    optionCallsFinished = true; // Finished showing all option calls
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
            dialogText.text = nextSentence; // Show the next sentence
            currentSentenceIndex++; // Move to the next sentence index
            Debug.Log($"Next sentence displayed: {nextSentence}");
        }
        else if (optionCallsFinished) // If all option calls are done
        {
            showingOptionCalls = false; // Now switch to showing answers
            currentSentenceIndex = 0; // Reset index for answers
            ShowAnswer(); // Show the first answer
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
            dialogText.text = answer; // Show the answer as a separate sentence
            showingAnswer = true; // Mark as showing answer
            Debug.Log($"Response displayed: {answer}");
            currentAnswerIndex++; // Move to the next answer index
        }
        else
        {
            Debug.Log("No more answers to display.");
        }
    }

    void HideButtons()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        Debug.Log("Buttons are now hidden.");
    }
}
