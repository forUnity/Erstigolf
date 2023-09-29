using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GolfkartSpeedUI : MonoBehaviour
{
    [SerializeField] private GolfkartSpeed golfkartSpeed;
    [SerializeField] private TeleportSystem teleportSystem;
    [SerializeField] private TextMeshProUGUI SpeedTM;
    [SerializeField] private RectTransform speedNeedle;
    [SerializeField] private RectTransform flySpeedIndicator;
    [SerializeField] private RectTransform flySpeedFill;
    [SerializeField] private int MaxDisplaySpeed;
    [SerializeField] private float MaxNeedleRotationOffset;
    [SerializeField] private float lerpSpeed = 1f;
    //[SerializeField] private TextMeshProUGUI flightReadyTM;

    float startNeedleRotation;
    private void Start()
    {
        startNeedleRotation = speedNeedle.rotation.eulerAngles.z;
        currentRotation = startNeedleRotation;

        float flySpeedDeg = MaxNeedleRotationOffset * Mathf.Clamp01((float)teleportSystem.speedToFly / MaxDisplaySpeed);
        flySpeedIndicator.rotation = Quaternion.Euler(0f, 0f, startNeedleRotation + flySpeedDeg);
        flySpeedFill.rotation = Quaternion.Euler(0f, 0f, startNeedleRotation + flySpeedDeg - 180f);

        float flySpeed = Mathf.Clamp01((float)teleportSystem.speedToFly / MaxDisplaySpeed);
        flySpeedFill.GetComponent<Image>().fillAmount = ((1f - flySpeed) * Mathf.Abs(MaxNeedleRotationOffset)) / 360f;
    }

    private float currentRotation;
    private void Update()
    {
        float speed = golfkartSpeed.GetAvgSpeedOverTimeRecord();
        SpeedTM.text = ((int)speed).ToString() + " m/s";

        float speedDegrees = MaxNeedleRotationOffset * Mathf.Clamp01(speed / MaxDisplaySpeed);
        currentRotation = lerpSpeed == 0f ? startNeedleRotation + speedDegrees : Mathf.Lerp(currentRotation, startNeedleRotation + speedDegrees, Time.deltaTime * lerpSpeed);
        speedNeedle.rotation = Quaternion.Euler(0f, 0f,currentRotation);

    }
}
