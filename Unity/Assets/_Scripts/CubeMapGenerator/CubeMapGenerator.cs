using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.CubeMapGenerator
{
    public class CubeMapGenerator : MonoBehaviour
    {
        [SerializeField] Camera userCamera;
        [SerializeField] RenderTexture cubeMapLeft;
        [SerializeField] RenderTexture equirect;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                CreateCubemap();
        }

        void CreateCubemap()
        {
            userCamera.RenderToCubemap(cubeMapLeft);
            cubeMapLeft.ConvertToEquirect(equirect);
            Save(equirect);
        }

        void Save(RenderTexture rt)
        {
            Texture2D tex = new Texture2D(rt.width, rt.height);

            RenderTexture.active = rt;
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            RenderTexture.active = null;

            byte[] bytes = tex.EncodeToPNG();
            string path = Application.dataPath + "/Panarama" + ".png";

            System.IO.File.WriteAllBytes(path, bytes);
        }
    }
}