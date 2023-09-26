[System.Serializable]
public class Quest
{
    public string QuestName = string.Empty;
    public string QuestDescription = string.Empty;
    public string[] Tasks = new string[0];
    public enum QUESTSTATUS { UNASSIGNED, ASSIGNED, COMPLETE };
    public QUESTSTATUS Status = QUESTSTATUS.UNASSIGNED;
    public int steps = 0;
    public int compStep = 1;

    public void IncrementStep()
    {
        steps++;
    }
}
