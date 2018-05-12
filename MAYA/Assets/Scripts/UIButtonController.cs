using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour {

    public KeyCode key;

    private Button _button;

	void Awake () 
    {
        _button = GetComponent<Button>();
	}
	
	void Update () 
    {
        if (Input.GetKeyDown (key))
        {
            _button.onClick.Invoke();
        }
		
	}
}
