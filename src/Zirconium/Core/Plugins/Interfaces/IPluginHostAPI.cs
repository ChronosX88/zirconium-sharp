using System.Threading.Tasks;
using MongoDB.Driver;
using Zirconium.Core.Models;

namespace Zirconium.Core.Plugins.Interfaces
{
    public interface IPluginHostAPI
    {
        void Hook(IC2SMessageHandler handler);
        void HookCoreEvent(ICoreEventHandler handler);
        void Unhook(IC2SMessageHandler handler);
        void UnhookCoreEvent(ICoreEventHandler handler);
        void FireEvent(CoreEvent coreEvent);
        string[] GetServerDomains();
        string GetServerID();
        void SendMessage(Session session, BaseMessage message);
        dynamic GetSettings(IPluginAPI plugin);
        dynamic GetSettings(string pluginName);
        void ProvideAuth(IAuthProvider provider);
        IAuthProvider GetAuthProvider();
        IExposedSessionManager GetSessionManager();
        void RegisterIPCService(IPluginAPI plugin, dynamic service);
        Task<dynamic> MakeIPCRequest(string pluginName, string methodName, dynamic paramsObject);
        Task MakeIPCNotification(string pluginName, string methodName, dynamic paramsObject);
        IMongoDatabase GetRawDatabase();
    }
}