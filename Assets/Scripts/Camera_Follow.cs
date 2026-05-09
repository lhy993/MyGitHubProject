using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // 따라갈 캐릭터
    [SerializeField] private float fixedY = 1.0f; // 고정 y값
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10); // x, y 오프셋 + z
    [SerializeField] private float smoothSpeed = 0.125f; // 부드러운 이동 속도

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            fixedY,
            offset.z
        );

        // x값 제한
        float clampedX = Mathf.Clamp(desiredPosition.x, -11f, 11f);
        desiredPosition.x = clampedX;

        // 부드럽게 이동
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed
        );

        transform.position = smoothedPosition;

    }
}