using System;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockColor
    {
        Red,
        Yellow,
        Green,
        Orange,
        Blue,
        Pink,
        Purple,
        Cyan,
    }

    [SerializeField] private BlockMaterials blockMaterials;

    [SerializeField] private BlockColor color;
    public BlockColor TheBlockColor => color;

    void OnEnable()
    {
        ApplyMaterial();
    }

    public void SetBlockColor(BlockColor newColor)
    {
        color = newColor;
        ApplyMaterial();
    }

    public void SetRandomBlockColor()
    {
        Array colors = Enum.GetValues(typeof(BlockColor));
        SetBlockColor((BlockColor)colors.GetValue(UnityEngine.Random.Range(0, colors.Length)));
    }

    public void ApplyMaterial()
    {
        if (blockMaterials == null) return;

        Material material = blockMaterials.GetMaterial(color);

        if (material == null) return;

        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();

        if (meshRenderer == null) return;

        meshRenderer.material = material;
    }

}
