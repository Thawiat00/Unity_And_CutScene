using UnityEngine;

// Root myDeserializedClass = JsonUtility.FromJson<Root_cutscene_3d>(myJsonResponse);
[System.Serializable]
public class Action_cutscene_3d
{
    public string action;
    public string distance;
    public string effect;
    public string[] target;
}

[System.Serializable]
public class ApplyForce_cutscene_3d
{
    public string[] targets;
}

[System.Serializable]
public class Boss_cutscene_3d
{
    public string action;
    public string distance;
    public string target;
    public Effect_cutscene_3d effect;
    public int id;
    public string name;
    public Action_cutscene_3d[] actions;
}

[System.Serializable]
public class Camera_cutscene_3d
{
    public string position;
    public string action;
    public string angle;
}

[System.Serializable]
public class CameraPosition_cutscene_3d
{
    public int id;
    public string position;
    public string angle;
    public string action;
}

[System.Serializable]
public class ComboActing_cutscene_3d
{
    public string key;
    public DataActing_cutscene_3d[] data_acting;
}

[System.Serializable]
public class DataActing_cutscene_3d
{
    public Camera_cutscene_3d camera;
    public Player_cutscene_3d player;
    public Sound_cutscene_3d sound;
    public Boss_cutscene_3d boss;
    public Ui_cutscene_3d ui;
}

[System.Serializable]
public class Effect_cutscene_3d
{
    public Physics_cutscene_3d physics;
}

[System.Serializable]
public class Physics_cutscene_3d
{
    public ApplyForce_cutscene_3d apply_force;
}

[System.Serializable]
public class Player_cutscene_3d
{
    public string action;
    public string distance;
    public string target;
    public int id;
    public Action_cutscene_3d[] actions;
}

[System.Serializable]
public class Root_cutscene_3d
{
    public Scene_cutscene_3d[] scene;
    public ComboActing_cutscene_3d[] combo_acting;
    public Boss_cutscene_3d boss;
    public Player_cutscene_3d player;
    public CameraPosition_cutscene_3d[] camera_positions;
    public SoundEffect_cutscene_3d[] sound_effects;
    public UiElement_cutscene_3d[] ui_elements;
}

[System.Serializable]
public class Scene_cutscene_3d
{
    public int id;
    public string key;
}

[System.Serializable]
public class Sound_cutscene_3d
{
    public string effect;
    public string location;
}

[System.Serializable]
public class SoundEffect_cutscene_3d
{
    public int id;
    public string effect;
    public string location;
}

[System.Serializable]
public class Ui_cutscene_3d
{
    public string display;
}

[System.Serializable]
public class UiElement_cutscene_3d
{
    public int id;
    public string display;
}
