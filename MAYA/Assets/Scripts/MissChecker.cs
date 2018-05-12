using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissChecker : MonoBehaviour {

    private RaycastHit2D _hit;
    private bool _playOnce = true;

    public PlayerController player;
    public LayerMask defLayerMask;
    public ParticleSystem missBurst;

	
	void Update () {

        _hit = Physics2D.Linecast(transform.position, Vector2.up * -100, defLayerMask);
        Debug.DrawRay(transform.position, Vector2.up * -100, Color.yellow);

        if (_hit.transform == null)
        {
            return;
        }

        if (_hit.transform.GetComponent<PlayerController>() &&  player.ringPassed == false && _playOnce == true)
        {
            Instantiate(missBurst, player.transform.position, Quaternion.identity);
            missBurst.Play();
            StartCoroutine(player.SmoothEnergyChange(player.energy - player.obstacleEnergyLoss));
            _playOnce = false;
            gameObject.SetActive(false);

        }

        
		
	}
}
