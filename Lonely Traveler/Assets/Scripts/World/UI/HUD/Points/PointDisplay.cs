using UnityEngine;

public class PointDisplay : MonoBehaviour
{
   [SerializeField] private Animator m_Animator;
   private PointDisplayLogic m_PointDisplayLogic;
   public bool IsFull => m_PointDisplayLogic.IsFull;

   private void Awake()
   {
      m_PointDisplayLogic = new PointDisplayLogic();
   }

   /// <summary>
   /// Handle the fill process of the point.
   /// </summary>
   public void Fill()
   {
       // Do some fulling animation
         m_PointDisplayLogic.IsFull = true;
         m_Animator.Play("Collect");
   }
   
   /// <summary>
   /// Reset the point display.
   /// </summary>
   public void Reset()
   {
      m_PointDisplayLogic.IsFull = false;
      m_Animator.Play("Idle");
   }
}
