using System;
using Decal.Adapter;
using Decal.Adapter.Wrappers;

namespace CombatHud
{
    [WireUpBaseEvents]
    [FriendlyName("CombatHUD")]
    public class PluginCore : PluginBase
    {
        private Logger _logger;

        protected override void Startup()
        {
            _logger = new Logger();

            Core.WorldFilter.ChangeObject += WorldFilter_ChangeObject;
            Core.WorldFilter.CreateObject += WorldFilter_CreateObject;
            Core.WorldFilter.ReleaseObject += WorldFilter_ReleaseObject;
        }

        protected override void Shutdown()
        {
        }
        
        [BaseEvent("LoginComplete", "CharacterFilter")]
        private void CharacterFilter_LoginComplete(object sender, EventArgs e)
        {
            _logger.LogToChat("Initialized");

        }

        private void WorldFilter_CreateObject(object sender, CreateObjectEventArgs e)
        {
            if (IsNotMonsterObject(e.New.ObjectClass))
            {
                return;
            }

            var worldObjectCollection = Core.WorldFilter.GetByObjectClass(ObjectClass.Monster);
            _logger.LogToChat($"New monster created. Total Count: {worldObjectCollection.Count}");
        }

        private void WorldFilter_ReleaseObject(object sender, ReleaseObjectEventArgs e)
        {
            if (IsNotMonsterObject(e.Released.ObjectClass))
            {
                return;
            }

            var worldObjectCollection = Core.WorldFilter.GetByObjectClass(ObjectClass.Monster);
            _logger.LogToChat($"Monster deleted. Total Count: {worldObjectCollection.Count}");
        }

        private void WorldFilter_ChangeObject(object sender, ChangeObjectEventArgs e)
        {
            if (IsNotMonsterObject(e.Changed.ObjectClass))
            {
                return;
            }

            var worldObjectCollection = Core.WorldFilter.GetByObjectClass(ObjectClass.Monster);
            _logger.LogToChat($"Monster changed. Total Count: {worldObjectCollection.Count}");
        }

        private bool IsNotMonsterObject(ObjectClass objectClass)
        {
            return objectClass != ObjectClass.Monster;
        }
    }
}
