﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rigidBody;
    private int count;

    void Start()
    {
        if (countText == null) throw new ArgumentNullException(nameof(countText));
        if (winText == null) throw new ArgumentNullException(nameof(winText));

        rigidBody = GetComponent<Rigidbody>();
        count = 0;
        winText.text = "";
        SetCountText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rigidBody.AddForce(new Vector3(moveHorizontal,0, moveVertical) * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        countText.text = $"Count: {count}";
        if (count >= 12)
        {
            winText.text = "You Win";
        }
    }
}