using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;

    public ShooterScript shooter;
    public Vector2 dir;
    public float leftWall = -4;
    public float rightWall = 4;
    public float topWall = 6;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("Shooter").GetComponent<ShooterScript>();

        dir = shooter.direction;
        leftWall = shooter.leftWall;
        rightWall = shooter.rightWall;
        topWall = shooter.topWall;
    }

    void FixedUpdate() // 球的移動運算 Update 改 FixedUpdate 才不會有漏算球跑出去問題
    {
        //transform.position += (Vector3)dir * speed * Time.deltaTime; //transform 寫法是瞬間移動，rb 是有路徑的移動
        rb.MovePosition(rb.position + dir * speed * Time.deltaTime);

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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.layer != 3) {
            dir = Vector2.Reflect(dir, collision.contacts[0].normal);
        }
    }
}
