using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D player_Rigidbody2D;
    private Vector2 controller;
    public float speed;
    private Animator animator;

    void Start()
    {
        player_Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        controller = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animator.SetBool("isWalking", controller != Vector2.zero);
        animator.SetFloat("InputX", controller.x);
        animator.SetFloat("InputY", controller.y);

        if (controller != Vector2.zero)
        {
            animator.SetFloat("LastInputX", controller.x);
            animator.SetFloat("LastInputY", controller.y);
        }

        if (controller.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (controller.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    void FixedUpdate()
    {
        player_Rigidbody2D.MovePosition(player_Rigidbody2D.position + controller * speed * Time.fixedDeltaTime);
    }



}
