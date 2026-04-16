using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;

    // scripts
    public RoundManagerScript manager;
    public ShooterScript shooter;

    public Vector2 dir;
    public float leftWall = -4;
    public float rightWall = 4;
    public float topWall = 6;

    private Vector3 shooterPos;
    public float timeToReachShooter = 1f;
    public bool ballIsAlive = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //shooter = GameObject.FindGameObjectWithTag("Shooter").GetComponent<ShooterScript>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<RoundManagerScript>();

        dir = manager.direction;
        leftWall = manager.leftWall;
        rightWall = manager.rightWall;
        topWall = manager.topWall;

        shooterPos = manager.shooterPos;
        transform.position = shooterPos;
    }

    void FixedUpdate() // 球的移動運算 Update 改 FixedUpdate 才不會有漏算球跑出去問題
    {
        //transform.position += (Vector3)dir * speed * Time.deltaTime; //transform 寫法是瞬間移動，rb 是有路徑的移動
        if (ballIsAlive) {
            rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
        }

        // clamp, just in case
        if (transform.position.x < leftWall)
        {
            transform.position = new Vector3(leftWall, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightWall)
        {
            transform.position = new Vector3(rightWall, transform.position.y, transform.position.z);
        }
        if (transform.position.y > topWall)
        {
            transform.position = new Vector3(transform.position.x, topWall, transform.position.z);
        }

        // go back to shooter
        //if (transform.position.y <= shooterPos.y-0.001f || !ballIsAlive)
        if (transform.position.y < shooterPos.y || !ballIsAlive)
        {
            //Debug.Log("碰到底線");
            ballIsAlive = false;

            dir = (Vector2)(transform.position - shooterPos).normalized;
            float speed = Vector2.Distance(transform.position, shooterPos) / timeToReachShooter;
            rb.MovePosition(rb.position - dir * speed * Time.fixedDeltaTime);
            //Debug.Log("moved");
            
            // Destroy
            if (Vector2.Distance(transform.position, shooterPos) < 0.01f)
            {
                Destroy(gameObject);
                manager.ballDestroyed();
                //Debug.Log("Object Destroyed");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided");
        if (collision.gameObject.layer != 3) {
            dir = Vector2.Reflect(dir, collision.contacts[0].normal);
        }
    }
}
