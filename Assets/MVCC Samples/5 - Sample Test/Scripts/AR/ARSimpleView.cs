using System.Collections;
using System.Collections.Generic;
using MVC.Model;
using UnityEngine;
using UnityEngine.UI;
namespace MVC.View
{
    public class ARSimpleView : UIView
    {

        #region SubViews
        public RawImage fullImage;
        public GameObject loaderObject;
        public RectTransform baseListObjects;
        public Text labelStore;
        public ProductItemSubView prefab;
        #endregion

        public ARViewModel model;
        static ARSimpleView baseInstance;

        public class ARViewModel
        {
            public List<ProductItemModel> products
            {
                get;
                set;
            }
            public string storeName
            {
                get
                {
                    return baseInstance.labelStore.text;
                }
                set
                {
                    baseInstance.labelStore.text = value;
                }
            }

            public ARViewModel()
            {
                products = new List<ProductItemModel>();
                storeName = string.Empty;
            }
        }

        public class ProductItemModel
        {
            public int id { get; set; }
            public string url { get; set; }

            public string description
            {
                get
                {
                    return subView.labelDescription.text;
                }
                set
                {
                    subView.labelDescription.text = value;
                }
            }
            public string thumb
            {
                get
                {
                    return subView.thumbImage.name;
                }
                set
                {
                    subView.thumbImage.name = value;
                    baseInstance.loaderObject.SetActive(true);
                    subView.app.model.GetModel<DownloadModel>().DownloadImage(value, subView.thumbImage, (result) =>
                    {
                        baseInstance.loaderObject.SetActive(false);
                    });
                }
            }

            public ProductItemSubView subView;

            public ProductItemModel()
            {
                subView = Instantiate(baseInstance.prefab, baseInstance.baseListObjects);
                subView.submodel = this;
            }
        }



        public override void SetModel()
        {
            base.SetModel();
            baseInstance = this;
        }
    }
}