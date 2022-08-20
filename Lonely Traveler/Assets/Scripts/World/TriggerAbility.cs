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
      var player = col.GetComponent<PlayerController>();

      if (!player)
      {
        return;
      }
      
      OnPlayerTriggerEnter2D(player);
    }
    
    protected abstract void OnPlayerTriggerEnter2D(PlayerController playerController);
  }
}
