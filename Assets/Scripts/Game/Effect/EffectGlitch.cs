using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2023.Beta
{
    public class EffectGlitch : MonoBehaviour
    {
        public Shader CurShader;
        private Material CurMaterial;

        [Range(0, 100)]
        public float GlitchPower;
        [Range(0, 100)]
        public float JitterPower;

        public float JitterImpact = 0f;

        private Vector2 glitchVec;
        private Vector2 jitterVec;
        

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

        private void FixedUpdate()
        {
            // Glich
            float glitchPower = (GameStatus.MAX_HEALTH - GameStatus.Health);
            glitchPower *= glitchPower;
            Vector2 glitchDir = new Vector2(
                Mathf.PerlinNoise(Time.realtimeSinceStartup * glitchPower, 0) * 2 - 1,
                Mathf.PerlinNoise(Time.realtimeSinceStartup * glitchPower, 1) * 2 - 1);
            glitchVec = glitchDir * glitchPower * GlitchPower;

            // Jitter Impact
            Vector2 impactDir = new Vector2(
                Mathf.PerlinNoise(Time.realtimeSinceStartup, 2) * 2 - 1, 1).normalized;
            jitterVec = JitterPower * JitterImpact * GlitchPower * impactDir;
            JitterImpact *= 0.5f;

        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (CurShader != null)
            {
                material.SetVector("_GlitchVec", glitchVec);
                material.SetVector("_JitterVec", jitterVec);
                Graphics.Blit(source, destination, material, 0);
            }
            else
            {
                Graphics.Blit(source, destination);
            }
        }
    }
}
