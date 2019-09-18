using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog", order = 2)]
public class Dialog : ScriptableObject
{

    [SerializeField]
    private DialogSpeaker speaker;
    public DialogSpeaker Speaker => speaker;

    [SerializeField, TextArea(3, 10)]
    private string text;
    public string Text => text;
}
