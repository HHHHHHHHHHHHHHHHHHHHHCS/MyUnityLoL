using UnityEngine;
using System.Collections;

public class StartNet : MonoBehaviour
{
    void Awake()
    {
        NetIO.Instance.Init();
        DontDestroyOnLoad(gameObject);
    }

    void OnDestroy()
    {
        NetIO.Instance.CloseSocket();
    }
}
