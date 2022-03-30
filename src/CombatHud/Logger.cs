using Decal.Adapter;

namespace CombatHud
{
    internal class Logger
    {
        public void LogToChat(string message)
        {
            CoreManager.Current.Actions.AddChatText($"CombatHUD: {message}", 5);
        }
    }
}
