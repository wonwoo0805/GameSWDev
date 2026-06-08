using TMPro;
using UnityEngine;

public class ItemExplainPanel : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;

    //Show item's description
    public void ShowDescription(ItemData item)
    {
        if (item == null)
        {
            descriptionText.text = "";
            return;
        }
        descriptionText.text = item.itemDataDescription;

        return;
    }
}