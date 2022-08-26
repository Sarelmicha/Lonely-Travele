using HappyFlow.LonelyTraveler.Player;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World
{
  /// <summary>
  /// This class responsible for handling the special ability. This is an abstract class and can be inherit by new special abilities using the InvokeAbility method.
  /// </summary>
  public abstract class TriggerAbility : MonoBehaviour
  {
    private void OnTriggerEnter2D(Collider2D col)
    {
      PlayerController m_PlayerController;

      if (IsPlayerCollide(col, out m_PlayerController))
      {
        OnPlayerTriggerEnter2D(m_PlayerController);
      }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
      PlayerController m_PlayerController;

      if (IsPlayerCollide(col, out m_PlayerController))
      {
        OnPlayerTriggerExit2D(m_PlayerController);
      }
    }
       
    private void OnTriggerStay2D(Collider2D col)
    {
      PlayerController m_PlayerController;

      if (IsPlayerCollide(col, out m_PlayerController))
      {
        OnPlayerTriggerStay2D(m_PlayerController);
      }
    }

    protected virtual void OnPlayerTriggerEnter2D(PlayerController playerController) {}
    protected virtual void OnPlayerTriggerExit2D(PlayerController playerController) {}
    protected virtual void OnPlayerTriggerStay2D(PlayerController playerController) {}
    
    private bool IsPlayerCollide(Collider2D col, out PlayerController playerController)
    { 
      playerController = col.gameObject.GetComponent<PlayerController>();
      return playerController;
    }
  }
}
