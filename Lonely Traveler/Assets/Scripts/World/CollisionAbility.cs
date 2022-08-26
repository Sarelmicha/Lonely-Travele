using HappyFlow.LonelyTraveler.Player;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World
{
    /// <summary>
    /// This class responsible for handling the special ability. This is an abstract class and can be inherit by new special abilities using the InvokeAbility method.
    /// </summary>
    public abstract class CollisionAbility : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            PlayerController m_PlayerController;

            if (IsPlayerCollide(col, out m_PlayerController))
            {
                OnPlayerCollisionEnter2D(m_PlayerController);
            }
        }
        
       private void OnCollisionExit2D(Collision2D col)
       {
           PlayerController m_PlayerController;

           if (IsPlayerCollide(col, out m_PlayerController))
           {
               OnPlayerCollisionExist2D(m_PlayerController);
           }
       }
       
       private void OnCollisionStay2D(Collision2D col)
       {
           PlayerController m_PlayerController;

           if (IsPlayerCollide(col, out m_PlayerController))
           {
               OnPlayerCollisionStay2D(m_PlayerController);
           }
       }

       protected virtual void OnPlayerCollisionEnter2D(PlayerController playerController) {}
       protected virtual void OnPlayerCollisionExist2D(PlayerController playerController) {}
       protected virtual void OnPlayerCollisionStay2D(PlayerController playerController) {}

       private bool IsPlayerCollide(Collision2D col, out PlayerController playerController)
       { 
           playerController = col.gameObject.GetComponent<PlayerController>();
           return playerController;
       }
    }
}