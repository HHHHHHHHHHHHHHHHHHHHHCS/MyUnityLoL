using UnityEngine;
using System.Collections;
using NetFrame.auto;

public interface IHandler
{
    void MessageReceive(SocketModel model);
}
