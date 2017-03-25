﻿//Apache2, 2017, WinterDev
//Apache2, 2009, griffm, FO.NET
using Fonet.Fo.Properties;
using Fonet.Layout;

namespace Fonet.Fo.Pagination
{
    internal class RegionBefore : Region
    {
        

        public static FObjMaker<RegionBefore> GetMaker()
        {
            return new FObjMaker<RegionBefore>((parent, propertyList) => new RegionBefore(parent, propertyList));
        }


        public const string REGION_CLASS = "before";

        private int precedence;

        protected RegionBefore(FObj parent, PropertyList propertyList)
            : base(parent, propertyList)
        {
            precedence = this.properties.GetProperty("precedence").GetEnum();
        }


        public override RegionArea MakeRegionArea(int allocationRectangleXPosition,
                                                  int allocationRectangleYPosition,
                                                  int allocationRectangleWidth,
                                                  int allocationRectangleHeight)
        {
            BorderAndPadding bap = propMgr.GetBorderAndPadding();
            BackgroundProps bProps = propMgr.GetBackgroundProps();
            int extent = this.properties.GetProperty("extent").GetLength().MValue();

            RegionArea area = new RegionArea(
                allocationRectangleXPosition,
                allocationRectangleYPosition,
                allocationRectangleWidth,
                extent);
            area.setBackground(bProps);

            return area;
        }


        protected override string GetDefaultRegionName()
        {
            return "xsl-region-before";
        }

        protected override string GetElementName()
        {
            return "fo:region-before";
        }

        public override string GetRegionClass()
        {
            return REGION_CLASS;
        }

        public bool getPrecedence()
        {
            return (precedence == Precedence.TRUE ? true : false);
        }

    }
}