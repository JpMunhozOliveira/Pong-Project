using UnityEngine;

[RequireComponent(typeof(Boundaries))]
public class Ball : MonoBehaviour
{
    float ball_speed = 6f;
    float dx;
    float dy;
    Boundaries border;

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
        dx = -dx;

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