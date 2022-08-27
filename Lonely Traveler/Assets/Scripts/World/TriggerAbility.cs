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
    
    private bool IsPlayerCollide(Collider2D col, out PlayerController playerController)
    { 
      playerController = col.gameObject.GetComponent<PlayerController>();
      return playerController;
    }
  }
}
