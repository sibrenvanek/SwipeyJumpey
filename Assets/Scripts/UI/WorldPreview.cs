using UnityEngine;

public class WorldPreview : MonoBehaviour
{
    [SerializeField] private string worldName = "Planet";
    [SerializeField] private int sceneIndex = 0;
    [SerializeField] private bool isInDevelopment = false;
    public string WorldName => worldName;
    public int SceneIndex => sceneIndex;
    public bool IsInDevelopment => isInDevelopment;
}
