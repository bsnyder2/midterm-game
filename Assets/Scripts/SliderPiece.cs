using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPiece : MonoBehaviour
{
    public Sprite[] frames;
    private SpriteRenderer spriteRenderer;

    // Awake is called when script instance is loaded
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Open()
    {
        // spriteRenderer will be initialized by the time this is called
        spriteRenderer.sprite = frames[0];
    }

    public void Close()
    {
        spriteRenderer.sprite = frames[1];
    }
}