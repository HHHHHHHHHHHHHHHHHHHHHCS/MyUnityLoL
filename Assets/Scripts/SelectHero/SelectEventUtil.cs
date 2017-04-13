using UnityEngine;
using System.Collections;
using GameProtocol.dto;

public class SelectEventUtil : MonoBehaviour
{
    public delegate void Selected();
    public delegate void RefreshVive(DTOSelectRoom room);
    //public delegate void SelectHero(int heroID);
    public static Selected selected;
    public static RefreshVive refreshVive;
    //public static SelectHero selectHero;
}
