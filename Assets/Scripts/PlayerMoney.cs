using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] PlayerDataObject playerData;
    [SerializeField] Animator moneyAnimator;

    private void Update()
    {
        string currentMoney= playerData.Money.ToString();
        if (!moneyText.text.Equals(currentMoney))
        {
            moneyAnimator.SetTrigger("bling");
            moneyText.text = currentMoney;
        }
    }
}
