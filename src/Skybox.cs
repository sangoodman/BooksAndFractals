using Il2Cpp;
using UnityEngine;
using UnityEngine.Audio;

namespace BooksAndFractals
{
    /// <summary>
    /// Tools related to skybox.
    /// </summary>
    public static class SkyboxManipulator
    {
        /// <summary>
        /// Changed the exposure value of skybox.
        /// </summary>
        /// <param name="exp">The exposure value.</param>
        public static void Exposure(float exp)
        {
            RenderSettings.skybox.SetFloat("_Exposure", exp);
        }
        /// <summary>
        /// Changes the skybox texture by using the material.
        /// </summary>
        /// <param name="skyboxMat">Skybox material, loaded by AssetLoader.</param>
        public static void ChangeSkybox(Material skyboxMat)
        {
            RenderSettings.skybox = skyboxMat;
            DynamicGI.UpdateEnvironment();
        }
        /// <summary>
        /// Changes the skybox texture by using textures.
        /// </summary>
        /// <param name="front"> The front face. </param>
        /// <param name="back"> The back face. </param>
        /// <param name="left"> The left face. </param>
        /// <param name="right"> The right face.</param>
        /// <param name="up"> The up face. </param>
        /// <param name="down"> The down face. </param>
        public static void ChangeSkybox(Texture2D front, Texture2D back, Texture2D left, Texture2D right, Texture2D up, Texture2D down)
        {
            Shader skyboxShader = Shader.Find("Skybox/6 Sided");

            Material skyboxMaterial = new Material(skyboxShader);
            skyboxMaterial.SetTexture("_FrontTex", front);
            skyboxMaterial.SetTexture("_BackTex", back);
            skyboxMaterial.SetTexture("_LeftTex", left);
            skyboxMaterial.SetTexture("_RightTex", right);
            skyboxMaterial.SetTexture("_UpTex", up);
            skyboxMaterial.SetTexture("_DownTex", down);

            RenderSettings.skybox = skyboxMaterial;
            DynamicGI.UpdateEnvironment();
        }
    }
}
