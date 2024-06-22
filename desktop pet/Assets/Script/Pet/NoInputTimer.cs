using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(NoInputEvent))]
public class NoInputTimer : MonoBehaviour
{
    [SerializeField] float noInputThreshold = 30f;
    private NoInputEvent noInputEvent;
    private Vector2 pastMouseWorldPosition;
    private float noInputTimeCount;
    bool isNoInput = true;

    private void Awake()
    {
        noInputEvent = GetComponent<NoInputEvent>();

        noInputTimeCount = 0;
    }

    private void Update()
    {
        noInputTimeCount += Time.deltaTime;

        Vector2 currentMouseWorldPosition = HelperUtils.GetMouseWorldPosition();

        if(currentMouseWorldPosition != pastMouseWorldPosition)
        {
            noInputTimeCount = 0;
            pastMouseWorldPosition = currentMouseWorldPosition;
        }

        if (noInputTimeCount < noInputThreshold)
        {
            if (isNoInput)
            {
                isNoInput = false;
                noInputEvent.CallNoInput(isNoInput);
            }

            return;
        }

        if (!isNoInput)
        {
            isNoInput = true;
            noInputEvent.CallNoInput(isNoInput);
        }
    }

}
