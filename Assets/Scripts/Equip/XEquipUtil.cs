﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XTable;


public class EquipPart
{
    public string[] partPath = new string[8];
    public string mainWeapon;
    public uint hash = 0;
    public List<string> suitName = new List<string>();
}

public class TempEquipSuit
{
    public uint hash = 0;
    public List<TempEquipData> data = new List<TempEquipData>();
}

public class TempEquipData
{
    public FashionList.RowData row;
    public string path;
}

public class ThreePart
{
    public string[] part = new string[3];
    public int id = 0;
}

public class XEquipUtil
{
    public static CombineMeshUtility _CombineMeshUtility = null;

    public static readonly Shader _flow_Spec = Shader.Find("Custom/Skin/FlowTexSpec");
    public static readonly Shader _flow_Diffuse = Shader.Find("Custom/Skin/FlowTexDiff");
    public static readonly Shader _skin_cube = Shader.Find("Custom/Skin/Cube");
    public static readonly Shader _skin_cube_nomask = Shader.Find("Custom/Skin/CubeNoMask");
    public static readonly Shader _skin_cutout = Shader.Find("Custom/Skin/RimlightBlendCutout");
    public static readonly Shader _skin_nocutout = Shader.Find("Custom/Skin/RimlightBlendNoCutout");
    public static readonly Shader _skin_blend = Shader.Find("Custom/Skin/RimlightBlend");
    public static readonly Shader _skin8 = Shader.Find("Custom/Skin/RimlightBlend8");

    private static Shader _EquipShader = null;


    public static Material GetRoleMat()
    {
        return GetMaterial(_skin8);
    }

    private static Material GetMaterial(Shader shader)
    {
        if (shader == _skin_cutout)
        {
            return XResourceMgr.Load<Material>("Materials/Char/RimLightBlendCutout");//mat
        }
        else if (shader == _skin_nocutout)
        {
            return XResourceMgr.Load<Material>("Materials/Char/RimLightBlendNoCutout");
        }
        else if (shader == _skin_blend)
        {
            return XResourceMgr.Load<Material>("Materials/Char/RimLightBlend");
        }
        else if (shader == _skin8)
        {
            return XResourceMgr.Load<Material>("Materials/Char/RimLightBlend8");
        }
        return new Material(shader);
    }

    public static Shader GetEquipShader()
    {
        if (_EquipShader == null)
            _EquipShader = _skin8;
        return _EquipShader;
    }

    public static void ReturnMaterial(Material mat)
    {
        if (mat != null)
        {
            Shader shader = mat.shader;
            if (shader == _skin_cutout || shader == _skin_nocutout)
            {
                mat.SetTexture("_Face", null);
                mat.SetTexture("_Hair", null);
                mat.SetTexture("_Body", null);
                mat.SetTexture("_Alpha", null);
            }
            else if (shader == _skin_blend)
            {
                mat.SetTexture("_Face", null);
                mat.SetTexture("_Hair", null);
                mat.SetTexture("_Body", null);
            }
            else if (shader == _skin8)
            {
                mat.SetTexture("_Tex0", null);
                mat.SetTexture("_Tex1", null);
                mat.SetTexture("_Tex2", null);
                mat.SetTexture("_Tex3", null);
                mat.SetTexture("_Tex4", null);
                mat.SetTexture("_Tex5", null);
                mat.SetTexture("_Tex6", null);
                mat.SetTexture("_Tex7", null);
            }
            if (Application.isPlaying)
                UnityEngine.Object.Destroy(mat);
        }
    }

    public static SkinnedMeshRenderer GetSmr(GameObject keyGo)
    {
        Transform skinmesh = keyGo.transform.FindChild("CombinedMesh");
        if (skinmesh == null)
        {
            GameObject skinMeshGo = new GameObject("CombinedMesh");
            skinMeshGo.transform.parent = keyGo.transform;
            skinmesh = skinMeshGo.transform;
        }
        SkinnedMeshRenderer skm = skinmesh.GetComponent<SkinnedMeshRenderer>();
        if (skm == null)
            skm = skinmesh.gameObject.AddComponent<SkinnedMeshRenderer>();
        return skm;
    }

    public static void MakeEquip(string name, int[] fashionIDs, List<EquipPart> equipList, TempEquipSuit tmpFashionData, int suitID)
    {
        FashionList fashionList = new FashionList(true);
        if (fashionIDs != null)
        {
            tmpFashionData.hash = 0;
            tmpFashionData.data.Clear();
            bool threePart = false;
            for (int i = 0; i < fashionIDs.Length; ++i)
            {
                int fashionID = fashionIDs[i];
                FashionList.RowData row = fashionList.GetByItemID(fashionID);
                if (row != null)
                {
                    List<ThreePart> tpLst = new List<ThreePart>();
                    if (row.EquipPos == 7 || row.EquipPos == 8 || row.EquipPos == 9)
                    {
                        ThreePart tp = FindThreePart(suitID, tpLst);
                        if (row.EquipPos == 9)
                        {
                            tp.part[2] = row.ModelPrefabArcher;
                        }
                        threePart = true;
                    }
                    else
                    {
                        if (row.ReplaceID != null && row.ReplaceID.Length > 1)
                        {
                            FashionList.RowData replace = fashionList.GetByItemID(row.ReplaceID[1]);
                            if (replace != null)
                            {
                                if (replace.EquipPos == row.EquipPos) row = replace;
                            }
                        }
                        string path = row.ModelPrefabArcher;
                        if (!string.IsNullOrEmpty(path))
                        {
                            Hash(ref tmpFashionData.hash, path);
                            TempEquipData data = new TempEquipData();
                            data.row = row;
                            data.path = path;
                            tmpFashionData.data.Add(data);
                        }
                    }
                }
            }
            if (threePart) return;

            bool findSame = false;
            TempEquipSuit suit = tmpFashionData;
            if (suit.hash == 0) return;
            for (int j = 0; j < equipList.Count; ++j)
            {
                EquipPart part = equipList[j];
                if (part != null && part.hash == suit.hash)
                {
                    part.suitName.Add(name);
                    findSame = true;
                    break;
                }
            }
            if (!findSame)
            {
                EquipPart part = new EquipPart();
                part.hash = suit.hash;
                part.suitName.Add(name);
                for (int j = 0; j < suit.data.Count; ++j)
                {
                    TempEquipData data = suit.data[j];
                    int partPos = ConvertPart(data.row.EquipPos);
                    if (partPos < part.partPath.Length)
                    {
                        part.partPath[partPos] = data.path;
                    }
                    else if (partPos == part.partPath.Length)
                    {
                        part.mainWeapon = data.path;
                    }
                }
                equipList.Add(part);
            }
        }
    }

    private static void Hash(ref uint hash, string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            hash = (hash << 5) + hash + str[i];
        }
    }


    private static ThreePart FindThreePart(int id, List<ThreePart> lst)
    {
        for (int i = 0; i < lst.Count; ++i)
        {
            ThreePart tp = lst[i];
            if (tp.id == id) return tp;
        }
        ThreePart newTp = new ThreePart();
        newTp.id = id;
        lst.Add(newTp);
        return newTp;
    }

    private static int ConvertPart(int pos)
    {
        switch (pos)
        {
            case 0:
                return (int)EPartType.EHeadgear;
            case 1:
                return (int)EPartType.EUpperBody;
            case 2:
                return (int)EPartType.ELowerBody;
            case 3:
                return (int)EPartType.EGloves;
            case 4:
                return (int)EPartType.EBoots;
            case 5:
                return (int)EPartType.EMainWeapon;
            case 6:
                return (int)EPartType.ESecondaryWeapon;
            case 7:
                return (int)EPartType.EWings;
            case 8:
                return (int)EPartType.ETail;
            case 9:
                return (int)EPartType.EDecal;
            case 10:
                return (int)EPartType.EFace;
            case 11:
                return (int)EPartType.EHair;
        }
        return -1;
    }

}