using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Text text;
    [SerializeField] Text Score1;
    [SerializeField] Text Score2;
    [SerializeField] Ball ball;

    AudioSource audioSource;
    [SerializeField] AudioClip scoreClip;

    enum GameState { Start, Play, Serve, Done }
    GameState state = GameState.Start;

    int player1Score = 0;
    int player2Score = 0;
    int servingPlayer = 1;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        state = GameState.Start;
    }

    void Update()
    {
        Score1.text = player1Score.ToString();
        Score2.text = player2Score.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (state == GameState.Start)
            {
                state = GameState.Serve;
            }
            else if (state == GameState.Serve)
            {
                state = GameState.Play;
            }
            else if (state == GameState.Done)
            {
                state = GameState.Start;
                player1Score = 0;
                player2Score = 0;
                servingPlayer = 1;
            }
        }

        if (state == GameState.Play)
        {
            text.text = "";
            ball.Play();
        }
        else if (state == GameState.Start)
        {
            text.text = "Welcome to Pong!";
            ball.SetDirection(servingPlayer);
        }
        else if (state == GameState.Serve)
        {
            text.text = "Player " + servingPlayer + "'s serve!";
            ball.ResetPosition();
            ball.SetDirection(servingPlayer);
        }
        else if (state == GameState.Done)
        {
            ball.ResetPosition();
        }
    }

    public void PlaySound(AudioClip song)
    {
        audioSource.PlayOneShot(song);
    }

    public void HandleScored(int playerNumber)
    {
        PlaySound(scoreClip);

        if (playerNumber == 1)
        {
            player1Score++;
            servingPlayer = 2;

            if (player1Score >= 10)
            {
                FinishGame(playerNumber);
            }
        }
        else if (playerNumber == 2)
        {
            player2Score++;
            servingPlayer = 1;

            if (player2Score >= 10)
            {
                FinishGame(playerNumber);
            }
        }

        if (state != GameState.Done)
        {
            state = GameState.Serve;
        }
    }

    void FinishGame(int player)
    {
        state = GameState.Done;
        text.text = "Player " + player + " Win!";
    }
}
