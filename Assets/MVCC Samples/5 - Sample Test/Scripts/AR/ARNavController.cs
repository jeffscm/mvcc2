using System.Collections;
using System.Collections.Generic;
using MVC.Model;
using MVC.View;
using UnityEngine;


namespace MVC.Controller
{
    public class ARNavController : UIViewControllerBase
    {
        public override void OnNavigationEnter()
        {
            base.OnNavigationEnter();

            app.model.GetModel<DownloadModel>().GetProducts((obj) =>
            {
                if (obj != null)
                {
                    app.ActivateNavController<TopMenuNavController>();
                    app.GetView<ARSimpleView>((view) =>
                    {
                       view.model = obj;
                       view.loaderObject.SetActive(false);
                       view.Present();
                    });
                }
                else
                {
                    app.Notify(NOTIFYSYSTEM.ERROR_MESSAGE, null, "Error OnLoad Data");
                    app.SwitchNavController<MVC.Controller.LoginNavController>(this.controllerType);
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
                    var sender = p_target as ProductItemSubView;

                    app.model.GetModel<DownloadModel>().DownloadImage(
                        sender.submodel.thumb,
                        app.GetView<ARSimpleView>().fullImage,
                        (result) => { }
                    );
                    break;
            }

            return true;
        }
    }
}