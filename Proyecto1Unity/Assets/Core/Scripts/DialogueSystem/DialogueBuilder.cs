using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueBuilder
{
    public static List<DialogueList> BuildDialogueListsList(int currentLevel)
    {
        string csv = CSVImporter.ImportCSV("DialogueCSV");
        List<string[]> parsedCSV = CSVParser.ParseCSV(csv);
        
        List<DialogueNode> dialogueNodesList = BuildDialogueNodesList(parsedCSV);

        List<DialogueList> dialogueListsList = BuildDialogueListsList(currentLevel, dialogueNodesList);

        return dialogueListsList;
    }

    private static List<DialogueNode> BuildDialogueNodesList(List<string[]> parsedCSV)
    {
        List<DialogueNode> dialogueNodesList = new List<DialogueNode>();
        foreach (string[] row in parsedCSV)
        {
            DialogueNode node = new DialogueNode(Convert.ToInt32(row[0]), Convert.ToInt32(row[1]), row[2], row[3].Remove(row[3].Length - 1));
            dialogueNodesList.Add(node);
        }
        return dialogueNodesList;
    }

    private static List<DialogueList> BuildDialogueListsList(int currentLevel, List<DialogueNode> dialogueNodesList) 
    {
        List<DialogueList> dialogueListsList = new List<DialogueList>();

        int currentID = 1;
        DialogueList currentDialogueList = new DialogueList(currentLevel, currentID);
        dialogueListsList.Add(currentDialogueList);

        foreach (DialogueNode node in dialogueNodesList)
        {
            if(currentLevel == node.Level)
            {
                if(currentDialogueList.ID != node.ID)
                {
                    currentID++;
                    currentDialogueList = new DialogueList(currentLevel, currentID);
                    dialogueListsList.Add(currentDialogueList);
                }
                currentDialogueList.Add(node);
            }
            else if (currentLevel < node.Level)
            {
                break;
            }
        }

        return dialogueListsList;
    }
}
