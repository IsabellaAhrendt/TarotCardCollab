// Example Script for Renderer Group
using UnityEngine;

public class RendererGroupAlpha : MonoBehaviour
{
    public float targetAlpha = 1f;
    Renderer[] renderers; // For 3D objects
    SpriteRenderer[] spriteRenderers; // For 2D sprites

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        SetGroupAlpha(targetAlpha); // Set initial alpha
    }

    public void SetGroupAlpha(float alphaValue)
    {
        // For 3D Renderers (MeshRenderer, SkinnedMeshRenderer)
        foreach (Renderer rend in renderers)
        {
            MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
            rend.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", new Color(1, 1, 1, alphaValue)); // Adjust "_Color" if using a different shader property
            rend.SetPropertyBlock(propBlock);
        }

        // For 2D Sprite Renderers
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            Color c = sr.color;
            sr.color = new Color(c.r, c.g, c.b, alphaValue);
        }
    }
}
