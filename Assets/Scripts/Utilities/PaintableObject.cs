using UnityEngine;


public class PaintableObject : MonoBehaviour
{
    [SerializeField] public float surfaceArea = -1;
    [SerializeField] public float uvRatio = -1;
    [SerializeField] public int textureSize = -1;

    public RenderTexture paintRT;

    private void Start()
    {
        Debug.Log(gameObject.GetComponent<MeshRenderer>().material.mainTexture);
        InitPaintLayer(gameObject.GetComponent<MeshRenderer>().material.mainTexture as Texture2D);

    }

    void InitPaintLayer(Texture2D texture)
    {
        paintRT = new RenderTexture(texture.width, texture.height, 0, RenderTextureFormat.ARGB32);
        paintRT.Create();

        RenderTexture active = RenderTexture.active;
        RenderTexture.active = paintRT;
        GL.Clear(true, true, Color.gray);
        RenderTexture.active = active;

        gameObject.GetComponent<MeshRenderer>().material.mainTexture = paintRT;
    }
}