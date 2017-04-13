using UnityEngine;
using System.Collections;
using GameProtocol.dto;

public class GameData 
{
    private static DTOUser user;

    public static DTOUser User
    {
        get
        {
            return user;
        }

        set
        {
            user = value;
        }
    }
}
