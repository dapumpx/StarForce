using System.Collections;
using System.Collections.Generic;
using System.Net;
using GameFramework.Event;
using GameFramework.Network;
using StarForce;
using UnityEngine;
using UnityGameFramework.Runtime;

public class TestSocket : MonoBehaviour {

	// Use this for initialization
	void Start () {
        NetworkChannelHelper helper = new NetworkChannelHelper();
		INetworkChannel channel = StarForce.GameEntry.Network.CreateNetworkChannel("socket0", helper);
        //helper.Initialize(channel);
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
		//IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

		StarForce.GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId, OnNetworkConnected);
		channel.Connect(ipAddress, 11000);



	}

	private void OnNetworkConnected(object sender, GameEventArgs e)
    {
        UnityGameFramework.Runtime.NetworkConnectedEventArgs ne = (UnityGameFramework.Runtime.NetworkConnectedEventArgs)e;
		if (ne.NetworkChannel != StarForce.GameEntry.Network.GetNetworkChannel("socket0"))
        {
            return;
        }

		Debug.Log("Ready to Send... =>");
		CSHeartBeat p = new CSHeartBeat();

		ne.NetworkChannel.Send<CSHeartBeat>(p);
    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
