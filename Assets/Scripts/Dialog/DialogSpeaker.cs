using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Speaker", order = 3)]
public class DialogSpeaker : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite => sprite;

    [SerializeField]
    private new string name;
    public string Name => name;
}
