using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RandomMetalTile : Tile {

    [SerializeField] private Sprite[] metalSprites;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        int randomNum = Random.Range(0, metalSprites.Length);

        tileData.sprite = metalSprites[randomNum];

    }

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/RandomMetalTile")]
    public static void CreateRandomMetalTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Metaltile", "New Metaltile", "asset", "Save metal tile", "Assets");
        if(path == "")
        {
            return;
        }

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RandomMetalTile>(), path);
    }
#endif
}
