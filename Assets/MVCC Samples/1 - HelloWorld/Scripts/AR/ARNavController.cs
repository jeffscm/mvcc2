using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCSamples.HelloWorld.Controller
{
    public class ARNavController : UIViewControllerBase
    {
        public override void OnNavigationEnter()
        {
            base.OnNavigationEnter();

            app.model.GetModel<Model.DownloadModel>().GetProducts((obj) =>
            {
                if (obj != null)
                {
                    app.ActivateNavController<TopMenuNavController>();
                    app.GetView<View.ARSimpleView>((view) =>
                    {
                       view.model = obj;
                       view.loaderObject.SetActive(false);
                       view.Present();
                    });
                }
                else
                {
                    app.Notify(NOTIFYSYSTEM.ERROR_MESSAGE, null, "Error OnLoad Data");
                    app.SwitchNavController<LoginNavController>(this.controllerType);
                }
            });

        }

        public override void OnNavigationExit()
        {
            base.OnNavigationExit();

            app.HideViews(this.controllerId);
            app.DeactivateNavController<TopMenuNavController>();
        }

        public override bool OnNotification(object p_event_path, Object p_target, params object[] p_data)
        {
            base.OnNotification(p_event_path, p_target, p_data);

            var path = NotifyArg.ConvertTo<NOTIFYUI>(p_event_path);
            switch (path)
            {
                case NOTIFYUI.UI_CLICK_CHANGE_AR:
                    var sender = p_target as View.ProductItemSubView;

                    app.model.GetModel<Model.DownloadModel>().DownloadImage(
                        sender.submodel.thumb,
                        app.GetView<View.ARSimpleView>().fullImage,
                        (result) => { }
                    );
                    break;
            }

            return true;
        }
    }
}