using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public enum CameraType {
        Normal,
        Combat,
    }
    [Serializable]
    public struct CameraData {
        public CinemachineFreeLook camera;
        public CameraType cameraType;
    }

    [SerializeField] private List<CameraData> cameras;
    private CameraType currentCameraType;

    public CameraType CurrentCameraType { get => currentCameraType; }

    private void Start() {
        SwitchCamera(CameraType.Normal);
    }

    public void SwitchCamera(CameraType cameraType) {
        foreach (var camera in cameras) {
            camera.camera.Priority = camera.cameraType == cameraType ? 10 : 0;
        }
        currentCameraType = cameraType;
    }
}
