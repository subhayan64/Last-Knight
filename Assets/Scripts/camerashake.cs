using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class camerashake : MonoBehaviour
{

    public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
    public float ShakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera[] virtualCam;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    
    private CinemachineVirtualCamera VirtualCamera;

  
    

    // Update is called once per every fixed frame
    void FixedUpdate()
    {
      
        foreach(CinemachineVirtualCamera cvm in virtualCam)
        {
            if (cvm.isActiveAndEnabled == true)
            {
                VirtualCamera = cvm;
            }
        }

        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
                
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }
    public void playShake(float SDuration, float SAmplitude, float SFrequency)
    {
        ShakeElapsedTime = ShakeDuration;
        ShakeDuration = SDuration;          
        ShakeAmplitude = SAmplitude;         
        ShakeFrequency = SFrequency;
    //Debug.Log("shaking");
    }
}