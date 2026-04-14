using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float speed = 5f;
    public ShooterScript shooter;
    public Vector2 dir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("Shooter").GetComponent<ShooterScript>();
        dir = shooter.direction;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)dir * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.layer != 3) {
            dir = Vector2.Reflect(dir, collision.contacts[0].normal);
        }
    }
}
