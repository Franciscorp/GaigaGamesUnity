using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static UtilsSpeechMachine;

public class BaseSpeechMachineDialogueGenerator 
{

    public const string SpeechMachine = "SpeechMachine";
    public const string Key = "Key";
    public const string SuggestionKey = "SuggestionKey";
    public const string RightAnswerKey = "RightAnswerKey";
    public const string WrongAnswerKey = "WrongAnswerKey";

    public static SpeechMachineDialogueStructure GenerateSpeechMachineDialogue()
    {
        // Introduction 

        List<Dialogue> introductionList = GenerateBaseIntroduction();
        List<Dialogue> SuggestionList = GenerateBaseSuggestion();
        List<Dialogue> rightAnswersList = GenerateBaseGenericRightAnswers();
        List<Dialogue> conclusionList = GenerateBaseConclusion();
        List<SpeechElementDialogues> speechElementDialogues = GenerateBaseSpeechElementDialogue();


        //private List<SpeechElementDialogues> speechElements;

        SpeechMachineDialogueStructure speechMachineDialogue = new SpeechMachineDialogueStructure(introductionList, SuggestionList,
            rightAnswersList, conclusionList, speechElementDialogues);

        Debug.Log("[speechMachineDialogue] deserialization = " + JsonUtility.ToJson(speechMachineDialogue));

        return speechMachineDialogue;
    }


    // Introduction 
    public static List<Dialogue> GenerateBaseIntroduction()
    {

        List<string> englishText = new List<string>
        {
            "Welcome to the Speech Machine!",
            "In this game, the objective is to fit the different parts of speech into the correct place in the body.",
            "Do you think you're ready for the challenge?",
            "Drag each piece to the correct place. The goal is to complete the speech machine!",
            "Let's get started!"
        };

        List<string> portugueseText = new List<string>
        {
            "Bem Vindo à Maquina da Fala!",
            "Neste jogo o objetivo é encaixar as diferentes partes da fala no sítio certo do corpo.",
            "Achas que estás pronto para o desafio?",
            "Arrasta cada peça para o sítio certo. O objetivo é conseguir a máquina da fala completa!",
            "Vamos lá começar!"
        };
        
        Dialogue dialogue = new Dialogue(SpeechMachine + "Introduction" + Key + "1", englishText, portugueseText);

        List<Dialogue> introductionList = new List<Dialogue>();
        introductionList.Add(dialogue);

        return introductionList;
    }

    // Suggestions
    public static List<Dialogue> GenerateBaseSuggestion()
    {
        List<Dialogue> suggestionList = new List<Dialogue>();

        List<string> englishText = new List<string>
        {
            "Psst... Here is a clue for the next speech element"
        };

        List<string> portugueseText = new List<string>
        {
            "Psst... Vou te deixar aqui uma pista para a próxima parte da máquina da fala"
        };

        Dialogue dialogue = new Dialogue(SpeechMachine + "Suggestion" + Key + "1", englishText, portugueseText);
        suggestionList.Add(dialogue);

        return suggestionList;
    }

    // right answer / positive feedback
    // TODO could use some optimization
    public static List<Dialogue> GenerateBaseGenericRightAnswers()
    {
        List<Dialogue> suggestionList = new List<Dialogue>();

        string[] englishSingleSentences = new string[]
        {
            "Excellent! You placed the in the right spot.",
            "Great job! That's exactly where it belongs.",
            "Perfect! You've matched the part correctly.",
            "Well done! You got it right.",
            "Spot on! The piece fits perfectly there.",
            "Fantastic! You've placed it in the right spot.",
            "Superb! That's the correct position.",
            "Awesome! You nailed it.",
            "Brilliant! You've got the right placement.",
            "Amazing! You know exactly where it goes."
        };
        
        string[] portugueseSingleSentences = new string[]
        {
            "Excelente! Conseguiste acertar no lugar correto.",
            "Ótimo trabalho! É exatamente onde deveria estar.",
            "Perfeito! Conseguiste combinar a parte corretamente.",
            "Parabéns! Acertaste",
            "Em cheio! A peça encaixa-se perfeitamente ali.",
            "Fantástico! Colocaste a peça no sítio certo.",
            "Excelente! Essa é a posição correta.",
            "Incrível! Acertaste em cheio.",
            "Brilhante! Está na posição correta",
            "Surpreendente! Sabias exatamente onde a colocar."
        };


        for (int i = 0; i < englishSingleSentences.Length; i++)
        {
            int id = i + 1;
            List<string> portugueseSentence = new List<string>() { portugueseSingleSentences[i] };
            List<string> englishSentence = new List<string>() { englishSingleSentences[i] };
            Dialogue dialogue = new Dialogue($"{SpeechMachine}RightAnswer{Key}{id}", englishSentence, portugueseSentence);
            suggestionList.Add(dialogue);
        }


        return suggestionList;
    }


    // conclusion dialogue
    public static List<Dialogue> GenerateBaseConclusion()
    {
        List<Dialogue> conclusionList = new List<Dialogue>();


        List<string> englishText = new List<string>
        {
            "Congratulations! You are becoming a master at understanding human speech!",
            "You have completed all the elements of the speech machine.",
            "But are you ready for the next challenge?",
        };

        List<string> portugueseText = new List<string>
        {
            "Parabéns! Estás a ficar um mestre a conhecer a fala do ser humano!", 
            "Conseguiste concluir todos os elementos da máquina da fala",
            "Mas estarás pronto para o próximo desafio?",
        };


        Dialogue dialogue = new Dialogue(SpeechMachine + "Conclusion" + Key + "1", englishText, portugueseText);
        conclusionList.Add(dialogue);

        return conclusionList;
    }

    public static List<SpeechElementDialogues> GenerateBaseSpeechElementDialogue()
    {
        List<SpeechElementDialogues> SpeechElementDialogues = new List<SpeechElementDialogues>();
        SpeechElementDialogues speechElementDialogue = GenerateBaseBrainDialogue();
        SpeechElementDialogues.Add(speechElementDialogue);
        
        speechElementDialogue = GenerateBaseVoiceBoxDialogue();
        SpeechElementDialogues.Add(speechElementDialogue);

        speechElementDialogue = GenerateBaseTongueDialogue();
        SpeechElementDialogues.Add(speechElementDialogue);

        speechElementDialogue = GenerateBaseMouthDialogue();
        SpeechElementDialogues.Add(speechElementDialogue);

        speechElementDialogue = GenerateBaseLungsDialogue();
        SpeechElementDialogues.Add(speechElementDialogue);

        speechElementDialogue = GenerateBaseTeethDialogue();
        SpeechElementDialogues.Add(speechElementDialogue);

        speechElementDialogue = GenerateBaseDiaphragmDialogue();
        SpeechElementDialogues.Add(speechElementDialogue);

        return SpeechElementDialogues;
    }

    public static SpeechElementDialogues GenerateBaseBrainDialogue()
    {
        List<Dialogue> suggestionList = new List<Dialogue>();
        List<Dialogue> wrongAnswersList = new List<Dialogue>();
        List<Dialogue> rightAnswersList = new List<Dialogue>();
        string englishText;
        string portugueseText;

        englishText = "Where does speech start?!? Think! Think! Think!";
        portugueseText = "A fala começa por onde?!? Pensa! Pensa! Pensa!";

        suggestionList.Add(new Dialogue(SpeechMachine + "Brain" + SuggestionKey + "1", englishText, portugueseText));

        // --------------

        englishText = "Woah! I don't think the brain really belongs there! Try again!";
        portugueseText = "Epa! Acho que o cerébro não é bem aí! Tenta outra vez!";

        wrongAnswersList.Add(new Dialogue(SpeechMachine + "Brain" + WrongAnswerKey + "1", englishText, portugueseText));

        // ---------

        englishText = "Nice! A perfect fit! Now your character is able to think!";
        portugueseText = "Boa! O lugar perfeito para o cerébro! Agora a tua personagem consegue pensar nas falas!";

        rightAnswersList.Add(new Dialogue(SpeechMachine + "Brain" + RightAnswerKey + "1", englishText, portugueseText));

        SpeechElementDialogues elementDialogue = new SpeechElementDialogues(SpeechElements.Brain, suggestionList, wrongAnswersList, rightAnswersList);
        return elementDialogue;
    }

    public static SpeechElementDialogues GenerateBaseVoiceBoxDialogue()
    {
        List<Dialogue> suggestionList = new List<Dialogue>();
        List<Dialogue> wrongAnswersList = new List<Dialogue>();
        List<Dialogue> rightAnswersList = new List<Dialogue>();
        string englishText;
        string portugueseText;

        englishText = "There's a part of the body responsible for creating our voice. What could it be?";
        portugueseText = "Há uma parte do corpo responsável por criar a nossa voz. Qual será?";

        suggestionList.Add(new Dialogue(SpeechMachine + "VoiceBox" + SuggestionKey + "1", englishText, portugueseText));

        // --------------

        englishText = "Oops! That wasn't quite right. Try moving the voice box a towards the neck area. You're almost there!";
        portugueseText = "Ups! Não era bem aó. Tenta mexer a caixa de voz para a zona do pescoço. Estás quase lá!";

        wrongAnswersList.Add(new Dialogue(SpeechMachine + "VoiceBox" + WrongAnswerKey + "1", englishText, portugueseText));

        // ---------

        englishText = "You got it right! The voice box contains our vocal cords, which vibrate when air passes through them, creating our voice.";
        portugueseText = "Acertaste! A caixa de voz contém as nossas cordas vocais, que vibram quando o ar passa por elas e cria a nossa voz.";

        rightAnswersList.Add(new Dialogue(SpeechMachine + "VoiceBox" + RightAnswerKey + "1", englishText, portugueseText));

        SpeechElementDialogues elementDialogue = new SpeechElementDialogues(SpeechElements.VoiceBox, suggestionList, wrongAnswersList, rightAnswersList);
        return elementDialogue;
    }

    public static SpeechElementDialogues GenerateBaseTongueDialogue()
    {
        List<Dialogue> suggestionList = new List<Dialogue>();
        List<Dialogue> wrongAnswersList = new List<Dialogue>();
        List<Dialogue> rightAnswersList = new List<Dialogue>();
        string englishText;
        string portugueseText;

        englishText = "There's a part of the body responsible for creating our voice. What could it be?";
        portugueseText = "Dentro da boca, esta parte do corpo ajuda a formar diferentes sons ao mexer. Qual é que achas que será?";

        suggestionList.Add(new Dialogue(SpeechMachine + "Tongue" + SuggestionKey + "1", englishText, portugueseText));

        // --------------

        englishText = "Oops! That wasn't quite right. Try moving the voice box a towards the neck area. You're almost there!";
        portugueseText = "Ora bolas, não é bem aí. A língua é um músculo que existe dentro da boca, responsável pelo paladar. Tenta outra vez!";

        wrongAnswersList.Add(new Dialogue(SpeechMachine + "Tongue" + WrongAnswerKey + "1", englishText, portugueseText));

        // ---------

        englishText = "You got it right! The voice box contains our vocal cords, which vibrate when air passes through them, creating our voice.";
        portugueseText = "Boa! A língua ao movimentar-se na boca forma certos tipos de sons.";

        rightAnswersList.Add(new Dialogue(SpeechMachine + "Tongue" + RightAnswerKey + "1", englishText, portugueseText));

        SpeechElementDialogues elementDialogue = new SpeechElementDialogues(SpeechElements.Tongue, suggestionList, wrongAnswersList, rightAnswersList);
        return elementDialogue;
    }

    public static SpeechElementDialogues GenerateBaseMouthDialogue()
    {
        List<Dialogue> suggestionList = new List<Dialogue>();
        List<Dialogue> wrongAnswersList = new List<Dialogue>();
        List<Dialogue> rightAnswersList = new List<Dialogue>();
        string englishText;
        string portugueseText;

        englishText = "There's a part of the body that helps us to speak, eat, and smile. What could it be?";
        portugueseText = "Há uma parte do corpo que nos ajuda a falar, comer e sorrir. Qual será?";

        suggestionList.Add(new Dialogue(SpeechMachine + "Mouth" + SuggestionKey + "1", englishText, portugueseText));

        // --------------

        englishText = "Oops! That wasn't quite right. Try thinking about the part of the body that's controls the muscles that move your teeth and chin.";
        portugueseText = "Ups! Não era bem isso. Tenta pensar na parte do corpo que está envolvida em controlar os musculos que fazem os dentes mexer.";

        wrongAnswersList.Add(new Dialogue(SpeechMachine + "Mouth" + WrongAnswerKey + "1", englishText, portugueseText));

        // ---------

        englishText = "You got it right! The mouth helps us to speak, eat, and smile. It plays a crucial role in our daily lives.";
        portugueseText = "Acertaste! A boca ajuda-nos a falar, comer e sorrir. Ela desempenha um papel crucial nas nossas vidas diárias.";

        rightAnswersList.Add(new Dialogue(SpeechMachine + "Mouth" + RightAnswerKey + "1", englishText, portugueseText));

        SpeechElementDialogues elementDialogue = new SpeechElementDialogues(SpeechElements.Mouth, suggestionList, wrongAnswersList, rightAnswersList);
        return elementDialogue;
    }

    public static SpeechElementDialogues GenerateBaseLungsDialogue()
    {
        List<Dialogue> suggestionList = new List<Dialogue>();
        List<Dialogue> wrongAnswersList = new List<Dialogue>();
        List<Dialogue> rightAnswersList = new List<Dialogue>();
        string englishText;
        string portugueseText;

        englishText = "This organ is responsible for providing air to the voice box. What could it be?";
        portugueseText = "Este orgão é responsável por dar ar à caixa de voz. Qual poderá ser?";

        suggestionList.Add(new Dialogue(SpeechMachine + "Lungs" + SuggestionKey + "1", englishText, portugueseText));

        // --------------

        englishText = "Oh no! You're almost there, remember the lungs are a large organ and need space!";
        portugueseText = "Ora bolas! Estás quase lá, lembra-te os pulmões são um orgão grande e precisam de espaço!";

        wrongAnswersList.Add(new Dialogue(SpeechMachine + "Lungs" + WrongAnswerKey + "1", englishText, portugueseText));

        // ---------

        englishText = "Bingo! The lungs are located in the chest and expand as they fill with air.";
        portugueseText = "Bingo! Os pulmões ficam no peito e expandem ao acumular ar dentro deles.";

        rightAnswersList.Add(new Dialogue(SpeechMachine + "Lungs" + RightAnswerKey + "1", englishText, portugueseText));

        SpeechElementDialogues elementDialogue = new SpeechElementDialogues(SpeechElements.Lungs, suggestionList, wrongAnswersList, rightAnswersList);
        return elementDialogue;
    }

    public static SpeechElementDialogues GenerateBaseTeethDialogue()
    {
        List<Dialogue> suggestionList = new List<Dialogue>();
        List<Dialogue> wrongAnswersList = new List<Dialogue>();
        List<Dialogue> rightAnswersList = new List<Dialogue>();
        string englishText;
        string portugueseText;

        englishText = "There's a part of the body that helps articulate sound and is part of the mouth. Do you know what it could be?";
        portugueseText = "Há uma parte do corpo que ajuda a articular o som e faz parte da boca. Sabes qual poderá ser?";

        suggestionList.Add(new Dialogue(SpeechMachine + "Teeth" + SuggestionKey + "1", englishText, portugueseText));

        // --------------

        englishText = "The teeth are part of the mouth and are immobile. Try again!";
        portugueseText = "Os dentes, fazem parte da boca e são imóveis. Tenta outra vez!";

        wrongAnswersList.Add(new Dialogue(SpeechMachine + "Teeth" + WrongAnswerKey + "1", englishText, portugueseText));

        // ---------

        englishText = "Good job! That's right! The teeth, together with the tongue, help form certain sounds and words.";
        portugueseText = "Boa! No sítio certo! Os dentes em conjunto com a língua ajudam a formar certos sons e palavras.";

        rightAnswersList.Add(new Dialogue(SpeechMachine + "Teeth" + RightAnswerKey + "1", englishText, portugueseText));

        SpeechElementDialogues elementDialogue = new SpeechElementDialogues(SpeechElements.Teeth, suggestionList, wrongAnswersList, rightAnswersList);
        return elementDialogue;
    }

    public static SpeechElementDialogues GenerateBaseDiaphragmDialogue()
    {
        List<Dialogue> suggestionList = new List<Dialogue>();
        List<Dialogue> wrongAnswersList = new List<Dialogue>();
        List<Dialogue> rightAnswersList = new List<Dialogue>();
        string englishText;
        string portugueseText;

        englishText = "To speak, we need to expel air from the lungs, and for that, we need the help of a muscle. Do you know which one?";
        portugueseText = "Para falar é preciso retirar o ar dos pulmões e para isso precisamos da ajuda de um músculo, sabes qual é?";

        suggestionList.Add(new Dialogue(SpeechMachine + "Diaphragm" + SuggestionKey + "1", englishText, portugueseText));

        // --------------

        englishText = "Auch! The diaphragm doesn't really belong there! Remember that this muscles is close to the lungs. Try again!";
        portugueseText = "Au! O diafragma não fica bem aí! Lembre-te que este músculo fica perto dos pulmões. Tente novamente!";

        wrongAnswersList.Add(new Dialogue(SpeechMachine + "Diaphragm" + WrongAnswerKey + "1", englishText, portugueseText));

        // ---------

        englishText = "Great! That's right! The diaphragm contracts and relaxes, and in doing so, it forces the lungs to fill or empty with air!";
        portugueseText = "Boa! É isso mesmo! O diafragma aperta e relaxa e ao fazer isso obriga os pulmões a encher ou esvaziar o ar!";

        rightAnswersList.Add(new Dialogue(SpeechMachine + "Diaphragm" + RightAnswerKey + "1", englishText, portugueseText));

        SpeechElementDialogues elementDialogue = new SpeechElementDialogues(SpeechElements.Diaphragm, suggestionList, wrongAnswersList, rightAnswersList);
        return elementDialogue;
    }

}
