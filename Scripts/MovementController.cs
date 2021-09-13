using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private GameObject joystick, toque;

    private Rigidbody2D rb;
    private float speed = 3f;
    private Touch oneTouch;

    private Vector2 touchPosition;
    private Vector2 moveDirection;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        joystick.SetActive(false);
        toque.SetActive(false);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            oneTouch = Input.GetTouch(0);

            //touchPosition = Camera.main.ScreenToViewportPoint(oneTouch.position);
            touchPosition = Camera.main.ScreenToWorldPoint(oneTouch.position);

            switch (oneTouch.phase)
            {
                case TouchPhase.Began:
                    joystick.SetActive(true);
                    toque.SetActive(true);

                    joystick.transform.position = touchPosition;
                    toque.transform.position = touchPosition;

                    break;

                case TouchPhase.Stationary:
                    movePersonagem();
                    break;

                case TouchPhase.Moved:
                    movePersonagem();
                    break;

                case TouchPhase.Ended:
                    joystick.SetActive(false);
                    toque.SetActive(false);

                    rb.velocity = Vector2.zero;
                    break;

                default:
                    print("?");
                    break;

            }
        }
    }

    private void movePersonagem()
    {
        toque.transform.position = touchPosition;

        toque.transform.position = new Vector3(
                Mathf.Clamp(toque.transform.position.x, joystick.transform.position.x - 0.8f, joystick.transform.position.x + 0.8f),
                Mathf.Clamp(toque.transform.position.y, joystick.transform.position.y - 0.8f, joystick.transform.position.y + 0.8f), 10
            );

        joystick.transform.position = new Vector3(joystick.transform.position.x, joystick.transform.position.y, 10);//*******

        moveDirection = (toque.transform.position - joystick.transform.position).normalized;
        rb.velocity = moveDirection * speed;

        Vector3 targetPos = moveDirection;
        float angle = Mathf.Atan2(-targetPos.x, targetPos.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
