using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DialogueList
{
    private List<DialogueNode> _dialogueList;
    private int _level;
    private int _id;

    public List<DialogueNode> DialogueNodeList { get { return _dialogueList; } private set { _dialogueList = value; } }
    public int Level { get { return _level; } }
    public int ID { get { return _id; } }

    public DialogueList(int level, int id)
    {
        _dialogueList = new List<DialogueNode>();
        _level = level;
        _id = id;
    }

    public void Add(DialogueNode node)
    {
        _dialogueList.Add(node);
    }
}
