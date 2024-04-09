using UnityEngine.UIElements.Experimental;

public class DialogueNode
{
    private int _level;
    private int _id;
    private string _character;
    private string _text;

    public int Level { get { return _level; } }
    public int ID { get { return _id; } }
    public string Character { get { return _character; } }
    public string Text { get { return _text; } }

    public DialogueNode(int level, int id, string character, string text)
    {
        _level = level;
        _id = id;
        _character = character;
        _text = text;
    }
}
