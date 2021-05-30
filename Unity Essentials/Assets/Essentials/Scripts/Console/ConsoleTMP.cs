using System;
using TMPro;
using UnityEngine;
using Console = UnityEngine.Console;

namespace UnityEngine
{
    [RequireComponent(typeof(TMP_Text))]
    public class ConsoleTMP : Console
    {
        [NonSerialized] public TMP_Text textTMP;

        protected override void UpdateVisuals()
        {
            if (textTMP == null)
                textTMP = gameObject.GetComponentRequired<TMP_Text>();

            textTMP.enabled = show;

            if (!show)
                return;

            textTMP.text = fullLog;
        }

    }
}