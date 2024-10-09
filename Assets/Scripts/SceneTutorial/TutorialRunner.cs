using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRunner : MonoBehaviour
{
    private SpriteRenderer renderer;
    //private SpriteRenderer _sRenderer;
    //private SpriteRenderer _iRenderer;
    //private SpriteRenderer _kRenderer;

    public Sprite[] keys;

    public KeyCode keycode;
    //public Sprite[] s_keys;
    //public Sprite[] i_keys;
    //public Sprite[] k_keys;

    // slider object

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        //_sRenderer = GetComponent<SpriteRenderer>();
        //_iRenderer = GetComponent<SpriteRenderer>();
        //_kRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(keycode))
        {
            renderer.sprite = keys[1];
            Debug.Log("Running second script");
            // change sprite to pressed W key
            // invoke the correct slider movement
        } else {
            renderer.sprite = keys[0];
            Debug.Log("Not running");
        }
        
        /*if(Input.GetKey(KeyCode.S))
        {
            _sRenderer.sprite = s_keys[1];
            // change sprite to pressed S key
            // invoke the correct slider movement
        } else {
            _sRenderer.sprite = s_keys[0];
        }

        if(Input.GetKey(KeyCode.I))
        {
            _iRenderer.sprite = i_keys[1];
            // change sprite to pressed I key
            // invoke the correct slider movement
        } else {
            _iRenderer.sprite = i_keys[0];
        }

        if(Input.GetKey(KeyCode.K))
        {
            _kRenderer.sprite = k_keys[1];
            // change sprite to pressed K key
            // invoke the correct slider movement
        } else{
            _kRenderer.sprite = k_keys[0];
        }*/
    }
}
