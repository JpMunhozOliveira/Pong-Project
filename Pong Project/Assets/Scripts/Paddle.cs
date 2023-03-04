using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    float paddleSpeed = 6f;
    [SerializeField] KeyCode upKey;
    [SerializeField] KeyCode downKey;

    void Update()
    {
        float directionY = Input.GetKey(upKey) ? 1f : Input.GetKey(downKey) ? -1f : 0f;
        transform.Translate(0f, directionY * paddleSpeed * Time.deltaTime, 0f);
    }
}
