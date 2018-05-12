using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


    [Header("Energy Variables")]
    public float flyForceMultiplier;    
    public float maxEnergy;
    public float energyLossMultiplier;
    public float ringEnergyIncrease;
    public float obstacleEnergyLoss;
    public float energy;
    public int score;

    [Header("Particle Effects")]
    public ParticleSystem ringBurst;
    public ParticleSystem ringDashBurst;
    public ParticleSystem dashTrail;
    
    [Header("Other")]
    public Vector2 moveForwardDirection;
    public GameManager gameManager;


    private Rigidbody2D _rigidBody;

    private bool _enableInput = true;
    private bool _isDashing;
    
    private bool _ringPassed;
    public bool ringPassed
    { 
        get { return _ringPassed; }
    }

    int _buttonPressed = 0;
    int _buttonReleased = 0;



    void Start () 
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        energy = maxEnergy;
	}
	


	void Update () 
    {

        //Ensures score doesn't go negative
        score = (score < 0) ? 0 : score;

        //Steadily decreases energy over time
        energy -= Time.deltaTime * energyLossMultiplier; 
        energy = (energy < 0.1) ? 0 : energy;

        //Decides if input is enabled or not
        _enableInput = (energy == 0f) ? false : true; //perhaps avoid setting floats to 0, ask teachers about that
        if (_enableInput == false)
        {
            _rigidBody.constraints = RigidbodyConstraints2D.None;
            gameManager.scrollSpeed = gameManager.normalScrollSpeed;
            return;
        }


        //Limits player's forward movement
        Vector3 pos = gameObject.transform.position;
        if (pos.x >= 12)
        {
            gameObject.transform.position = new Vector3(12, pos.y, pos.z);
        }


        //Checks for input and manages controls
        MapInputToInteger();
        ManageInput();


    }

    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.tag)
        {
            case "Finish":
                gameManager.Win();
                break;

            case "KillV":
                gameManager.Lose();
                break;

            case "Obstacle":
                StartCoroutine(SmoothEnergyChange(energy - obstacleEnergyLoss));
                score--;
                break;

            case "Ring":
                if (_isDashing == false && ringPassed == false)
                {
                    ringBurst.transform.position = transform.position;
                    ringBurst.Play();
                    _ringPassed = true;
                }
                break;

            case "DashArea":
                _ringPassed = false;
                break;
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "DashArea" && ringPassed == false && _isDashing == true)         
        {
            ringDashBurst.transform.position = transform.position;
            ringDashBurst.Play();
            StartCoroutine(SmoothEnergyChange(energy + ringEnergyIncrease));
            score++;
            _ringPassed = true;
        }
    }



    private void MapInputToInteger()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _buttonPressed = 1;
            _buttonReleased = 0;
        }
        if (Input.GetButton("Dash"))
        {
            _buttonPressed = 2;
            _buttonReleased = 0;
        }


        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            _buttonReleased = 1;
            _buttonPressed = 0;
        }
        if (Input.GetButtonUp("Dash"))
        {
            _buttonReleased = 2;
            _buttonPressed = 0;
        }
        else
        {
            _buttonReleased = 0;
        }

    }



    private void ManageInput ()
    {
        if (_buttonPressed == 1)
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.AddForce(moveForwardDirection * flyForceMultiplier);
        }
        if (_buttonReleased == 1)
        {
            _rigidBody.velocity = Vector3.zero;
        }
        if (_buttonPressed == 2)                            
        {
            transform.localScale = new Vector3(1.3f, 0.7f, 1);
            dashTrail.Play();
            _isDashing = true;
            _rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
            gameManager.scrollSpeed = gameManager.dashScrollSpeed;
        }
        if (_buttonReleased == 2)
        {
            transform.localScale = new Vector3(1, 1f, 1);
            dashTrail.Stop();
            _isDashing = false;
            _rigidBody.constraints = RigidbodyConstraints2D.None;
            StartCoroutine(gameManager.SmoothScrollSpeedChange(gameManager.normalScrollSpeed));                   
            StartCoroutine(SmoothMoveBack());
        }
    }


    
    //Smoothly decreases or increases energy when colliding with obstacle/ring/dash area
    public IEnumerator SmoothEnergyChange (float newEnergyValue)                                             
    {
        newEnergyValue = Mathf.Min(newEnergyValue, maxEnergy);
        
        while (energy > newEnergyValue)
        {
            energy -= 6f * Time.deltaTime;
            yield return new WaitForEndOfFrame();       
            
        }        

        while (energy < newEnergyValue) 
        {
            energy += 6f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

    }



    //Smoothly moves player back as soon as they stop dashing
    IEnumerator SmoothMoveBack()                                                                    
    {
        while (transform.position.x > 0)
        {
            if (Time.timeScale > 0)
            {
                transform.Translate(new Vector2(-0.1f, 0));
                yield return null;
            }
            else
            {
                yield break;
            }
            
        }
       
    }
    


}
