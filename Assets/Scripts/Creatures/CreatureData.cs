using UnityEngine;

[CreateAssetMenu(fileName = "New Creature Data", menuName = "Creature Data")]
public class CreatureData : ScriptableObject
{
    public CreatureInfo[] creatures;
}

[System.Serializable]
public class CreatureInfo
{
    public string creatureName;
    public string description;
    public Sprite creatureImage;
    // Other properties as needed
}
