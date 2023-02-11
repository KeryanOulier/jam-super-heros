using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    // Components
    Rigidbody2D rb;

    // movement
    [Header("Movement")]
    public float walkSpeed = 4;
    float activeMoveSpeed;
    float inputHorizontal;
    float inputVertical;

    // dash
    [Header("Dash")]
    public float dashSpeed;
    public float dashLength = 0.5f, dashCooldown = 1.0f;
    bool canDash = true;
    bool dashing = false;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dashing)
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            inputVertical = Input.GetAxisRaw("Vertical");
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        var v = new Vector2(inputHorizontal, inputVertical);
        v.Normalize();
        rb.velocity = v * activeMoveSpeed;
    }

    IEnumerator Dash()
    {
        canDash = false;
        dashing = true;
        activeMoveSpeed = dashSpeed;
        yield return new WaitForSeconds(dashLength);
        dashing = false;
        activeMoveSpeed = walkSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        print("canDash");
    }
}