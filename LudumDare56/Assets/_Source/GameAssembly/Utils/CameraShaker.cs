using System;
using Cinemachine;
using UnityEngine;

namespace Utils
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private NoiseSettings noiseSettings;

        private CinemachineBasicMultiChannelPerlin _vCam;
        
        private void Awake()
        {
            _vCam = GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        }

        public void StartShake()
        {
            _vCam.enabled = true;
        }

        public void StopShake()
        {
            _vCam.enabled = false;
        }
    }
}
