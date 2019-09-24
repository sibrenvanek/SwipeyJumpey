using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation", order = 1)]
public class Conversation : ScriptableObject
{
    [SerializeField] private Dialog[] dialogs = null;
    public Dialog[] Dialogs => dialogs;
}
