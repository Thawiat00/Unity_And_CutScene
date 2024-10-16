using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ?????? DialogManager ??????????????
public class DialogManager_Choice : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button[] choiceButtons;
    [SerializeField] private TextMeshProUGUI[] choiceTexts;
    [SerializeField] private GameObject continueIcon;

    [Header("Dialog Settings")]
    [SerializeField] private TextAsset jsonFile;
    [SerializeField] private float textSpeed = 0.05f;

    private DialogData_Choice dialogData;
    private int currentDialogIndex = 0;
    private bool isTyping = false;
    private DialogScene_Choice currentScene;
    private Coroutine typingCoroutine;

    void Start()
    {
        try
        {
            // Add debug logging
            string jsonText = jsonFile.text;
            JsonHelper_Choice.DebugLogJson(jsonText);

            // Parse JSON
            dialogData = JsonUtility.FromJson<DialogData_Choice>(jsonText);

            if (dialogData == null)
            {
                Debug.LogError("Failed to parse JSON: result is null");
                return;
            }

            // Initialize UI
            choicePanel.SetActive(false);
            continueIcon.SetActive(false);
            StartDialog();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in Start: {e.Message}\n{e.StackTrace}");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacebarPressed();
        }
    }

    void OnSpacebarPressed()
    {
        if (choicePanel.activeSelf)
            return;

        if (isTyping)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);
            CompleteCurrentDialog();
        }
        else
        {
            currentDialogIndex++;
            ShowCurrentDialog();
        }
    }

    void StartDialog()
    {
        if (dialogData.dialog_cs_OC != null && dialogData.dialog_cs_OC.Count > 0)
        {
            currentScene = dialogData.dialog_cs_OC[0];
            ShowCurrentDialog();
        }
    }

    void ShowCurrentDialog()
    {
        if (currentDialogIndex >= 4)
        {
            ShowChoices();
            return;
        }

        string currentText = "";
        string characterName = "";

        if (currentDialogIndex % 2 == 0)
        {
            currentText = currentScene.girl;
            characterName = "Girl";
        }
        else
        {
            currentText = currentScene.boy;
            characterName = "Boy";
        }

        characterNameText.text = characterName;
        continueIcon.SetActive(false);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(currentText));
    }

    void CompleteCurrentDialog()
    {
        isTyping = false;
        if (currentDialogIndex % 2 == 0)
        {
            dialogText.text = currentScene.girl;
        }
        else
        {
            dialogText.text = currentScene.boy;
        }
        continueIcon.SetActive(true);
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char c in text.ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        continueIcon.SetActive(true);
    }

    void ShowChoices()
    {
        continueIcon.SetActive(false);
        choicePanel.SetActive(true);

        var choices = dialogData.break_choice_scene_1_1[0].option_call_all[0];

        SetupChoice(0, choices.option_call_option_1[0], dialogData.break_choice_scene_1_1[0].option_call_all_answer[0].option_answer_option_1);
        SetupChoice(1, choices.option_call_option_2[0], dialogData.break_choice_scene_1_1[0].option_call_all_answer[0].option_answer_option_2);
        SetupChoice(2, choices.option_call_option_3[0], dialogData.break_choice_scene_1_1[0].option_call_all_answer[0].option_answer_option_3);
    }

    void SetupChoice(int index, string choiceText, List<string> answers)
    {
        choiceTexts[index].text = choiceText;
        choiceButtons[index].onClick.RemoveAllListeners();
        choiceButtons[index].onClick.AddListener(() => OnChoiceSelected(answers));
    }

    void OnChoiceSelected(List<string> answers)
    {
        choicePanel.SetActive(false);
        characterNameText.text = "Response";
        StartCoroutine(TypeText(answers[0]));
    }
}