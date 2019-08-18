using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVCSamples.HelloWorld.View
{
    public class ProductItemSubView : AppElement
    {

        public ARSimpleView.ProductItemModel submodel;
        public Text labelDescription;
        public RawImage thumbImage;

    }
}