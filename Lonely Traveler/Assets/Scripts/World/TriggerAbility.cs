using System;
using HappyFlow.LonelyTraveler.Player;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World
{
  /// <summary>
  /// This class responsible for handling the special ability. This is an abstract class and can be inherit by new special abilities using the InvokeAbility method.
  /// </summary>
  public abstract class TriggerAbility : MonoBehaviour
  {
    protected PlayerController m_PlayerController;
    protected bool m_IsPlayerInsideCollider;
    protected LevelManager m_LevelManager;
    
    private void Start()
    {
      var go = GameObject.FindGameObjectWithTag("LevelManager");

      if (go == null)
      {
        Debug.Log("The level manager has not been found on scene.");
        return;
      }
      
      m_LevelManager = go.GetComponent<LevelManager>();
      m_LevelManager.OnLevelShouldRestart += Reset;
    }

    private void OnDestroy()
    {
      m_LevelManager.OnLevelShouldRestart -= Reset;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
      if (IsPlayerCollide(col, out m_PlayerController))
      {
        m_IsPlayerInsideCollider = true;
        OnPlayerTriggerEnter2D(m_PlayerController);
      }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
      if (IsPlayerCollide(col, out m_PlayerController))
      {
        m_IsPlayerInsideCollider = false;
        OnPlayerTriggerExit2D(m_PlayerController);
      }
    }
    
    protected virtual void OnPlayerTriggerEnter2D(PlayerController playerController) {}
    protected virtual void OnPlayerTriggerExit2D(PlayerController playerController) {}

    protected virtual void Reset(bool shouldFullReset) {}

    private bool IsPlayerCollide(Collider2D col, out PlayerController playerController)
    { 
      playerController = col.gameObject.GetComponent<PlayerController>();
      return playerController;
    }
  }
}
