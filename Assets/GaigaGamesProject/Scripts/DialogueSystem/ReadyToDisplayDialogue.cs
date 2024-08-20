
using static Utils;
using System.Collections.Generic;

public class ReadyToDisplayDialogue{
    public string npcName;
    public Npc npc;
    public string[] text;

    public ReadyToDisplayDialogue()
    {
        npcName = GetPortugueseTranslatedNpcList(Npc.Error);
        npc = Npc.Error;
        text = new string[] { "Default Error" };
    }

    public ReadyToDisplayDialogue(Npc npc, string[] text)
    {
        this.npcName = GetPortugueseTranslatedNpcList(npc);
        this.npc = npc;
        this.text = text;
    }

    public ReadyToDisplayDialogue(string npcName, Npc npc, string[] text)
    {
        this.npcName = npcName;
        this.npc = npc;
        this.text = text;
    }
}
