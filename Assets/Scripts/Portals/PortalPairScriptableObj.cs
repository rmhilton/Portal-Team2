using UnityEngine;

public class PortalPairScriptableObj : ScriptableObject
{
    public Portal a;
    public Portal b;

    public Portal getPartner(Portal self)
    {
        if(self == a)
        {
            return b;
        }
        else if(self == b)
        {
            return a;
        }
        else
        {
            return null;
        }

    }
}
