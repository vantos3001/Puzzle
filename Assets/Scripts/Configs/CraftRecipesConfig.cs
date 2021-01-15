using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CraftRecipesConfig", menuName = "Configs/CraftRecipesConfig", order = 51)]
public class CraftRecipesConfig : ScriptableObject
{
    public List<CraftAllItemRecipesData> CraftRecipes = new List<CraftAllItemRecipesData>();
}
