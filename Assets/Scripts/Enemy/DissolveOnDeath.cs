using UnityEngine;

namespace Mahou
{
    class DissolveOnDeath : MonoBehaviour
    {
        private float progress = 0f;
        private bool disolving = false;
        public Renderer targetRenderer;
        public GameObject destroyOnComplete;

        private void Update()
        {
            if (disolving)
            {
                foreach (var mat in targetRenderer.materials)
                    mat.SetFloat("Dissolve_Progress", progress);
                progress += Time.deltaTime;

                if (progress >= 1)
                {
                    disolving = false;
                    Destroy(destroyOnComplete);
                }
            }
        }


        public void ANIM_DissolveBegin(string s)
        {
            disolving = true;
        }
    }
}
