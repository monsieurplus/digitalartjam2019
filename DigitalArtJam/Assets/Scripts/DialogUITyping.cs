using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUITyping : MonoBehaviour
{
    public GameObject[] dots;
    public float[] dotsDelay;
    private Vector3[] dotsOrigin;

    public float speed;

    public float startY;
    public float waveHeight;

    // Start is called before the first frame update
    void Start()
    {
        dotsOrigin = new Vector3[dots.Length];
        for (int i=0; i < dots.Length; i++) {
            dotsOrigin[i] = dots[i].transform.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Mathf.Clamp01(Mathf.Sin(Time.time));

        for(int i=0; i < dots.Length; i++) {
            Vector3 newPosition = dotsOrigin[i];
            newPosition.y +=  Mathf.Clamp01(Mathf.Sin(Time.time * speed + dotsDelay[i])) * waveHeight;
            dots[i].transform.localPosition = newPosition;
        }
    }
}
