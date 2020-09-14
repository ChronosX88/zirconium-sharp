using System.Collections.Generic;
using Newtonsoft.Json;
using Zirconium.Core.Models;
using Zirconium.Core.Plugins.Interfaces;

namespace Zirconium.Core.Plugins
{
    public class PluginHostAPI : IPluginHostAPI
    {
        private App _app;
        private Router _router;

        public PluginHostAPI(App app, Router router)
        {
            _router = router;
            _app = app;
        }

        public IExposedSessionManager GetSessionManager() {
            return _app.SessionManager;
        }

        public void ProvideAuth(IAuthProvider provider) {
            _app.AuthManager.AddAuthProvider(provider);
        }

        public void FireEvent(CoreEvent coreEvent)
        {
            _router.RouteCoreEvent(coreEvent);
        }

        public string GenerateAuthToken(string entityID, string deviceID, int tokenExpirationMillis)
        {
            return _app.AuthManager.CreateToken(entityID, deviceID, tokenExpirationMillis);
        }

        public string[] GetServerDomains()
        {
            return _app.Config.ServerDomains;
        }

        public string GetServerID()
        {
            return _app.Config.ServerID;
        }

        public dynamic GetSettings(IPluginAPI plugin)
        {
            return _app.Config.Plugins.GetValueOrDefault(plugin.GetPluginUniqueName(), null);
        }

        public dynamic GetSettings(string pluginName)
        {
            return _app.Config.Plugins.GetValueOrDefault(pluginName, null);
        }

        public void Hook(IC2SMessageHandler handler)
        {
            _router.AddC2SHandler(handler.GetHandlingMessageType(), handler);
        }

        public void HookCoreEvent(ICoreEventHandler handler)
        {
            _router.AddCoreEventHandler(handler.GetHandlingEventType(), handler);
        }

        public void SendMessage(Session session, BaseMessage message)
        {
            session.ConnectionHandler.SendMessage(JsonConvert.SerializeObject(message));
        }

        public void Unhook(IC2SMessageHandler handler)
        {
            _router.RemoveC2SHandler(handler.GetHandlingMessageType(), handler);
        }

        public void UnhookCoreEvent(ICoreEventHandler handler)
        {
            _router.RemoveCoreEventHandler(handler.GetHandlingEventType(), handler);
        }
    }
}