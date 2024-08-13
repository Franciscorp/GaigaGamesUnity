using System.Collections.Generic;
using UnityEngine.InputSystem;

class BaseIntroductionDialogueGenerator
{
    public const string Key = "Key";


    public static IntroductionDialogueStructure GenerateIntroductionDialogueStructure()
    {
        IntroductionDialogueStructure introduction = new IntroductionDialogueStructure();
        introduction.introduction = GenerateIntroduction();
        introduction.askName = GenerateAskName();

        return introduction;
    }


    // Introduction 
    public static List<Dialogue> GenerateIntroduction()
    {

        List<string> englishText = new List<string>
        {
            "Our story begins with your trip to Grandma's house at the start of summer vacation.",
            "I am the narrator, the all-powerful guide who will accompany you throughout this journey.",
            "But before we start, I need to know..."
        };

        List<string> portugueseText = new List<string>
        {
            "A nossa história começa com a tua ida para a casa da avó no ínicio das férias de verão.",
            "Eu sou o narrador, deus todo poderoso que te acompanha ao longo desta história",
            "Mas antes de começarmos, preciso de saber..."
        };

        Dialogue dialogue = new Dialogue("Introduction" + Key + "1", englishText, portugueseText);

        List<Dialogue> introductionList = new List<Dialogue>();
        introductionList.Add(dialogue);

        return introductionList;
    }

    // Ask Name 
    public static List<Dialogue> GenerateAskName()
    {

        List<string> englishText = new List<string>
        {
            "What should I call you?"
        };

        List<string> portugueseText = new List<string>
        {
            "Como te devo chamar?"
        };

        Dialogue dialogue = new Dialogue("AskName" + Key + "1", englishText, portugueseText);

        List<Dialogue> askNameList = new List<Dialogue>();
        askNameList.Add(dialogue);

        return askNameList;
    }

    // Ask Name 
    public static List<Dialogue> GenerateAskGender()
    {

        List<string> englishText = new List<string>
        {
            "Welcome $username$! Welcome to Gaiga Games!",
            "Are you a boy or a girl?"
        };

        List<string> portugueseText = new List<string>
        {
            "Bem vindo $username$! Bem vindo aos jogos Gaiga!",
            "Diz-me, és um rapaz ou uma rapariga?"
        };

        Dialogue dialogue = new Dialogue("AskName" + Key + "1", englishText, portugueseText);

        List<Dialogue> askNameList = new List<Dialogue>();
        askNameList.Add(dialogue);

        return askNameList;
    }

}