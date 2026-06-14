using TMPro;
using UnityEngine;

public class ItemExplainPanel : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;            // 상세 창 (켜고 끄는 대상 = 자식)
    public TextMeshProUGUI nameText;    // 아이템 이름
    public TextMeshProUGUI flavorText;  // 플레이버 텍스트
    public TextMeshProUGUI statText;    // 스탯 정보
    public TextMeshProUGUI moneyText;    // 판매 금액 정보
    public TextMeshProUGUI rarityText;    // 희귀도 정보

    void Start()
    {
        if (panel != null) panel.SetActive(false);
    }

    public void ShowDescription(ItemData item)
    {
        if (item == null) { if (panel != null) panel.SetActive(false); return; }

        if (panel != null) panel.SetActive(true);
        nameText.text = item.name;
        flavorText.text = item.itemDataDescription;
        statText.text = BuildStatText(item);
        moneyText.text = $"무게 {item.itemDataWeight}kg\n구매 {item.ItemDataPurchaseMoney}G / 판매 {item.ItemDataSellMoney}G";
        rarityText.text = item.itemDataRarity.ToString();
    }

    // 아이템 종류별 스탯 문자열
    private string BuildStatText(ItemData item)
    {
        if (item is Weapons w)
            return $"공격력 {w.damage}\n탄창 {w.maxAmmo}\n연사력 {w.fireRate}\n장전 속도 {w.reloadSpeed}";

        if (item is Armors a)
            return $"체력 +{a.HPBonus}";

        if (item is Chips c)
        {
            string s = "";
            if (c.Hpbonus != 0) s += $"체력 +{c.Hpbonus} ";
            if (c.StaminaBonus != 0) s += $"스태미나 +{c.StaminaBonus} ";
            if (c.AttackBonus != 0) s += $"총기 공격력 +{c.AttackBonus} ";
            if (c.AttackPercentBonus != 0) s += $"공격력 +{c.AttackPercentBonus}% ";
            if (c.DamageBonus != 0) s += $"데미지 +{c.DamageBonus}% ";
            if (c.MaxammoBonus != 0) s += $"최대 탄약 +{c.MaxammoBonus} ";
            if (c.FirerateBonus != 0) s += $"연사력 +{c.FirerateBonus*100}% ";
            if (c.ReloadBonus != 0) s += $"장전 +{c.ReloadBonus*100}% ";
            if (c.WeightBonus != 0) s += $"최대 무게 +{c.WeightBonus} ";
            return s;
        }

        if (item is Uses u)
            return $"회복량 {u.Heal}";

        return "";   // Refunds 등 스탯 없는 것
    }
}