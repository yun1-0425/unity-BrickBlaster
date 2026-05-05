using UnityEngine;
using TMPro;

public class BrickScript : MonoBehaviour
{
    public int  hp =  1;
    public TextMeshPro hpText;
    public RoundManagerScript manager;
    public float deadLineY = -6.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<RoundManagerScript>();
        manager.bricks.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = hp.ToString();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3) {
            hp --;
            if (hp <= 0) // Die
            {
                manager.bricks.Remove(this);
                Destroy(gameObject);
            }
        }
    }

    public bool moveDownOneStep()
    {
        transform.position += Vector3.down;
        return transform.position.y <= deadLineY;
    }
}
