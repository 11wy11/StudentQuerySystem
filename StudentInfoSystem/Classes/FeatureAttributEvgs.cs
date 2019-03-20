using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Whu_Navigation.Classes
{
    class FeatureAttributeEventArgs
    {
            public IArray SelectFeatures = null;
            public IPolygon pPolygon = null;

    }
}
