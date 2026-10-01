using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockMaterials", menuName = "Scriptable Objects/Block Material")]
public class BlockMaterials : ScriptableObject
{
    [Serializable]
    public class MaterialEntry
    {
        public Block.BlockColor color;
        public Material material;
    }

    [SerializeField]
    private MaterialEntry[] materials;

    public Material GetMaterial(Block.BlockColor color)
    {
        foreach (MaterialEntry entry in materials)
        {
            if (entry.color == color)
            {
                return entry.material;
            }

        }

        return null;
    }
}
