using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRunner : MonoBehaviour
{
    public Sprite[] keys;
    public KeyCode keycode;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keycode))
        {
            spriteRenderer.sprite = keys[1];
            // change sprite to pressed key
            // invoke the correct slider movement
        } else {
            spriteRenderer.sprite = keys[0];
        }
        
    }
}
