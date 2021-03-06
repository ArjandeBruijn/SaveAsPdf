﻿//Apache2, 2017, WinterDev
//Apache2, 2009, griffm, FO.NET
namespace Fonet.Fo.Flow
{
    using Fonet.DataTypes;
    using Fonet.Fo.Properties;
    using Fonet.Layout;

    internal class TableColumn : FObj
    {
        private Length columnWidthPropVal;
        private int columnWidth;
        private int columnOffset;
        private int numColumnsRepeated;
        private int iColumnNumber;
        private bool setup = false;
        private AreaContainer areaContainer;

        public static FObjMaker<TableColumn> GetMaker()
        {
            return new FObjMaker<TableColumn>((parent, propertyList) => new TableColumn(parent, propertyList));
        }
         

        public TableColumn(FObj parent, PropertyList propertyList)
            : base(parent, propertyList)
        {
           
        }
        public override string ElementName { get { return "fo:table-column"; } }
        public Length GetColumnWidthAsLength()
        {
            return columnWidthPropVal;
        }

        public int GetColumnWidth()
        {
            return columnWidth;
        }

        public void SetColumnWidth(int columnWidth)
        {
            this.columnWidth = columnWidth;
        }

        public int GetColumnNumber()
        {
            return iColumnNumber;
        }

        public int GetNumColumnsRepeated()
        {
            return numColumnsRepeated;
        }

        public void DoSetup(Area area)
        {
            BorderAndPadding bap = propMgr.GetBorderAndPadding();
            BackgroundProps bProps = propMgr.GetBackgroundProps();

            this.iColumnNumber =
                this.properties.GetProperty("column-number").GetNumber().IntValue();

            this.numColumnsRepeated =
                this.properties.GetProperty("number-columns-repeated").GetNumber().IntValue();

            this.columnWidthPropVal =
                this.properties.GetProperty("column-width").GetLength();

            this.columnWidth = columnWidthPropVal.MValue();

            string id = this.properties.GetProperty("id").GetString();
            area.GetMyRefs().InitializeID(id, area);

            setup = true;
        }

        public override Status Layout(Area area)
        {
            if (this.marker == MarkerBreakAfter)
            {
                return new Status(Status.OK);
            }

            if (this.marker == MarkerStart)
            {
                if (!setup)
                {
                    DoSetup(area);
                }
            }
            if (columnWidth > 0)
            {
                this.areaContainer =
                    new AreaContainer(propMgr.GetFontState(area.getFontInfo()),
                                      columnOffset, 0, columnWidth,
                                      area.getContentHeight(), Position.RELATIVE);
                areaContainer.foCreator = this;
                areaContainer.setPage(area.getPage());
                areaContainer.setBorderAndPadding(propMgr.GetBorderAndPadding());
                areaContainer.setBackground(propMgr.GetBackgroundProps());
                areaContainer.SetHeight(area.GetHeight());
                area.addChild(areaContainer);
            }
            return new Status(Status.OK);
        }

        public void SetColumnOffset(int columnOffset)
        {
            this.columnOffset = columnOffset;
        }

        public void SetHeight(int height)
        {
            if (areaContainer != null)
            {
                areaContainer.setMaxHeight(height);
                areaContainer.SetHeight(height);
            }
        }
    }
}