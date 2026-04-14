using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterScript : MonoBehaviour
{
    public float leftWall = -4;
    public float rightWall = 4;
    public float topWall = 6;

    public float lineWidth = 0.05f;
    public Vector2 direction;

    public LineRenderer line;
    public LineRenderer lineRefl;
    private PlayerInputActions input;
    public GameObject ball;

    Vector2 aimPosition;

    void Awake()
    {
        input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Gameplay.Shoot.canceled += onShoot ;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // aim
        Vector2 screenPos = input.Gameplay.Aim.ReadValue<Vector2>();
        aimPosition = Camera.main.ScreenToWorldPoint(screenPos);

        direction = (aimPosition - (Vector2)transform.position).normalized;
        if (direction.x == 0)
        {
            direction.x = 0.0001f; // Avoid division by zero
        }
        if (direction.y < 0.3)
        {
            direction.y = 0.3f; // limit the angle
        }
        //Debug.Log(direction);

        drawAimLine(direction);

        // shoot
    }

    void drawAimLine(Vector2 dir)
    {
        // 原本想用 slope 來判斷先到哪面牆，然後分別計算到牆的距離

        float originX = transform.position.x;
        float originY = transform.position.y;

        // 要走多少到哪一面牆
        float tTop  = (topWall - originY) / dir.y;
        float tLeft = (leftWall - originX) / dir.x;
        float tRight = (rightWall - originX) / dir.x;
        //Debug.Log($"tTop: {tTop}, tLeft: {tLeft}, tRight: {tRight}");

        float minT = Mathf.Min(tTop, Mathf.Max(tLeft, tRight));
        // tLeft 和 tRight 必為一正一負, but based on the restriction that not allowing player to aim downwards.

        Vector3 hitPoint = transform.position + (Vector3)dir * minT;

        // draw line
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hitPoint);

        // reflection
        Vector2 reflDir;
        if (Mathf.Abs(minT - tTop) < 0.001f)
        {
            reflDir = new Vector2(dir.x, -dir.y);
        } else
        {
            reflDir = new Vector2(-dir.x, dir.y);
        }

        // draw reflection line
        lineRefl.SetPosition(0, hitPoint);
        lineRefl.SetPosition(1, hitPoint + (Vector3)reflDir * 20);
        
        line.startWidth = line.endWidth = lineWidth;
        lineRefl.startWidth = lineRefl.endWidth = lineWidth;
    }

    void onShoot(InputAction.CallbackContext context)
    {
        // shoot
        //Debug.Log("Shoot!");
        //ball.SetActive(true);
        Instantiate(ball, transform.position, transform.rotation);
    }
}
