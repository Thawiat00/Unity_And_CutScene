using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

// Dialog Manager
public class DialogManager : MonoBehaviour
{
    [SerializeField] private RawImage videoRawImage; // ???????? RawImage ?????? UI
    private VideoPlayer videoPlayer;
    private RenderTexture renderTexture;

    [SerializeField] private TextMeshProUGUI dialogText; // ????????????????????
    private List<IDialogState> states = new List<IDialogState>();
    private int currentStateIndex = -1;
    private IDialogState currentState;

    void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>(); // ????? VideoPlayer
        LoadDialogFromJson(); // ?????????????????? JSON

        // ????? RenderTexture
      //  renderTexture = new RenderTexture(512, 512, 24);
        videoPlayer.targetTexture = renderTexture;
        videoPlayer.aspectRatio = VideoAspectRatio.Stretch;

        // ????? Texture ?????? RawImage
        videoRawImage.texture = renderTexture;
        videoRawImage.gameObject.SetActive(false); // ??? RawImage ??????????????
    }

    void Update()
    {
     

        if (currentState != null)
        {
            currentState.Update(); // ???????????????????
        }
    }

    public RawImage GetRawImage()
    {
        return videoRawImage; // ?????? RawImage
    }

    public void SetDialogText(string text)
    {
        if (dialogText != null)
        {
            dialogText.text = text; // ?????????????????
        }
    }

    // ????????????????????????
    private void ReloadDialog()
    {
        // ????????????????????????
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void LoadDialogFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("dialog_cutscene_clip");
        if (jsonFile != null)
        {
            DialogContainer container = JsonConvert.DeserializeObject<DialogContainer>(jsonFile.text);

            foreach (var dialog in container.dialog_cs_c)
            {
                if (!string.IsNullOrEmpty(dialog.boy))
                {
                    states.Add(new BoyDialogState(this, dialog.boy));
                }
                if (!string.IsNullOrEmpty(dialog.girl))
                {
                    states.Add(new GirlDialogState(this, dialog.girl));
                }
                if (!string.IsNullOrEmpty(dialog.cutscene_clip))
                {
                    states.Add(new CutsceneState(this, dialog.cutscene_clip));
                }
            }

            NextState(); // ??????????????????
        }
        else
        {
            Debug.LogError("JSON file not found!"); // ?????????????????????????? JSON
        }
    }

    public void NextState()
    {
        if (currentState != null)
        {
            currentState.Exit(); // ???????????????????
        }

        currentStateIndex++;
        if (currentStateIndex >= states.Count)
        {
            Debug.Log("Dialog ended"); // ????????????
            return;
        }

        currentState = states[currentStateIndex]; // ??????????????????
        currentState.Enter(); // ????????????????????
    }
}
