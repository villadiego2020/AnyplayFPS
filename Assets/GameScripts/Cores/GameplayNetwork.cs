using FishNet.Managing;
using FishNet.Transporting;
using System;
using UnityEngine;

namespace AFPS.Cores
{
    public class GameplayNetwork : MonoBehaviour
    {
        /// <summary>
        /// Found NetworkManager.
        /// </summary>
        public NetworkManager NetworkManager { get; private set; }
        /// <summary>
        /// Current state of client socket.
        /// </summary>
        public LocalConnectionState m_ClientState { get; private set; } = LocalConnectionState.Stopped;
        /// <summary>
        /// Current state of server socket.
        /// </summary>
        public LocalConnectionState m_ServerState { get; private set; } = LocalConnectionState.Stopped;

        public static Action<GameObject> OnConnectedEvent;

        public void Initialize()
        {
            NetworkManager = FindObjectOfType<NetworkManager>();

            NetworkManager.ServerManager.OnServerConnectionState += OnServerConnectionState;
            NetworkManager.ClientManager.OnClientConnectionState += OnClientConnectionState;
            NetworkManager.ClientManager.OnConnectedClients += Test;
        }

        private void Test(ConnectedClientsArgs args)
        {

        }

        private void OnServerConnectionState(ServerConnectionStateArgs obj)
        {
            m_ServerState = obj.ConnectionState;
            Debug.Log("Server Connection State: " + m_ServerState);
        }

        private void OnClientConnectionState(ClientConnectionStateArgs obj)
        {
            m_ClientState = obj.ConnectionState;
            Debug.Log("Client Connection State: " +  m_ClientState);
        }

        public void OnClickClient()
        {
            if (NetworkManager == null)
                return;

            if (m_ClientState != LocalConnectionState.Stopped)
            {
                NetworkManager.ClientManager.StopConnection();
            }
            else
            {
                NetworkManager.ClientManager.StartConnection();
            }
        }

        public void OnClickServer()
        {
            if (NetworkManager == null)
                return;

            if (m_ServerState != LocalConnectionState.Stopped)
            {
                NetworkManager.ServerManager.StopConnection(true);
            }
            else
            {
                NetworkManager.ServerManager.StartConnection();
            }
        }
    }
}