using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathe : MonoBehaviour
{
    [SerializeField] float moveToTargetSpeed;
    [SerializeField] float moveToHoldSpeed;

    [SerializeField] AnimationCurve toTargetCurve;
    [SerializeField] AnimationCurve toHoldCurve;

    [SerializeField] Vector3 targetOffset;
    [Space]

    [SerializeField] float tiltSpeed;
    [SerializeField] float angle;

    [SerializeField] AnimationCurve tiltCurve;

    private RectTransform rect;
    private Vector3 holdPos;
    private void Start()
    {
        rect = this.GetComponent<RectTransform>();
        holdPos = rect.localPosition;

        StartCoroutine(Bounce());
        StartCoroutine(Tilt());
    }

    public void Initialize(float _moveToTargetSpeed, float _moveToHoldSpeed, AnimationCurve _toTargetCurve, AnimationCurve _toHoldCurve, Vector3 _targetOffset, float _tiltSpeed, float _angle)
    {
        /*moveToTargetSpeed = _moveToTargetSpeed;
        moveToHoldSpeed = _moveToHoldSpeed;

        toTargetCurve = */
    }

    private IEnumerator Bounce()
    {
        while (true)
        {
            float lerp = 0;

            // Move out 
            while (lerp <= 1)
            {

                rect.localPosition = Vector3.Lerp(holdPos, holdPos + targetOffset, toTargetCurve.Evaluate(lerp));
                
                lerp += Time.deltaTime * moveToTargetSpeed;
                yield return null;
            }

            // Reset
            lerp = 0;

            // Move in 
            while (lerp <= 1)
            {

                rect.localPosition = Vector3.Lerp(holdPos + targetOffset, holdPos, toHoldCurve.Evaluate(lerp));

                lerp += Time.deltaTime * moveToHoldSpeed;
                yield return null;
            }
        }
    }

    private IEnumerator Tilt()
    {
        while (true)
        {
            float lerp = 0;

            // Move out 
            while (lerp <= 1)
            {

                rect.localEulerAngles = Vector3.Lerp(Vector3.zero, Vector3.forward * angle, tiltCurve.Evaluate(lerp));

                lerp += Time.deltaTime * tiltSpeed;
                yield return null;
            }

            // Reset
            lerp = 0;

            // Move in 
            while (lerp <= 1)
            {

                rect.localEulerAngles = Vector3.Lerp( Vector3.forward * angle, Vector3.zero, tiltCurve.Evaluate(lerp));

                lerp += Time.deltaTime * tiltSpeed;
                yield return null;
            }

            // Reset
            lerp = 0;

            // Move out 
            while (lerp <= 1)
            {

                rect.localEulerAngles = Vector3.Lerp(Vector3.zero, -Vector3.forward * angle, tiltCurve.Evaluate(lerp));

                lerp += Time.deltaTime * tiltSpeed;
                yield return null;
            }

            // Reset
            lerp = 0;

            // Move in 
            while (lerp <= 1)
            {

                rect.localEulerAngles = Vector3.Lerp(-Vector3.forward * angle, Vector3.zero, tiltCurve.Evaluate(lerp));

                lerp += Time.deltaTime * tiltSpeed;
                yield return null;
            }
        }
    }
}
