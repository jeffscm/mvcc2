using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVCSamples.HelloWorld.View
{
    public class ErrorView : UIView
    {
        static ErrorView baseInstance;
        public ErrorViewModel model;

        public class ErrorViewModel
        {
            public string msg
            {
                get
                {
                    return baseInstance.labelError.text;
                }
                set
                {
                    baseInstance.labelError.text = value;
                }
            }
        }


        public Text labelError;

        public override void SetModel()
        {
            base.SetModel();
            baseInstance = this;
            model = new ErrorViewModel();
        }
    }
}
