using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public static MoneyController Instance;
 
    public float countDuration;
    private Coroutine countCR;
    public float money;
    private float winningMoney = 0;
    private float targetMoney;
    public TextMeshProUGUI spinnerMoneyText,mainMoneyText,winningMoneyText;
    private int multiplieBonus;
    private void Awake()
    {
        Instance = this;
    }

    public void SetMainMoney()
    {
        money += winningMoney;
        SetMoneyTexts();
    }

    public void ResetWin()
    {
        money += winningMoney;
        winningMoney = 0;
        targetMoney = 0;
        SetMoneyTexts();
        winningMoneyText.text = winningMoney.ToString();
    }
    
    private void SetMoneyTexts()
    {
        spinnerMoneyText.text = FormatNumber(money);
        mainMoneyText.text = FormatNumber(money);
    }

    private void Start()
    {
        multiplieBonus = 0;
        money = PlayerPrefs.GetFloat("PlayerMoney", 1000);
        targetMoney = money;
        winningMoney = 0;
        SetMoneyTexts();
    }

    public bool HaveMoney(float amount)
    {
        return money >= amount;
    }

    public void SpendMoney(float amount)
    {
        money -= amount;
        mainMoneyText.text = FormatNumber(money);
    }
    
    public void EarnMoney(int amount)
    {
        targetMoney += amount/5;
        if(countCR != null)
            StopCoroutine(countCR);
        countCR = StartCoroutine(CountTo(targetMoney));
    }

    public void AddMultiply(int amount)
    {
        Debug.Log("AddMultiply");
        multiplieBonus += amount;
        winningMoneyText.text = winningMoney + " " + "x" + " " + multiplieBonus ;
        winningMoneyText.transform.GetComponent<DOTweenAnimation>().DOPlay();
    }

    public void MultiplyMoney()
    {
        StartCoroutine(MultiplyDelay());
    }

    IEnumerator MultiplyDelay()
    {
        yield return new WaitForSeconds(0.5f);
        float originalWinningMoney = winningMoney; // сохраняем текущий выигрыш
        targetMoney *= multiplieBonus;
        winningMoney *= multiplieBonus; // добавьте эту строку
        winningMoneyText.text = winningMoney.ToString();

        if (countCR != null)
            StopCoroutine(countCR);

        countCR = StartCoroutine(CountTo(targetMoney));

        winningMoneyText.transform.DOScale(0.8f, countDuration).OnComplete(() => winningMoneyText.transform.DOScale(0.5853804f, 0.2f));
        multiplieBonus = 0;
    }

    IEnumerator CountTo(float targetAmount)
    {
        var rate = Mathf.Abs(targetAmount - winningMoney) / countDuration;
        while (winningMoney != targetAmount)
        {
            winningMoney = Mathf.MoveTowards(winningMoney, targetAmount, rate * Time.deltaTime);
            winningMoneyText.text = ((int)winningMoney).ToString();
            yield return null;
        }
    }

    IEnumerator CountDown(float targetAmount)
    {
        var rate = Mathf.Abs(targetAmount - money) / countDuration;
        while (money != targetAmount)
        {
            money = Mathf.MoveTowards(money, targetAmount, rate * Time.deltaTime);
            mainMoneyText.text = ((int)money).ToString();
            yield return null;
        }
    }
    
    public string FormatNumber(float num)
    {
        if (num >= 100000000) {
            return (num / 1000000D).ToString("0.#M");
        }
        if (num >= 1000000) {
            return (num / 1000000D).ToString("0.##M");
        }
        if (num >= 100000) {
            return (num / 1000D).ToString("0.#K");
        }
        if (num >= 10000) {
            return (num / 1000D).ToString("0.##K");
        }
        return num.ToString("#,0");
    }
    
    private void OnDestroy()
    {
        // Сохраняем количество монет в PlayerPrefs при завершении игры
        PlayerPrefs.SetFloat("PlayerMoney", money);
        PlayerPrefs.Save();
    }
    
}
