using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;
using MVC.View;

namespace Tests
{

    public class NewTestScript
    {
        static bool loaded = false;
        [SetUp]
        public void Setup()
        {
            if (loaded) return;
            loaded = true;
            Debug.Log("Load Scene");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            //EditorSceneManager.OpenScene("Assets/Sample/Sample.unity");
        }


        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions
            Debug.Log("Run test");
        }


        [UnityTest]        
        public IEnumerator CheckValidLogin()
        {
            yield return null;
           
            MVC.Controller.LoginNavController.onStubExecute = (r) =>
            {
                if (r)
                    Assert.Pass("Valid Login Check");
                else
                    Assert.Fail("Valid Login Fail");

                MVC.Controller.LoginNavController.onStubExecute = null;
            };
            MVCC.app.GetView<LoginView>().passwordInput.text = "123";

            yield return null;

            yield return null;
            MVCC.app.Notify(NOTIFYUI.UI_CLICK_LOGIN, null, null);
            yield return null;

            MVC.Controller.LoginNavController.onStubExecute = null;
        }

        [UnityTest]
        public IEnumerator CheckInvalidLogin()
        {

            yield return null;
            MVC.Controller.LoginNavController.onStubExecute = (r) =>
            {
                if (r)
                    Assert.Fail("Invalid Login Fail");
                else
                    Assert.Pass("Invalid Login Pass");

                MVC.Controller.LoginNavController.onStubExecute = null;
            };

            MVCC.app.GetView<LoginView>().passwordInput.text = "3243423";

            yield return null;

            yield return null;
            MVCC.app.Notify(NOTIFYUI.UI_CLICK_LOGIN, null, null);
            yield return null;
            MVC.Controller.LoginNavController.onStubExecute = null;
        }
    }
}
