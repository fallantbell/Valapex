using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFade : MonoBehaviour
{
    public Color color;
    public float speed = 10f;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        color.a = Mathf.Lerp(color.a, 0, Time.deltaTime * speed);

        //update color
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}
