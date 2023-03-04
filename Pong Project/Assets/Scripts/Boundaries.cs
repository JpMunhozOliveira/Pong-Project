using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    public Vector2 ScreenBounds { get => screenBounds; set => screenBounds = value; }
    public float ObjectWidth { get => objectWidth; set => objectWidth = value; }
    public float ObjectHeight { get => objectHeight; set => objectHeight = value; }

    void Start()
    {
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        ObjectWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        ObjectHeight = -gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        //viewPos.x = Mathf.Clamp(viewPos.x, ScreenBounds.x * -1 + ObjectWidth, ScreenBounds.x - ObjectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, ScreenBounds.y * -1 -ObjectHeight, ScreenBounds.y + ObjectHeight);
        transform.position = viewPos;
    }

    
}
