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
        if (breakChoiceSceneData != null && breakChoiceSceneData.break_choice_scene_1_1.Count > 0)
        {
            currentOptionCall = breakChoiceSceneData.break_choice_scene_1_1[0].option_call_all[0];

            if (currentOptionCall.option_call_option_1.Count > 0)
            {
                button1.GetComponentInChildren<Text>().text = currentOptionCall.option_call_option_1[0];
                button1.onClick.AddListener(() => HandleOptionSelected(1));
            }

            if (currentOptionCall.option_call_option_2.Count > 0)
            {
                button2.GetComponentInChildren<Text>().text = currentOptionCall.option_call_option_2[0];
                button2.onClick.AddListener(() => HandleOptionSelected(2));
            }

            if (currentOptionCall.option_call_option_3.Count > 0)
            {
                button3.GetComponentInChildren<Text>().text = currentOptionCall.option_call_option_3[0];
                button3.onClick.AddListener(() => HandleOptionSelected(3));
            }
        }
    }

    void HandleOptionSelected(int optionNumber)
    {
        // ?????????????????????????????
        HideButtons();

        // ????????????? 2 ??????????????????????
        ShowSecondSentence(optionNumber);
    }

    void ShowSecondSentence(int optionNumber)
    {
        string secondSentence = "";

        switch (optionNumber)
        {
            case 1:
                if (currentOptionCall.option_call_option_1.Count > 1)
                {
                    secondSentence = currentOptionCall.option_call_option_1[1]; // ????????????? 2
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

        dialogText.text = secondSentence; // ??????????????? dialogText
        Debug.Log($"Second sentence displayed: {secondSentence}");
    }

    // ??????????????
    void HideButtons()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        Debug.Log("Buttons are now hidden.");
    }
}
