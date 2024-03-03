using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheel : MonoBehaviour
{
    [SerializeField] private Vector2Int _turnsRange;
    [SerializeField] private float _rotatingTime;
    [SerializeField] private float _counterSmooth;
    [Header("Text")]
    [SerializeField] TextMeshProUGUI _WinBalance;
    [SerializeField] GameObject _bigWinAnimation;

    [Space]

    [SerializeField] private Transform _wheelTransform;
    [SerializeField] private Button _rotateButton;
    [SerializeField] private TMP_Text _counter;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GlobalVariable globalVariable;

    private int _counterValue;
    private bool _rotating;
    private int _result; // Add this field to store the result
    
    private bool _guiActived {
        get { return _bigWinAnimation.activeSelf; }
        set { }
    }

    private void Awake()
    {
        _rotateButton.onClick.AddListener(TryRotate);
        _bigWinAnimation.SetActive(false);
    }

    private void TryRotate()
    {
        if (_rotating == false && menuManager.TryDeductCoins(50000)) // Check if the player has enough coins to spin
        {
            RotateWheel();
        }
    }

    private void RotateWheel()
    {
        _rotating = true;

        int turnsCount = UnityEngine.Random.Range(_turnsRange.x, _turnsRange.y);

        float rawAngle = 45 * UnityEngine.Random.Range(1, 9);
        float result = rawAngle;

        rawAngle -= UnityEngine.Random.Range(0.1f, 44.9f) + _wheelTransform.eulerAngles.z;

        float rotatingAngle = rawAngle + 360 * turnsCount;

        _wheelTransform.DORotate(Vector3.forward * rotatingAngle, _rotatingTime, RotateMode.WorldAxisAdd).onComplete += () =>
        {
            _rotating = false;
            _result = GetWinValue(result); // Calculate the result

            // Add the result to the player's balance
            menuManager.AddWin(_result);
            _WinBalance.text = _result.ToString("$0.00");
            _bigWinAnimation.SetActive(true);

            // Update the counter
            AddResult(_result);
        };
    
    }
    private void AddResult(int value)
    {
        _counterValue += value;
        StartCoroutine(SmoothCounter(_counterValue, _counterValue - value));
    }


    private IEnumerator SmoothCounter(int endValue, int startValue)
    {
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime / _counterSmooth;

            _counter.text = Convert.ToInt32(Mathf.Lerp(startValue, endValue, time)).ToString();

            yield return new WaitForEndOfFrame();
        }

        _counter.text = endValue.ToString();
    }

    private void PrintWinByAngle(float angle)
    {
        print(GetWinValue(angle));
    }

    private int GetWinValue(float angle)
    {
        switch (angle)
        {
            case 45:
                return 10000;

            case 90:
                return 62500;

            case 135:
                return 25000;

            case 180:
                return 75000;

            case 225:
                return 50000;

            case 270:
                return 87500;

            case 315:
                return 37500;

            case 360:
                return 12500;

            default:
                return 0; // Set the default case to 0 or another appropriate value.
        }
    }
}
