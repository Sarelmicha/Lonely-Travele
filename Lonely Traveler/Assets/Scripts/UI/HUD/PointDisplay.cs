using UnityEngine;

public class PointDisplay : MonoBehaviour
{
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
   }
}
