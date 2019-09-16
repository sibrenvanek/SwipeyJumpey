using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CrumblingGround : MonoBehaviour
{
    [SerializeField] private float crumbleAfter = 2f;
    [SerializeField] private float resetAfter = 2f;
    private TilemapRenderer tileMapRenderer;
    private TilemapCollider2D tilemapCollider2D;
    private float defaultCrumbleAfter = 0f;

    private void Start()
    {
        tileMapRenderer = GetComponent<TilemapRenderer>();
        tilemapCollider2D = GetComponent<TilemapCollider2D>();
        defaultCrumbleAfter = crumbleAfter;
    }

    private IEnumerator Reset()
    {
        tileMapRenderer.enabled = false;
        tilemapCollider2D.enabled = false;
        yield return new WaitForSeconds(resetAfter);
        crumbleAfter = defaultCrumbleAfter;
        tileMapRenderer.enabled = true;
        tilemapCollider2D.enabled = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        crumbleAfter--;

        if (crumbleAfter <= 0)
        {
            StartCoroutine(Reset());
        }
    }
}
