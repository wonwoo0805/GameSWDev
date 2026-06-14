using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public GameObject equipmentPanel; 

    public List<ItemSlot> inventory = new List<ItemSlot>();
    public List<ItemSlot> equipment = new List<ItemSlot>();

    public Player_St1 player;
    public int totalMoney = 100;
    public int totalWeight = 0;

    private void Start()
    {
        Debug.Log($"equipmentPanel: {equipmentPanel}");
        Debug.Log($"equipmentPanel name: {equipmentPanel?.name}");
    }

    public void InitializeData(int size)
    {
        inventory.Clear();
        for (int i = 0; i < size; i++)
            inventory.Add(new ItemSlot());
        equipment.Clear();
        for (int i = 0; i < 7; i++)
            equipment.Add(new ItemSlot());
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //to link to player character
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        player = FindAnyObjectByType<Player_St1>();
        player.weightBar.UpdateBar(totalWeight);
        //UpdateStat();
    }

    public int addItem(ItemData newItem)
    {
        //if player is in mainLobby, ignore weight
        if(SceneManager.GetActiveScene().buildIndex != 2)
        {
            Debug.Log("아이템 추가 함수 호출됨");
            if (totalWeight >= player.limitWeight)
            {
                Debug.Log("무게 초과로 아이템 획득 실패");
                return -1;
            }
            else if (totalWeight > player.limitWeight * 0.9)
            {
                Debug.Log("과중량!");
                //이동속도 감소
                player.runSpeed = 6f;
                player.walkSpeed = 3f;
            }
            else if (totalWeight > player.limitWeight * 0.7)
            {
                Debug.Log("무거움!");
                //이동속도 소폭 감소
                player.runSpeed = 8f;
                player.walkSpeed = 4f;
            }
            player.weightBar.UpdateBar(totalWeight);
        }
        
        //check all inventorySlots
        for (int i = 0; i < inventory.Count; i++)
        {
            //if can overlap item
            if (inventory[i].itemInSlot != null &&
                (int)inventory[i].itemInSlot.itemDataType >= (int)ItemType.Any &&
                inventory[i].itemInSlot == newItem)
            {
                inventory[i].itemInSlot = newItem;
                inventory[i].quantity += newItem.itemDataNum;
                return i;
            }
        }

        //check all inventorySlots
        for (int i = 0; i < inventory.Count; i++)
        {
            //if this slot is empty
            if (inventory[i].isEmpty)
            {
                inventory[i].itemInSlot = newItem;
                inventory[i].quantity += newItem.itemDataNum;
                totalWeight += newItem.itemDataWeight * newItem.itemDataNum;     
                return i;
            }
        }
        return -1;
    }

    public void exchangeItemData(SlotUI startSlot, SlotUI endSlot)
    {
        //get info about start/end itemData
        //List<ItemSlot> startList = inventory.Contains(startSlot.slotData) ? inventory : equipment;
        //List<ItemSlot> endList = inventory.Contains(endSlot.slotData) ? inventory : equipment;

        List<ItemSlot> startList = startSlot.isEquipment ? equipment : inventory;
        List<ItemSlot> endList = endSlot.isEquipment ? equipment : inventory;

        int startIdx = startSlot.slotIndex;
        int endIdx = endSlot.slotIndex;

        Debug.Log("check1");
        Debug.Log($"[Swap] 출발({(startSlot.isEquipment ? "장비" : "인벤")}): {startIdx}번 / 도착({(endSlot.isEquipment ? "장비" : "인벤")}): {endIdx}번");
        if (startIdx < 0 || endIdx < 0)
        {
            Debug.Log(startIdx);
            Debug.Log(endIdx);
            return;

        }
        
        Debug.Log("check3");
        //swapping
        ItemData tempItem = startList[startIdx].itemInSlot;
        startList[startIdx].itemInSlot = endList[endIdx].itemInSlot;
        endList[endIdx].itemInSlot = tempItem;

        
        
        
        
    }

    public void UpdateEquipment(SlotUI startSlot, SlotUI endSlot)
    {
        //if changed slot have stat
        if (startSlot.slotType == ItemType.Armor || startSlot.slotType == ItemType.Chip ||
        endSlot.slotType == ItemType.Armor || endSlot.slotType == ItemType.Chip)
        {
            Debug.Log("check4");
            UpdateStat();
        }
        else if (startSlot.slotType == ItemType.Weapon || endSlot.slotType == ItemType.Weapon)
        {
            Debug.Log("check5");
            player.currentWeapon.currentItem = player.currentWeapon.inventoryManager.GetEquippedItem(ItemType.Weapon);
            player.currentWeapon.wpm.ChangeItemPreview(player.currentWeapon.currentItem);
            player.currentWeapon.currentWeapon = (Weapons)player.currentWeapon.currentItem;
            player.currentWeapon.UpdateWeaponStat(player.currentWeapon.currentWeapon);
        }
    }
            

    //update all statBonus
    public void UpdateStat()
    {
        Debug.Log($"UpdateStat - equipmentPanel: {equipmentPanel}");
        //clear all bonus
        player.hpBonus = 0;
        player.staminaBonus = 0;
        player.damageBonus = 0;
        player.maxAmmoBonus = 0;
        player.fireRateBonus = 0;
        player.reloadBonus = 0;
        player.weightBonus = 0;
        player.attackBonus = 0;
        player.attackPercentBonus = 0;

        SlotUI[] eqptSlots = equipmentPanel.GetComponentsInChildren<SlotUI>();
        //circuit all equipItems
        foreach (SlotUI slot in eqptSlots)
        {
            //check stats each itemType has
            if (slot.slotData.isEmpty) continue;
            ItemData item = slot.slotData.itemInSlot;

            if (item.itemDataType == ItemType.Armor)
            {
                Armors armor = (Armors)item;
                player.hpBonus += armor.HPBonus;
            }
            else if (item.itemDataType == ItemType.Chip)
            {
                Chips chip = (Chips)item;
                player.hpBonus += chip.Hpbonus;
                player.staminaBonus += chip.StaminaBonus;
                player.damageBonus += chip.DamageBonus;
                player.maxAmmoBonus += chip.MaxammoBonus;
                player.fireRateBonus += chip.FirerateBonus;
                player.reloadBonus += chip.ReloadBonus;
                player.weightBonus += chip.WeightBonus;
                player.attackBonus += chip.AttackBonus;
                player.attackPercentBonus += chip.AttackPercentBonus;
            }
        }

        player.playerMaxHealth = 150f + player.hpBonus;
        player.maxStamina = 100f + player.staminaBonus;
        player.limitWeight = 30f + player.weightBonus;
    }
}