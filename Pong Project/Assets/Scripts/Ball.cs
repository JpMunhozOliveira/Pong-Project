using UnityEngine;

[RequireComponent(typeof(Boundaries))]
public class Ball : MonoBehaviour
{
    [SerializeField] float ball_speed = 6f;
    float dx;
    float dy;
    Boundaries border;

    [SerializeField] AudioClip hitWall;
    [SerializeField] AudioClip hitPaddle;

    private void Start()
    {
        border = GetComponent<Boundaries>();
    }

    private void Update()
    {
        if (transform.position.x <= border.ScreenBounds.x * -1 + border.ObjectWidth)
        {
            GameManager.instance.HandleScored(2);
        }
        else if (transform.position.x >= border.ScreenBounds.x - border.ObjectWidth)
        {
            GameManager.instance.HandleScored(1);
        }

        if (transform.position.y <= border.ScreenBounds.y * -1 - border.ObjectHeight || transform.position.y >= border.ScreenBounds.y + border.ObjectHeight)
        {
            dy = -dy;

            GameManager.instance.PlaySound(hitWall);
        }
    }

    public void ResetPosition()
    {
        this.transform.position = Vector3.zero;
    }

    public void SetDirection(int serve)
    {
        dx = (serve == 1) ? 1 : -1;
        dy = Random.Range(-1f, 1f);
    }

    public void Play()
    {
        Vector2 direction = new Vector2(dx, dy).normalized;
        dx = direction.x;
        dy = direction.y;
        transform.Translate(dx * ball_speed * Time.deltaTime, dy * ball_speed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.PlaySound(hitPaddle);

        dx = -dx;
        ball_speed *= 1.03f;

        if (dy < 0)
        {
            dy = -Random.Range(0f, 1f);
        }
        else
        {
            dy = Random.Range(0f, 1f);
        }
    }
}