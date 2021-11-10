using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
	public TMP_InputField createInput;
	public TMP_InputField joinInput;
	public TMP_Text error;

	public void CreateRoom()
	{
		if (string.IsNullOrEmpty(createInput.text))
			return;

		PhotonNetwork.CreateRoom(createInput.text);
	}

	public void JoinRoom()
	{
		PhotonNetwork.JoinRoom(joinInput.text);
	}

	public override void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel("Main");
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		error.text = message;
		//base.OnCreateRoomFailed(returnCode, message);
		Debug.Log("Error creating Room" + message);
	}
}
