using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialStep
{
    None,
    SelectInventoryItem,
    PutItemToCell,
    FinishTutorial
    
}
public static class TutorialManager
{
    private static bool _isTutorial;
    public static bool IsTutorial => _isTutorial;

    private static TutorialStep _currentStep = TutorialStep.None;

    private static bool _isInit;

    private static TutorialConfig _tutorialConfig;

    private static UIController _uiController;

    private static UITutorialHint _tutorialHint;
    
    public static void Init()
    {
        if (!_isInit)
        {
            _isInit = true;

            if (!DataManager.GameSettingsConfig.IsTestLevel)
            {
                _isTutorial = !SaveManager.GetBool(SaveManager.IS_TUTORIAL_FINISHED_SAVE_KEY);
                _tutorialConfig = DataManager.TutorialConfig; 
            }
        }
    }
    
    public static IEnumerator StartTutorial(UIController uiController)
    {
        _uiController = uiController;
        _tutorialHint = _uiController.TutorialHint;
        
        yield return new WaitForEndOfFrame();
        
        ChangeState(TutorialStep.SelectInventoryItem);
    }

    private static void ChangeState(TutorialStep tutorialStep)
    {
        if(_currentStep == tutorialStep) {return;}
        
        _currentStep = tutorialStep;

        switch (_currentStep)
        {
            case TutorialStep.SelectInventoryItem:
                DoSelectInventoryItemStep();
                break;
            case TutorialStep.PutItemToCell:
                DoPutItemToCellStep();
                break;
            case TutorialStep.FinishTutorial:
                DoFinishTutorial();
                break;
            default:
                Debug.LogError("Not found TutorialStep = " + tutorialStep);
                break;
        }
    }

    private static void DoSelectInventoryItemStep()
    {
        var itemButton = _uiController.ItemButtons.GetItemButton(_tutorialConfig.TargetInventoryItemIndex);
        var itemButtonRectTransform = itemButton.transform as RectTransform;
        
        var tutorialHintRectTransform = _tutorialHint.transform as RectTransform;

        var startHintPosition = itemButtonRectTransform.position + new Vector3(0f, itemButtonRectTransform.rect.size.y * itemButtonRectTransform.lossyScale.y, 0f);
        var endHintPosition = itemButtonRectTransform.position + new Vector3(0f,tutorialHintRectTransform.rect.size.y / 2 * tutorialHintRectTransform.lossyScale.y, 0f);
        
        var tutorialMovements = new List<TutorialHintMovement>();
        
        tutorialMovements.Add(new TutorialHintMovement()
        {
            From = startHintPosition,
            To = endHintPosition,
            Speed = 300f
        });
        
        tutorialMovements.Add(new TutorialHintMovement()
        {
            From = endHintPosition,
            To = startHintPosition,
            Speed = 300f
        });
        
        _tutorialHint.Show(tutorialMovements);

        EventManager.OnInventoryItemMoveStarted += OnInventoryItemMoveStarted;
    }

    private static void OnInventoryItemMoveStarted(IDraggable draggable)
    {
        EventManager.OnInventoryItemMoveStarted -= OnInventoryItemMoveStarted;
        ChangeState(TutorialStep.PutItemToCell);
    }
    
    private static void DoPutItemToCellStep()
    {
        var itemButton = _uiController.ItemButtons.GetItemButton(_tutorialConfig.TargetInventoryItemIndex);
        var itemButtonRectTransform = itemButton.transform as RectTransform;
        
        var tutorialHintRectTransform = _tutorialHint.transform as RectTransform;

        var startHintPosition = itemButtonRectTransform.position + new Vector3(0f,tutorialHintRectTransform.rect.size.y / 2 * tutorialHintRectTransform.lossyScale.y, 0f);

        var targetCell = GetTargetTutorialCell();
        var endHintPosition = Camera.main.WorldToScreenPoint(targetCell.transform.position) + new Vector3(0f,tutorialHintRectTransform.rect.size.y / 2 * tutorialHintRectTransform.lossyScale.y, 0f);
        
        var tutorialMovements = new List<TutorialHintMovement>();
        
        tutorialMovements.Add(new TutorialHintMovement()
        {
            From = startHintPosition,
            To = endHintPosition,
            Speed = 1000f,
            WaitTime = 0.9f
        });
        
        _tutorialHint.Show(tutorialMovements);
        
        EventManager.OnInventoryItemMoveEnded += OnInventoryItemMoveEnded;
    }
    
    private static void OnInventoryItemMoveEnded(IDroppable droppable)
    {
        EventManager.OnInventoryItemMoveEnded -= OnInventoryItemMoveEnded;
        
        var droppableCell = droppable as Cell;
        
        var targetCell = GetTargetTutorialCell();
        
        if (targetCell == droppableCell)
        {
            ChangeState(TutorialStep.FinishTutorial);
        }
        else
        {
            ChangeState(TutorialStep.SelectInventoryItem);
        }
    }

    public static bool IsTutorialCell(Cell cell)
    {
        return cell == GetTargetTutorialCell();
    }

    private static Cell GetTargetTutorialCell()
    {
        return LevelManager.CurrentLevel.Field.GetCell(_tutorialConfig.TargetCellCoords);
    }

    private static void DoFinishTutorial()
    {
        _isTutorial = false;
        SaveManager.SaveBool(SaveManager.IS_TUTORIAL_FINISHED_SAVE_KEY, true);
        _tutorialHint?.Hide();
    }
}
