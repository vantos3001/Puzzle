using System;
using System.Collections.Generic;
using UnityEngine;

public enum CraftResultType
{
    RemoveItem = 0,
    PutItem = 1,
}

[Serializable]
public class CraftResultData
{
    public CraftResultType Type;
}

[Serializable]
public class CraftAllItemRecipesData
{
    public ItemType From;
    public List<CraftToItemResultPair> ToItemResultPairs;
}

[Serializable]
public class CraftToItemResultPair
{
    public ItemType To;
    public CraftResultData ResultData;
}

public class CraftRecipe
{
    public ItemType From;
    public ItemType To;
    public CraftResultData ResultData;
}

public static class CraftManager
{
    private const string CRAFT_RECIPES_CONFIG_NAME = "DefaultCraftRecipesConfig";
    
    private static List<CraftAllItemRecipesData> _allRecipes = new List<CraftAllItemRecipesData>();

    private static bool _isInit;

    public static void Init()
    {
        if (!_isInit)
        {
            _isInit = true;
            _allRecipes = DataManager.LoadCraftRecipesConfig(CRAFT_RECIPES_CONFIG_NAME).CraftRecipes;
        }
    }
    
    public static bool TryGetCraftRecipe(ItemType from, ItemType to, out CraftRecipe recipe)
    {

        recipe = GetRecipe(from, to);

        if (recipe != null)
        {
            return true;
        }

        return false;
    }

    private static CraftRecipe GetRecipe(ItemType from, ItemType to)
    {
        CraftRecipe recipe = null;
        
        //TODO: instead list use dictionary. On init parse list to dictionary. Or parse config to dictionary
        CraftAllItemRecipesData allItemRecipes = _allRecipes.Find(craft => craft.From == from);
        if (allItemRecipes != null)
        {
            var craftToItemResultPair = allItemRecipes.ToItemResultPairs.Find(pair => pair.To == to);

            if (craftToItemResultPair != null)
            {
                recipe = CreateRecipe(from, craftToItemResultPair);
            }
        }

        return recipe;
    }

    private static CraftRecipe CreateRecipe(ItemType from, CraftToItemResultPair pair)
    {
        return new CraftRecipe
        {
            From = from,
            To = pair.To,
            ResultData = pair.ResultData
        };
    }

    public static void DoCraft(CraftRecipe recipe, ItemData fromItem, Cell toCell)
    {
        switch (recipe.ResultData.Type)
        {
            case CraftResultType.RemoveItem:
                RemoveItem(toCell);
                break;
            case CraftResultType.PutItem:
                PutItem(toCell, fromItem);
                break;
            default:
                Debug.LogError("Not found CraftResultType = " + recipe.ResultData.Type);
                break;
        }
    }

    private static void RemoveItem(Cell toCell)
    {
        toCell.RemoveItem();
    }

    private static void PutItem(Cell cell, ItemData data)
    {
        cell.SetItem(data, false);
    }
}
