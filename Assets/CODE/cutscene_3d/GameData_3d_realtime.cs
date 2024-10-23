using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public List<SceneData> scene;
    public BossData boss;
    public PlayerData player;
    public List<CameraPositionData> camera_positions;
    public List<SoundEffectData> sound_effects;
    public List<UIElementData> ui_elements;
}

[System.Serializable]
public class SceneData
{
    public int id;
    public CameraData camera;
    public PlayerActionData player;
    public SoundData sound;
    public BossActionData boss;
    public UIData ui;
}

[System.Serializable]
public class CameraData
{
    public string position;
    public string action; // optional
    public string angle;   // optional
}

[System.Serializable]
public class PlayerActionData
{
    public string action;
    public string distance; // optional
    public string target;   // optional
}

[System.Serializable]
public class SoundData
{
    public string effect;
    public string location; // optional
}

[System.Serializable]
public class BossActionData
{
    public string action;
    public BossEffectData effect; // optional
}

[System.Serializable]
public class BossEffectData
{
    public PhysicsEffectData physics; // optional
}

[System.Serializable]
public class PhysicsEffectData
{
    public ApplyForceData apply_force; // optional
}

[System.Serializable]
public class ApplyForceData
{
    public List<string> targets;
}

[System.Serializable]
public class UIData
{
    public string display; // optional
}

[System.Serializable]
public class BossData
{
    public int id;
    public string name;
    public List<BossAction> actions;
}

[System.Serializable]
public class BossAction
{
    public string action;
    public string distance; // optional
    public string effect;   // optional
    public List<string> target; // optional
}

[System.Serializable]
public class PlayerData
{
    public int id;
    public List<PlayerAction> actions;
}

[System.Serializable]
public class PlayerAction
{
    public string action;
    public string distance; // optional
    public string target;   // optional
}

[System.Serializable]
public class CameraPositionData
{
    public int id;
    public string position;
    public string action; // optional
    public string angle;  // optional
}

[System.Serializable]
public class SoundEffectData
{
    public int id;
    public string effect;
    public string location; // optional
}

[System.Serializable]
public class UIElementData
{
    public int id;
    public string display;
}
