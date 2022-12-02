namespace HappyFlow.LonelyTraveler.UI
{
    /// <summary>
    /// Define the functionality of any button of the game.
    /// </summary>
    public interface IButton
    {
        /// <summary>
        /// Call when the user clicks the button
        /// </summary>
        void OnClick();
        
        /// <summary>
        /// The command to execute when the button is clicked.
        /// </summary>
        ICommand Command { get; set; }
    }
}

