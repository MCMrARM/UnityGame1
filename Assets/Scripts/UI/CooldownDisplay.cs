using Mahou.Combat;
using Mahou.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mahou.UI
{
    public class CooldownDisplay : MonoBehaviour
    {
        public SpellCaster caster;
        public SpellConfig spell;
        public RawImage image;
        public TMPro.TextMeshProUGUI text;
        public Color normalColor;
        public Color grayoutColor;

        void Start()
        {

        }

        void Update()
        {
            float cd = caster.GetCD(spell);
            if (cd > 0)
            {
                image.color = grayoutColor;
                text.text = cd.ToString("0.0");
                text.enabled = true;
            }
            else
            {
                image.color = normalColor;
                text.enabled = false;
            }
        }
    }
}
