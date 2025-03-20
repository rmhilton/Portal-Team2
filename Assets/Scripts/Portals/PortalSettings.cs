using UnityEngine;

public class PortalSettings : MonoBehaviour
{
    #region singleton
    public static PortalSettings instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public RenderTexture PortalRender;
    public Material PortalMaterial;
    public Texture PortalAInactive;
    public Texture PortalBInactive;
}
