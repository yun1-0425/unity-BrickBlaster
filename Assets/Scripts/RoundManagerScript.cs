using UnityEngine;

public class RoundManagerScript : MonoBehaviour
{
    public enum Gamestate {
        Aiming,
        Shooting, 
        BallMoving
        //BricksMoving
    } 
    public Gamestate state = Gamestate.Aiming;

    public int ballsToShoot = 1;
    public int activeBalls = 0;

    public Vector2 direction;
    public Vector3 shooterPos;
    
    public float leftWall = -4;
    public float rightWall = 4;
    public float topWall = 6;

    public LineRenderer line;
    public LineRenderer lineRefl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ballDestroyed() {
        activeBalls --;

        if (activeBalls == 0)
        {
            state = Gamestate.Aiming;
            Debug.Log("All balls destroyed, back to aiming");
            // enable stuff
            GameObject.FindGameObjectWithTag("Shooter").GetComponent<ShooterScript>().input.Enable();
            line.enabled = true;
            lineRefl.enabled = true;
        }
    }
}
