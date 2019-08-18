using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVCSamples.HelloWorld.View
{
    public class LoginView : UIView
    {

        public LoginViewModel model;
        static LoginView baseInstance;

        public class LoginViewModel
        {           
            public string username { get; set; }
            public string password
            {
                get
                {
                    return baseInstance.passwordInput.text;
                }
                set
                {
                    baseInstance.passwordInput.text = value;
                }
            }
        }

        public InputField passwordInput;

        public override void SetModel()
        {
            base.SetModel();
            baseInstance = this;
            model = new LoginViewModel();
        }
    }
}