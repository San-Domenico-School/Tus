using UnityEngine;


public class PaintableObject : MonoBehaviour
{
    [SerializeField] public float surfaceArea = -1;
    [SerializeField] public float uvRatio = -1;
    [SerializeField] public int textureSize = -1;
    [SerializeField] public float fullTextureArea = -1;

    public Color lastPaintedColor;

    public RenderTexture paintRT;

}