
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.Controls.AxisControl;
using static Utils;
using System.Collections;

class BaseMainGameDialogueGenerator
{
    public const string Key = "Key";

    public static MainGameDialogueStructure GenerateMainGameDialogueStructure()
    {
        MainGameDialogueStructure mainGameDialogues = new MainGameDialogueStructure();
        mainGameDialogues.introduction1 = GenerateIntroduction1();
        mainGameDialogues.introduction2 = GenerateIntroduction2();

        return mainGameDialogues;
    }

    private static List<Dialogue> GenerateIntroduction1()
    {
        List<string> englishText = new List<string>
        {
            "As you enter Grandma’s house, everything feels so familiar. The walls are covered with pictures, each one bringing back a special memory.",
            "You pause for a moment to admire the photos on the wall when something new catches your eye..."
        };

        List<string> portugueseText = new List<string>
        {
            "Ao entrar na casa da avó, tudo parece tão familiar. As paredes estão repletas de quadros, cada um trazendo uma memória especial.", 
            "Tu chegas a parar um pouco para apreciar as fotos na parede, quando algo novo te chama a atenção..."
        };

        Dialogue dialogue = new Dialogue("MainGameIntroduction" + Key + "1", Npc.Narrator, englishText, portugueseText);

        List<Dialogue> introductionList = new List<Dialogue>();
        introductionList.Add(dialogue);
        
        return introductionList;
    }


    private static List<Dialogue> GenerateIntroduction2()
    {
        List<string> englishText = new List<string>
        {
            "Ahh, it's been a while... I almost forgot about these photos.",
            "Here is my grandma, always with a loving look, and that's my grandpa, the biggest joker of all...",
            "But... who is this?!?",
            "It looks like a black cat!"
        };

        List<string> portugueseText = new List<string>
        {
            "Ahh já faz algum tempo... nem me lembrava destas fotos.",
            "Aqui está a minha avó, sempre com um ar carinhoso e aquele é o meu avó, o maior brincalhão de todos...",
            "Mas... quem é este?!?",
            "Parece um gato preto!"
        };

        Dialogue dialogue = new Dialogue("MainGameIntroduction" + Key + "1", Npc.Narrator, englishText, portugueseText);

        List<Dialogue> introductionList = new List<Dialogue>();
        introductionList.Add(dialogue);

        return introductionList;
    }
}
