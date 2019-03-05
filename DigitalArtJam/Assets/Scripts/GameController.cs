using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject splash;
    public BlobController blob;
    public DialogManager dialog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        StartIntro();
        dialog.gameObject.SetActive(false);
        blob.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            TogglePause();

        if (introIng)
            UpdateIntro();

        if (outroIng)
            UpdateOutro();
    }

    private bool paused = false;
    private void TogglePause() {
        paused = !paused;

        Time.timeScale = (paused ? 0f : 1f);
    }

    private bool introIng = false;
    private float introStart;
    public float introDuration = 9f;
    public void StartIntro() {
        introIng = true;
        introStart = Time.time;
        splash.GetComponent<Animator>().SetBool("intro", true);
    }

    private void UpdateIntro() {
        if (Time.time >= (introStart + introDuration)) {
            splash.GetComponent<Animator>().SetBool("intro", false);

            blob.gameObject.SetActive(true);
            blob.SetAppear();
            dialog.gameObject.SetActive(true);
            introIng = false;
        }
    }

    private bool outroIng = false;
    private float outroStart;
    public float outroDuration = 10f;
    private float outroBgDuration = 3f;
    public CanvasGroup outroBg;

    public void StartOutro() {
        outroIng = true;
        outroStart = Time.time;
        splash.GetComponent<Animator>().SetBool("intro", true);
    }

    private void UpdateOutro() {
        float bgAlpha = Mathf.Clamp01((Time.time - outroStart) / outroBgDuration);
        outroBg.alpha = bgAlpha;

        if (Time.time >= (outroStart + outroDuration)) {
            splash.GetComponent<Animator>().SetBool("outro", false);

            outroIng = false;
        }
    }
}
