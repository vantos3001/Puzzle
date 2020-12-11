using System;
using System.Collections.Generic;

public class CraftRecipeData
{
    public string From;
    public string To;
    public string Result;
}

public static class CraftManager
{
    private static List<CraftRecipeData> _craftRecipeDates = new List<CraftRecipeData>();
    
    public static string Craft(string from, string to)
    {
        var recipe = _craftRecipeDates.Find(craft => craft.From == from && craft.To == to);

        if (recipe != null)
        {
            return recipe.Result;
        }
        else
        {
            //TODO: return red not place
        }
        
        throw new NotImplementedException();
    }
}
