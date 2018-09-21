﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace CommonTools
{
    public class MeshFBXEditorTools
    {
        [MenuItem("CommonTools/Mesh/设置FBX模式(readwrite,compress)(选择文件夹)")]
        public static void SetFolderFbxType()
        {
            Object[] folds = Selection.objects;
            for (int iii = 0; iii < folds.Length; iii++)
            {
                string relatepath = AssetDatabase.GetAssetPath(folds[iii]);
                string folderpath = Path.GetFullPath(relatepath);
                AssetsSearch assetSearch = new ModelSearch(folderpath.Replace("\\", "/").Replace(Application.dataPath, "Assets"));
                assetSearch.Search();
                for (int i = 0; i < assetSearch.Assets.Count; i++)
                {
                    string filepath = assetSearch.Assets[i];
                    if (!Path.GetExtension(filepath).Equals(".FBX"))
                        continue;
                    ModelImporter assets = AssetImporter.GetAtPath(filepath) as ModelImporter;
                    if (assets == null)
                    {
                        Debug.LogError("assetimport is null" + filepath);
                        continue;
                    }
                    if (filepath.Contains("@"))
                    {
                        assets.isReadable = false;
                        assets.importMaterials = false;
                        assets.animationCompression = ModelImporterAnimationCompression.Optimal;
                    }
                    else
                    {
                        assets.importAnimation = false;
                        assets.isReadable = false;
                    }
                    assets.SaveAndReimport();
                }
            }
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 置空FBX上默认材质球引用的Shader
        /// </summary>
        /// <param name="model"></param>
        public static void HandleDeleteFbxMaterials(GameObject model)
        {
            Renderer[] renders = model.GetComponentsInChildren<Renderer>();
            if (null != renders)
            {
                foreach (Renderer render in renders)
                {
                    render.sharedMaterials = new Material[render.sharedMaterials.Length];
                }
            }
        }
        /// <summary>
        /// 重新导入所有的FBX文件
        /// </summary>
        [MenuItem("CommonTools/FBX/重新导入所有的Mesh文件来删除StandardShader")]
        public static void ReImPortAllFbxFiles()
        {
            string[] allassets = AssetDatabase.GetAllAssetPaths();
            foreach (var assetpath in allassets)
            {
                if (Path.GetExtension(assetpath).Equals(".FBX") || Path.GetExtension(assetpath).Equals(".obj"))
                {
                    AssetDatabase.ImportAsset(assetpath, ImportAssetOptions.Default);
                    Debug.LogError(assetpath);
                }
            }
        }
    }
}
