using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueBuilder
{
    public static List<DialogueDatabase> BuildDialogueDatabaseList()
    {
        string csv = CSVImporter.ImportCSV("DialogueCSV");
        List<string[]> parsedCSV = CSVParser.ParseCSV(csv);
        
        List<DialogueNode> dialogueNodesList = BuildDialogueNodesList(parsedCSV);

        List<DialogueList> dialogueListsList = BuildDialogueListsList(dialogueNodesList);

        List<DialogueDatabase> dialogueDatabasesList = BuildDialogueDatabasesList(dialogueListsList);

        return dialogueDatabasesList;
    }

    private static List<DialogueNode> BuildDialogueNodesList(List<string[]> parsedCSV)
    {
        List<DialogueNode> dialogueNodesList = new List<DialogueNode>();
        foreach (string[] row in parsedCSV)
        {
            DialogueNode node = new DialogueNode(Convert.ToInt32(row[0]), Convert.ToInt32(row[1]), row[2], row[3]);
            dialogueNodesList.Add(node);
        }
        return dialogueNodesList;
    }

    private static List<DialogueList> BuildDialogueListsList(List<DialogueNode> dialogueNodesList) 
    {
        List<DialogueList> dialogueListsList = new List<DialogueList>();

        int currentLevel = 1;
        int currentID = 1;
        DialogueList currentDialogueList = new DialogueList(1, 1);

        foreach(DialogueNode node in dialogueNodesList)
        {
            if(currentDialogueList.Level != node.Level)
            {
                currentLevel++;
                currentID = 1;
                currentDialogueList = new DialogueList(currentLevel, currentID);
                dialogueListsList.Add(currentDialogueList);
            }
            else if(currentDialogueList.ID != node.ID)
            {                
                currentID++;
                currentDialogueList = new DialogueList(currentLevel, currentID);
                dialogueListsList.Add(currentDialogueList);
            }
            currentDialogueList.Add(node);
        }

        return dialogueListsList;
    }

    private static List<DialogueDatabase> BuildDialogueDatabasesList(List<DialogueList> dialogueListsList)
    {
        List<DialogueDatabase> dialogueDatabasesList = new List<DialogueDatabase>();

        int currentLevel = 1;
        DialogueDatabase currentDialogueDatabase = new DialogueDatabase(currentLevel);

        foreach(DialogueList dialogueList in dialogueListsList)
        {
            if(currentDialogueDatabase.Level != dialogueList.Level)
            {
                currentLevel++;
                currentDialogueDatabase = new DialogueDatabase(currentLevel);
                dialogueDatabasesList.Add(currentDialogueDatabase);
            }
            currentDialogueDatabase.Add(dialogueList);
        }

        return dialogueDatabasesList;
    }
}
