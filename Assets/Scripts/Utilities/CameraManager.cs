using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private EnemySensor _enemySensor;

    private CameraSwitcher _cameraSwitcher;

    private void Awake() {
        _cameraSwitcher = Camera.main.GetComponent<CameraSwitcher>();
    }

    private void Update() {
        
        if (_enemySensor.CurrentEnemy != null && _cameraSwitcher.CurrentCameraType != CameraSwitcher.CameraType.Combat)
        {
            _cameraSwitcher.SwitchCamera(CameraSwitcher.CameraType.Combat);
        } 
        else if (_enemySensor.CurrentEnemy == null && _cameraSwitcher.CurrentCameraType != CameraSwitcher.CameraType.Normal)
        {
            _cameraSwitcher.SwitchCamera(CameraSwitcher.CameraType.Normal);
        }
    }
}
