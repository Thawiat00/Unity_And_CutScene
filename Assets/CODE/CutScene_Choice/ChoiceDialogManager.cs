using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ChoiceDialogManager : MonoBehaviour
{
    [SerializeField] private RawImage videoRawImage;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private Button[] optionButtons;
    private VideoPlayer videoPlayer;
    private List<IChoiceDialogState> states = new List<IChoiceDialogState>();
    private int currentStateIndex = -1;
    private IChoiceDialogState currentState;
    private ChoiceDialogContainer dialogContainer;

    void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        LoadDialogFromJson();
        InitializeUI();
        NextState();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    private void LoadDialogFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("dialog_cutscene_choice");
        if (jsonFile != null)
        {
            dialogContainer = JsonConvert.DeserializeObject<ChoiceDialogContainer>(jsonFile.text);
            CreateDialogStates();
        }
        else
        {
            Debug.LogError("JSON file not found!");
        }
    }

    private void CreateDialogStates()
    {
        foreach (var dialog in dialogContainer.dialog_cs_OC)
        {
            if (!string.IsNullOrEmpty(dialog.girl))
            {
                states.Add(new ChoiceGirlDialogState(this, dialog.girl));
            }
            if (!string.IsNullOrEmpty(dialog.boy))
            {
                states.Add(new ChoiceBoyDialogState(this, dialog.boy));
            }
            if (dialog.cutscene_choice != null && dialog.cutscene_choice.Count > 0)
            {
                foreach (var choiceKey in dialog.cutscene_choice)
                {
                    states.Add(new ChoiceSelectionState(this, choiceKey, dialogContainer.break_choice_scenes));
                }
            }
        }
    }

    private void InitializeUI()
    {
        // Initialize UI elements (dialogText, optionButtons, etc.)
    }

    public void NextState()
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentStateIndex++;
        if (currentStateIndex >= states.Count)
        {
            Debug.Log("Dialog ended");
            return;
        }

        currentState = states[currentStateIndex];
        currentState.Enter();
    }

    public void SetDialogText(string text)
    {
        if (dialogText != null)
        {
            dialogText.text = text;
        }
    }

    public void ShowOptions(List<string> options)
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < options.Count)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<Text>().text = options[i];
                int optionIndex = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnOptionSelected(optionIndex));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnOptionSelected(int optionIndex)
    {
        if (currentState is ChoiceSelectionState choiceState)
        {
            choiceState.SelectOption(optionIndex);
        }
    }

    public RawImage GetRawImage()
    {
        return videoRawImage;
    }
}