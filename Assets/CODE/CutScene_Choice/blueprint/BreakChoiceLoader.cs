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
    }

    private BreakChoiceData breakChoiceSceneData;

    public TextMeshProUGUI dialogText;
    public Button button1;
    public Button button2;
    public Button button3;

    private OptionCall currentOptionCall;
    private int currentOptionIndex; // ??????????????????????????????????
    private int currentSentenceIndex; // ???????????????

    void Start()
    {
        LoadBreakChoiceScene();
        ShowOptionButtons();
    }

    void Update()
    {
        // ???????????? Spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextSentence(); // ???????????????????????????????
        }
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
        if (breakChoiceSceneData != null && breakChoiceSceneData.break_choice_scene_1_1.Count > 0)
        {
            currentOptionCall = breakChoiceSceneData.break_choice_scene_1_1[0].option_call_all[0];

            // ???????????
            SetupButton(button1, currentOptionCall.option_call_option_1, 0, 1);
            SetupButton(button2, currentOptionCall.option_call_option_2, 1, 2);
            SetupButton(button3, currentOptionCall.option_call_option_3, 2, 3);
        }
        else
        {
            Debug.LogWarning("No break_choice_scene_1_1 data found in breakChoiceSceneData.");
        }
    }

    void SetupButton(Button button, List<string> options, int optionIndex, int buttonIndex)
    {
        if (options.Count > 0)
        {
            button.GetComponentInChildren<Text>().text = options[0];
            button.onClick.AddListener(() => HandleOptionSelected(optionIndex));
        }
        else
        {
            Debug.LogWarning($"No option_call_option_{buttonIndex} data found.");
        }
    }

    void HandleOptionSelected(int optionIndex)
    {
        currentOptionIndex = optionIndex; // ????????????????????????????
        currentSentenceIndex = 1; // ???????????????????????????? 2
        ShowNextSentence();
        HideButtons(); // ?????????????????????????
    }

    void ShowNextSentence()
    {
        string nextSentence = "";

        // ??????????????????????????????????????
        switch (currentOptionIndex)
        {
            case 0:
                if (currentSentenceIndex < currentOptionCall.option_call_option_1.Count)
                {
                    nextSentence = currentOptionCall.option_call_option_1[currentSentenceIndex];
                    currentSentenceIndex++;
                }
                break;
            case 1:
                if (currentSentenceIndex < currentOptionCall.option_call_option_2.Count)
                {
                    nextSentence = currentOptionCall.option_call_option_2[currentSentenceIndex];
                    currentSentenceIndex++;
                }
                break;
            case 2:
                if (currentSentenceIndex < currentOptionCall.option_call_option_3.Count)
                {
                    nextSentence = currentOptionCall.option_call_option_3[currentSentenceIndex];
                    currentSentenceIndex++;
                }
                break;
        }

        // ????????????? dialogText
        dialogText.text = nextSentence;
        Debug.Log($"Displayed sentence: {nextSentence}");
    }

    // ????????????????????????????
    void HideButtons()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        Debug.Log("Buttons are now hidden.");
    }
}
