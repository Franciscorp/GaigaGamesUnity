
using System;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEditor;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.Controls.AxisControl;
using static Utils;

class BaseMainGameDialogueGenerator
{
    public const string Key = "Key";

    public static MainGameDialogueStructure GenerateMainGameDialogueStructure()
    {
        MainGameDialogueStructure mainGameDialogues = new MainGameDialogueStructure();
        mainGameDialogues.introduction = GenerateIntroduction();

        return mainGameDialogues;
    }

    private static List<Dialogue> GenerateIntroduction()
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
}
