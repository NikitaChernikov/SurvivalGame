using UnityEngine;
using UnityEngine.UI;

public class MonsterDetectionVisual : MonoBehaviour
{
    [SerializeField] private Image _detectorFillImage;

    [SerializeField] private MonsterDetector _monsterDetector;

    private void OnEnable()
    {
        _monsterDetector.OnChangeImage += MonsterDetector_OnChangeImage;
    }

    private void OnDisable()
    {
        _monsterDetector.OnChangeImage -= MonsterDetector_OnChangeImage;
    }

    private void MonsterDetector_OnChangeImage(float distanceInPercent)
    {
        _detectorFillImage.fillAmount = distanceInPercent;
    }
}
