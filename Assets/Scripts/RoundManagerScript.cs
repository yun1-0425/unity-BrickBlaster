using UnityEngine;
using System.Collections.Generic;

public class RoundManagerScript : MonoBehaviour
{
    public enum Gamestate {
        Aiming,
        Shooting, 
        BallMoving,
        BricksMoving
    } 
    public Gamestate state = Gamestate.Aiming;

    public int ballsToShoot = 1;
    public int activeBalls = 0;

    public Vector2 direction;
    public Vector3 shooterPos;
    public float nextStartPosX = 0f;
    
    public float leftWall = -4;
    public float rightWall = 4;
    public float topWall = 6;

    public LineRenderer line;
    public LineRenderer lineRefl;
    public bool noBallsBack = true;

    // bricks
    public List<BrickScript> bricks = new List<BrickScript>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ballDestroyed(float returnPosX) {
        activeBalls --;

        if (noBallsBack) {
            nextStartPosX = returnPosX;
            noBallsBack = false;
        }

        if (activeBalls == 0)
        {
            //nextStartPosX = returnPosX;
            //Debug.Log($"nextStartPosX: {nextStartPosX}");
            startBricksMoving();
        }
    }

    // when balls all are destroyed
    public void startAiming()
    {
            state = Gamestate.Aiming;
            //Debug.Log("All balls destroyed, back to aiming");

            // enable stuff
            GameObject.FindGameObjectWithTag("Shooter").GetComponent<ShooterScript>().input.Enable();
            line.enabled = true;
            lineRefl.enabled = true;            

            noBallsBack = true;
    }


    public void startShooting()
    {
        state = Gamestate.Shooting;
    }
    
    public void startBallMoving()
    {
        state = Gamestate.BallMoving;
    }

    public void startBricksMoving()
    {
        state = Gamestate.BricksMoving;

        // 移動磚塊
        foreach (var brick in bricks)
        {
            brick.moveDownOneStep();
        }
        // reset shooter position

    }
}
