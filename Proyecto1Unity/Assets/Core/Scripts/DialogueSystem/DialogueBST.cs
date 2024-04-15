using System.Collections;
using System.Collections.Generic;

public class DialogueBST
{
    private DialogueBSTNode _root;

    public DialogueBST(List<DialogueList> dialogueListsList)
    {
        InsertListBalanced(dialogueListsList);
    }

    private void InsertListBalanced(List<DialogueList> dialogueListsList)
    {
        _root = new DialogueBSTNode(dialogueListsList, 0, dialogueListsList.Count - 1);
    }

    public List<DialogueNode> Search(int id)
    {
        if (_root == null)
            throw new System.Exception("DialogueBST is empty and cannot find the item.");
        else
        {
            return _root.Search(id);
        }
    }

    private class DialogueBSTNode
    {
        private List<DialogueNode> _dialogueNodeList;
        private DialogueBSTNode _left;
        private DialogueBSTNode _right;
        private int _id;

        public DialogueBSTNode(List<DialogueList> dialogueListsList, int start, int end)
        {
            if (start > end)
                return;

            int midList = (start + end) / 2;
            DialogueList dialogueList = dialogueListsList[midList];
            _dialogueNodeList = dialogueList.DialogueNodeList;
            _id = dialogueList.ID;

            _left = new DialogueBSTNode(dialogueListsList, start, midList - 1);
            _right = new DialogueBSTNode(dialogueListsList, midList + 1, end);
        }

        public List<DialogueNode> Search(int id)
        {
            if(id == _id)
            {
                return _dialogueNodeList;
            }
            else
            {
                if(id < _id)
                {
                    if(_left != null)
                    {
                        return _left.Search(id);
                    }
                    else
                    {
                        throw new System.Exception($"There are not dialogue lines for id {id}.");
                    }
                }
                else
                {
                    if (_right != null)
                    {
                        return _right.Search(id);
                    }
                    else
                    {
                        throw new System.Exception($"There are not dialogue lines for id {id}.");
                    }
                }
            }
        }
    }
}
