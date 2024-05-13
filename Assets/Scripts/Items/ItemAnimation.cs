using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    public float amplitude; 
    public float speed; 
    private float initialY;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
        initialY = transform.position.y;

        speed = 5f;
        amplitude = 0.05f;
    }

    void FixedUpdate()
    {
        // Berechne die vertikale Verschiebung basierend auf der Sinusfunktion
        float verticalOffset = amplitude * Mathf.Sin(speed * (Time.time - startTime));

        // Aktualisiere die Position des GameObjects
        transform.position = new Vector2(transform.position.x, initialY+verticalOffset);
    }
}
