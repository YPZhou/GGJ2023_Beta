using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBlur : MonoBehaviour
{
    public Shader CurShader;
    private Material CurMaterial;

    [Range(0, 6), Tooltip("降采样次数，此值越大，所需空间开销越小，运行速度越快")]
    public int downSample = 2;
    [Range(0.0f, 100.0f), Tooltip("模糊半径")]
    public float radiusBlur = 3.0f;
    [Range(0, 8), Tooltip("迭代次数，提高效果的同时有很大的性能开销")]
    public int BlurIterations = 2;

    Material material
    {
        get
        {
            if (CurMaterial == null)
            {
                CurMaterial = new Material(CurShader);
                CurMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return CurMaterial;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (CurShader != null)
        {
            float widthMod = 1.0f / (1.0f * (1 << downSample));
            material.SetFloat("_DownSample", radiusBlur * widthMod);
            source.filterMode = FilterMode.Bilinear;
            int renderWidth = source.width >> downSample;
            int renderHeight = source.height >> downSample;

            // Pass 0 for down sample
            RenderTexture renderBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, source.format);
            renderBuffer.filterMode = FilterMode.Bilinear;
            Graphics.Blit(source, renderBuffer, material, 0);

            // Interation blur
            for (int i = 0; i < BlurIterations; i++)
            {
                float iterationOffs = i * 1.0f;
                material.SetFloat("_DownSample", radiusBlur * widthMod * iterationOffs);
                
                // pass 1 for vertical blur
                RenderTexture tempBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, source.format);
                Graphics.Blit(renderBuffer, tempBuffer, material, 1);
                RenderTexture.ReleaseTemporary(renderBuffer);
                renderBuffer = tempBuffer;

                // pass 2 for horizontal blur
                tempBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, source.format);
                Graphics.Blit(renderBuffer, tempBuffer, material, 2);

                RenderTexture.ReleaseTemporary(renderBuffer);
                renderBuffer = tempBuffer;
            }

            Graphics.Blit(renderBuffer, destination);
            RenderTexture.ReleaseTemporary(renderBuffer);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
