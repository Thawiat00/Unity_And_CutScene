using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataLoader_3d_realtime : MonoBehaviour
{
    [SerializeField]
    private GameData gameData;

    void Start()
    {
        LoadGameData();
    }

    private void LoadGameData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("dialog_3d_realtime");

        if (jsonFile == null)
        {
            Debug.LogError("Failed to load JSON file.");
            return;
        }

        gameData = JsonUtility.FromJson<GameData>(jsonFile.text);

        // ????????????????????
        CheckLoadedData();

        // ???????????? debug ??????????????????????
        Debug.Log("Game data loaded successfully.");
    }

    private void CheckLoadedData()
    {
        if (gameData.scene == null || gameData.scene.Count == 0)
        {
            Debug.LogWarning("No scenes data found in JSON.");
        }

        if (gameData.camera_positions == null || gameData.camera_positions.Count == 0)
        {
            Debug.LogWarning("No camera positions data found in JSON.");
        }

        if (gameData.sound_effects == null || gameData.sound_effects.Count == 0)
        {
            Debug.LogWarning("No sound effects data found in JSON.");
        }

        if (gameData.ui_elements == null || gameData.ui_elements.Count == 0)
        {
            Debug.LogWarning("No UI elements data found in JSON.");
        }

        // ??????????? ????????????????????????????
    }
}
