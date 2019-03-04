using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobController : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 minPosition;
    public Vector3 maxPosition;

    public float moveSpeed;
    public float rotateSpeed;

    public string state = "none";
    private string previousState = "";
    private Animator animator;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        animator = this.GetComponent<Animator>();
        material = this.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float random = Random.Range(0f, 100f);

        if (!CheckLimit()) {
            SetRotating(45f);
        }

        switch (state) {
            case "idle":
                if (random > 99f)
                    SetMoving();
                else if (random < 2f)
                    SetRotating(45f);
            break;

            case "rotating":
                UpdateRotating();
            break;

            case "moving":
                if (random > 99f)
                    SetIdle();
                else if (random < 1f)
                    SetRotating(-45f);

                UpdateMoving();
            break;

            case "backToStart":
                UpdateBackToStart();
            break;

            case "appear":
                UpdateAppear();
            break;

            case "disappear":
                UpdateDisappear();
            break;
        }
    }

    private bool CheckLimit() {
        Vector3 position = this.transform.localPosition;
        return (
            position.x > minPosition.x
            && position.x < maxPosition.x
            && position.y > minPosition.y
            && position.y < maxPosition.y
        );
    }

    public void SetIdle() {
        previousState = state;
        state = "idle";
        animator.SetBool("moving", false);
    }

    private void UpdateIdle() {
        this.transform.Rotate(Vector3.forward, Random.Range(-1f, 1f)  * rotateSpeed * Time.deltaTime);
    }

    public void SetMoving() {
        previousState = state;
        state = "moving";
    }


    private void UpdateMoving() {
        if (Random.Range(0f, 100f) > 95f) {
            SetIdle();
            return;
        }

        if (Random.Range(0f, 100f) > 95f) {
            SetRotating(Random.Range(-30f, 30f));
            return;
        }

        float rotateAngle = Random.Range(-1f, 1f) * (rotateSpeed / 4f) * Time.deltaTime;
        this.transform.Rotate(Vector3.forward, rotateAngle);

        this.transform.Translate(this.transform.up * moveSpeed * Time.deltaTime);
    }

    private float remainingRotation;
    public void SetRotating(float angle) {
        previousState = state;
        state = "rotating";

        remainingRotation = angle;
    }

    private void UpdateRotating() {
        float rotateAngle = remainingRotation;
        rotateAngle *= Time.deltaTime;

        remainingRotation -= rotateAngle;

        this.transform.Rotate(Vector3.forward, rotateAngle);
        this.transform.Translate(this.transform.up * (moveSpeed / 4f) * Time.deltaTime);

        if (Mathf.Abs(remainingRotation) < 1f)
            SetMoving();
    }

    public void SetBackToStart() {
        previousState = state;
        state = "backToStart";
    }

    private void UpdateBackToStart() {
        float rotateAngle = -this.transform.eulerAngles.z;
        rotateAngle *= Time.deltaTime;
        this.transform.Rotate(Vector3.forward, rotateAngle);
        
        Vector3 newPosition = this.transform.localPosition;
        newPosition += (startPosition - this.transform.localPosition) * Time.deltaTime;
        this.transform.localPosition = newPosition;
    }

    public void SetShapeNumber(int shapeNumber) {
        animator.SetInteger("shape", shapeNumber);
    }

    private float transitionStart;

    public float appearDuration;
    public float disappearDuration;

    public void SetDisappear() {
        previousState = state;
        state = "disappear";

        transitionStart = Time.time;
    }

    public void SetAppear() {
        previousState = state;
        state = "appear";
        
        animator.SetBool("moving", false);

        transitionStart = Time.time;
    } 

    public void UpdateAppear() {
        float alpha = Mathf.Clamp01((Time.time - transitionStart) / appearDuration);

        Color color = Color.white;
        color.a = alpha;
        material.color = color;

        if (alpha == 1f) {
            SetIdle();
        }
    }

    public void UpdateDisappear() {

        float alpha = Mathf.Clamp01(1f - (Time.time - transitionStart) / disappearDuration);

        Color color = Color.white;
        color.a = alpha;
        material.color = color;

        if (alpha == 0f) {
            this.transform.gameObject.SetActive(false);
            state = "finished";
        }
    }
}
