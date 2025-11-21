using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class OwnedItemsData
{
    private const string PlayerPrefsKey = "OWNNED_ITEMS_DATA";
    public static OwnedItemsData Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = PlayerPrefs.HasKey(PlayerPrefsKey)
                    ? JsonUtility.FromJson<OwnedItemsData>(PlayerPrefs.GetString(PlayerPrefsKey))
                    : new OwnedItemsData();
            }
            return _instance;
        }
    }
    private static OwnedItemsData _instance;

    public OwnedItem[] OwnedItems
    {
        get { return ownedItems.ToArray(); }
    }
    [SerializeField] private List<OwnedItem> ownedItems = new List<OwnedItem>();

    private OwnedItemsData()
    {

    }
    public void Save()
    {
        var jsonString = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(PlayerPrefsKey, jsonString);
        PlayerPrefs.Save();
    }

    public void Add(Item.ItemType type, int number = 1)
    {
        var item = GetItem(type);
        if (null == item || item.Number < number)
        {
            item = new OwnedItem(type);
            ownedItems.Add(item);
        }
        item.Add(number);
    }
    public void Use(Item.ItemType type, int numder = 1)
    {
        var item = GetItem(type);
        if (null == item || item.Number < numder)
        {
            throw new Exception("ƒAƒCƒeƒ€‚ª‘«‚è‚Ü‚¹‚ñ");
        }
        item.Use(numder);
    }
    public OwnedItem GetItem(Item.ItemType type)
    {
        return ownedItems.FirstOrDefault(x => x.Type == type);
    }
    [Serializable]
    public class OwnedItem
    {
        public Item.ItemType Type
        {
            get { return type; }
        }

        public int Number
        {
            get { return number; }
        }
        [SerializeField] private Item.ItemType type;
        [SerializeField] private int number;

        public OwnedItem(Item.ItemType type)
        {
            this.type = type;
        }
        public void Add(int number = 1)
        {
            this.number += number;
        }
        public void Use(int numder = 1)
        {
            this.number -= number;
        }

    }
}

