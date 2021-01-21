using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialStep
{
    None,
    SelectInventoryItem,
    PutItemToCell
    
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
            
            _isTutorial = !SaveManager.GetBool(SaveManager.IS_TUTORIAL_FINISHED_SAVE_KEY);
            _tutorialConfig = DataManager.TutorialConfig;
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
        ShowTutorialHint(startHintPosition, endHintPosition);
    }
    
    

    private static void ShowTutorialHint(Vector3 startPosition, Vector3 endPosition)
    {
        var tutorialMovements = new List<TutorialHintMovement>();
        
        tutorialMovements.Add(new TutorialHintMovement()
        {
            From = startPosition,
            To = endPosition,
            Speed = 300f
        });
        
        tutorialMovements.Add(new TutorialHintMovement()
        {
            From = endPosition,
            To = startPosition,
            Speed = 300f
        });
        
        _tutorialHint.Show(tutorialMovements);
    }
    
    private static void DoPutItemToCellStep()
    {
        
    }

    private static void FinishTutorial()
    {
        _isTutorial = false;
        SaveManager.SaveBool(SaveManager.IS_TUTORIAL_FINISHED_SAVE_KEY, true);
        _tutorialHint.Hide();
    }
}
