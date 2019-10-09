using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog", order = 2)]
public class Dialog : ScriptableObject
{

    [SerializeField] private DialogSpeaker speaker = null;
    [SerializeField, TextArea(3, 10)] private string text = null;
    public DialogSpeaker Speaker => speaker;
    public string Text => text;
}
