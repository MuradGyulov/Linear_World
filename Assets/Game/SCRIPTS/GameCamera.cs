using UnityEngine;
using YG;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private float cameraFolowSpeed;
    [Space(14)]
    [SerializeField] private Vector3 cameraOffcet;

    private GameObject target;

    private void Start()
    {
        Camera cameraComponent = GetComponent<Camera>();
        cameraComponent.orthographicSize = YandexGame.savesData.cameraSize;

        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.transform.position + cameraOffcet;
        Vector3 smothSpeed = Vector3.Lerp(transform.position, desiredPosition, cameraFolowSpeed);
        transform.position = smothSpeed;
    }
}
