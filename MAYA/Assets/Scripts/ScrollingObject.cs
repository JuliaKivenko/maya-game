using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {
    
    //look into moving player through the level instead of the entire level

    public GameManager gm;
    
    private float _oldSpeed;
    private Rigidbody2D _rb2d;

    void Start()
    {
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        _rb2d = GetComponent<Rigidbody2D>();

        _oldSpeed = gm.scrollSpeed;
        _rb2d.velocity = new Vector2(gm.scrollSpeed, 0);
    }

    void FixedUpdate()
    {       
        if (_oldSpeed != gm.scrollSpeed)
        {
            _rb2d.velocity = Vector3.zero;
            _rb2d.velocity = new Vector2(gm.scrollSpeed, 0);
            _oldSpeed = gm.scrollSpeed;
        }
        
    }

}
