using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Speaker", order = 3)]
public class DialogSpeaker : ScriptableObject
{
    [SerializeField] private Sprite sprite = null;
    public Sprite Sprite => sprite;

    [SerializeField] private new string name = null;
    public string Name => name;
}
