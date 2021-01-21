using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TutorialConfig", menuName = "Configs/TutorialConfig", order = 51)]
public class TutorialConfig : ScriptableObject
{
    public int TargetInventoryItemIndex;
    public PointData TargetCellCoords;
}
