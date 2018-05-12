using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    private bool _isDone = true;
    private float _multiplier = 1;

	void Update () 
    {
        transform.Translate(Vector3.up * _multiplier * Time.deltaTime);

        if (_isDone == true)
        {
            _isDone = false;
            StartCoroutine(ChangeMultiplier());
        }
		
	}

    IEnumerator ChangeMultiplier()
    {
        yield return new WaitForSeconds(0.5f);
        _multiplier = _multiplier * -1f;
        _isDone = true;
        yield return null;
    }
}
