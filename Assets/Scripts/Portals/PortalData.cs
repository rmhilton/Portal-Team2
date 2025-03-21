using System.Collections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class PortalData : ScriptableObject
{
    public PortalPair pair;
    public CustomRenderTexture renderTexture;
    public Texture offTexture;
    public Material material;
    public void init(Portal portal, int pairIndex)
    {
        renderTexture = new CustomRenderTexture(512,512);
        renderTexture.updateMode = CustomRenderTextureUpdateMode.Realtime;
        portal.portalView.targetTexture = renderTexture;
        Debug.Log("ding");

        //if there is no assigned pair instance
        if (portal.pair == null)
        {
            pair = new PortalPair();
            portal.pair = pair;
            switch (pairIndex)
            {
                case 2:
                    pair.b = portal;
                    offTexture = PortalSettings.instance.PortalBInactive;
                    break;
                case 1:
                    pair.a = portal;
                    offTexture = PortalSettings.instance.PortalAInactive;
                    break;
                default:
                    Debug.Log("portal did not recieve clean pair index");
                    break;
            }
            material = new Material(PortalSettings.instance.PortalMaterial);
            portal.GetComponent<Renderer>().material = material;
        }
        //if there is an assigned pair instance, but no partner
        else if (portal.pair.getPartner(portal) == null)
        {
            pair = portal.pair;
            switch (pairIndex)
            {
                case 2:
                    pair.b = portal;
                    offTexture = PortalSettings.instance.PortalBInactive;
                    break;
                case 1:
                    pair.a = portal;
                    offTexture = PortalSettings.instance.PortalAInactive;
                    break;
                default:
                    Debug.Log("portal did not recieve clean pair index");
                    break;
            }
            
            material = new Material(PortalSettings.instance.PortalMaterial);
            portal.GetComponent<Renderer>().material = material;
            Deactivate(portal);
        }
        //if there is both an assigned pair instance and a partner
        else
        {
            pair = portal.pair;
            Debug.Log("blink");
            material = new Material(PortalSettings.instance.PortalMaterial);
            portal.GetComponent<Renderer>().material = material;
            Activate(portal);
        }
        
    }
    public IEnumerator Activate(Portal portal)
    {
        yield return new WaitForSeconds(0.001f);
        Debug.Log("Dong");
        Debug.Log(pair.getPartner(portal));
        portal.gameObject.GetComponent<Renderer>().material.SetTexture("_PortalCam", pair.getPartner(portal).portalView.targetTexture);
        portal.rendering = true;
    }
    public void Deactivate(Portal portal)
    {
        Debug.Log("deactive");
        material.SetTexture("_PortalCam", offTexture);
        portal.rendering = false;
    }
}
