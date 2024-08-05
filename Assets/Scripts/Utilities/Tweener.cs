using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private Transform _object;
    private Vector3 _destination;

    private float _timeToLerp = 0f;

    private float _elapsedTime;
    public bool IsStarted { get; private set; }

    public void TweenTo(Transform obj, Transform dest, float timeToLerp) {
        _object = obj;
        _destination = dest.position;

        _timeToLerp = timeToLerp;
        
        _elapsedTime = 0f;

        IsStarted = true;
    }

    public void TweenTo(GameObject obj, Transform dest, float timeToLerp) {
        _object = obj.transform;
        _destination = dest.position;

        _timeToLerp = timeToLerp;

        _elapsedTime = 0f;

        IsStarted = true;
    }

    public void TweenTo(GameObject obj, Vector3 dest, float timeToLerp) {
        _object = obj.transform;
        _destination = dest;

        _timeToLerp = timeToLerp;

        _elapsedTime = 0f;

        IsStarted = true;
    }

    public void TweenTo(Transform obj, Vector3 dest, float timeToLerp) {

        _object = obj;
        _destination = dest;

        _timeToLerp = timeToLerp;

        _elapsedTime = 0f;

        IsStarted = true;
    }

    private void Update() {
        
        if (IsStarted) {
            if (_elapsedTime > _timeToLerp) {
                IsStarted = false;
                return;
            }
            _elapsedTime += Time.deltaTime;

            float t = _elapsedTime / _timeToLerp;
            
            _object.transform.position = Vector3.Lerp(_object.transform.position, _destination, t);
        }
    }
    
}
