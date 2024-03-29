﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    // todo slow start speed

    [Header("General")]
    [Tooltip("in ms/sec")][SerializeField] float numberOfSpeed = 4f;
    [Tooltip("x range")] [SerializeField] float xRange = 5;
    [Tooltip("y range")] [SerializeField] float yRange = 5;

    [Header("Screen Position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -30f;

    [Header("Open Control Based")]
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = 5f;

    float xThrow, yThrow;
    bool isControlEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
    }

    void OnPlayerDeath() // called by string reference
    {
        isControlEnabled = false;
    }


    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * numberOfSpeed * Time.deltaTime;
        float yOffset = yThrow * numberOfSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToControlThrow + pitchDueToPosition;

        float yaw = transform.localPosition.x * positionYawFactor;
        
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
