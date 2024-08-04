using System.Collections.Generic;
using UnityEngine.InputSystem;

class BaseIntroductionDialogueGenerator
{
    public const string Key = "Key";


    public static IntroductionDialogueStructure GenerateIntroductionDialogueStructure()
    {
        IntroductionDialogueStructure introduction = new IntroductionDialogueStructure();
        introduction.introduction = GenerateIntroduction();

        return introduction;
    }


    // Introduction 
    public static List<Dialogue> GenerateIntroduction()
    {

        List<string> englishText = new List<string>
        {
            "Our story begins with your trip to Grandma's house at the start of summer vacation.",
            "I am the narrator, the all-powerful guide who will accompany you throughout this journey.",
            "But before we start, I need to know...",
            "What should I call you?"
        };

        List<string> portugueseText = new List<string>
        {
            "A nossa história começa com a tua ida para a casa da avó no ínicio das férias de verão.",
            "Eu sou o narrador, deus todo poderoso que te acompanha ao longo desta história",
            "Mas antes de começarmos, preciso de saber...",
            "Como te devo chamar?"
        };

        Dialogue dialogue = new Dialogue("Introduction" + Key + "1", englishText, portugueseText);

        List<Dialogue> introductionList = new List<Dialogue>();
        introductionList.Add(dialogue);

        return introductionList;
    }


}