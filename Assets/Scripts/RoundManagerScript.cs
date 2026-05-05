using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RoundManagerScript : MonoBehaviour
{
    public enum Gamestate {
        Aiming,
        Shooting, 
        BallMoving,
        BrickPhase, 
        GameOver
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

    public GameObject gameOverScreen;

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
            startBrickPhase();
        }
    }

    // when balls all are destroyed
    public void startAiming() // called by shooter after it resets
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

    public void startBrickPhase()
    {
        state = Gamestate.BrickPhase;

        // 移動磚塊
        bool hitBottom = false;
        foreach (var brick in bricks)
        {
            if (brick.moveDownOneStep())
            {
                hitBottom = true;
            }
        }
        if (hitBottom)
        {
            gameOver();
        }
        // and reset shooter position
    }

    private void gameOver()
    {
        Debug.Log("Game Over!");
        state = Gamestate.GameOver;
        gameOverScreen.SetActive(true);
    }

    public void restartGame() // not used yet
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
