using System.Collections.Generic;
using Unity.VisualScripting;

public class DialogueDatabase
{
    private List<DialogueList> _list;
    private int _level;

    public List<DialogueList> List { get { return _list; } }
    public int Level {  get { return _level; } } 

    public DialogueDatabase(int level) 
    {
        _list = new List<DialogueList>();
        _level = level;
    }

    public void Add(DialogueList list)
    {
        _list.Add(list);
    }

    public List<DialogueNode> GetDialogueNodeList(int id)
    {
        return _list[id].DialogueNodeList;
    }
}
