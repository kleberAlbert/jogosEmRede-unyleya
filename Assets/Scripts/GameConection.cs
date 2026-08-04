using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameConection : MonoBehaviourPunCallbacks
{
    public Text chatLog;
    private string myNickName; // Variável para garantir que o valor não se perca

    private void Start()
    {
        // 1. Gera o nome localmente
        myNickName = "Koelho_" + Random.Range(1000, 9999);

        // 2. Aplica ao Photon e atualiza a UI
        PhotonNetwork.NickName = myNickName;

        if (chatLog != null)
        {
            chatLog.text = myNickName + " - Conectando ao servidor...";
        }

        // 3. Conecta
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        
        // Reforça a definição do nome no servidor Master
        PhotonNetwork.NickName = myNickName;

        if (chatLog != null)
            chatLog.text = "Conectado ao servidor!";

        if (!PhotonNetwork.InLobby)
        {
            if (chatLog != null) chatLog.text = "Entrando no Lobby...";
            PhotonNetwork.JoinLobby();
        }
        else
        {
            if (chatLog != null) chatLog.text = "Já está no Lobby!";
        }
    }

    public override void OnJoinedLobby()
    {
        if (chatLog != null) chatLog.text = "Entrando na Sala de Atividade 4...";
        PhotonNetwork.JoinRoom("Atividade 4");    
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if (chatLog != null) chatLog.text = "Sala não encontrada, criando a sala...";
        PhotonNetwork.CreateRoom("Atividade 4", new RoomOptions { MaxPlayers = 10 });
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.NickName = myNickName;
        // Garante a leitura do NickName da variável caso o Photon demore para atualizar a propriedade LocalPlayer
        string localNick = !string.IsNullOrEmpty(PhotonNetwork.NickName) 
            ? PhotonNetwork.NickName 
            : myNickName;

        if (chatLog != null)
        {  
            if (localNick != null)
                chatLog.text = "Entrou na sala de Atividade 4! UserName = " + localNick;
            else
                Debug.Log("NickName não definido, usando valor local: " + myNickName);
                chatLog.text = "Entrou na sala de Atividade 4! UserName não definido.";
        }
        chatLog.text = "Sala " + PhotonNetwork.CurrentRoom.Name;
        Debug.Log("NickName confirmado: " + localNick);
        
    }
    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        Debug.LogError("Erro de conexão: " + errorInfo.Info);
        if (chatLog != null)
            chatLog.text = "Erro de conexão: " + errorInfo.Info;
    }
}