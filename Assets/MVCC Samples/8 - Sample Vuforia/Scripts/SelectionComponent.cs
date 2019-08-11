using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuforiaSample
{
    public class SelectionComponent : AppElement
    {
        public VuforiaPressHandler[] buttons;
        public UIViewAnim viewAnim;

        private void OnEnable()
        {
            viewAnim.onAnimateIn += OnAnimateIn;
            viewAnim.onAnimateOut += OnAnimateOut;
        }

        private void OnDisable()
        {
            viewAnim.onAnimateIn -= OnAnimateIn;
            viewAnim.onAnimateOut -= OnAnimateOut;
        }

        void OnAnimateIn()
        {
            foreach(var button in buttons)
            {
                button.ResetDefaultColor();
            }
        }
        void OnAnimateOut()
        {
            foreach (var button in buttons)
            {
                button.ResetDefaultColor();
            }
        }
    }
}