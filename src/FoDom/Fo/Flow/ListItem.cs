﻿//Apache2, 2017, WinterDev
//Apache2, 2009, griffm, FO.NET
namespace Fonet.Fo.Flow
{
    using Fonet.Layout;
    using Fonet.Fo.Properties;
    internal class ListItem : FObj
    {
        public static FObjMaker<ListItem> GetMaker()
        {
            return new FObjMaker<ListItem>((parent, propertyList) => new ListItem(parent, propertyList));
        }
         

        private TextAlign align;
        private TextAlign alignLast;
        private int lineHeight;
        private int spaceBefore;
        private int spaceAfter;
        private string id;
        private BlockArea blockArea;

        public ListItem(FObj parent, PropertyList propertyList)
            : base(parent, propertyList)
        { 
        }
        public override string ElementName { get { return "fo:list-item"; } }

        public override Status Layout(Area area)
        {
            if (this.marker == MarkerStart)
            {
                AccessibilityProps mAccProps = propMgr.GetAccessibilityProps();
                AuralProps mAurProps = propMgr.GetAuralProps();
                BorderAndPadding bap = propMgr.GetBorderAndPadding();
                BackgroundProps bProps = propMgr.GetBackgroundProps();
                MarginProps mProps = propMgr.GetMarginProps();
                RelativePositionProps mRelProps = propMgr.GetRelativePositionProps();

                this.align = this.properties.GetTextAlign();
                this.alignLast = this.properties.GetTextAlignLast();
                this.lineHeight =
                    this.properties.GetProperty("line-height").GetLength().MValue();
                this.spaceBefore =
                    this.properties.GetProperty("space-before.optimum").GetLength().MValue();
                this.spaceAfter =
                    this.properties.GetProperty("space-after.optimum").GetLength().MValue();
                this.id = this.properties.GetProperty("id").GetString();

                area.getIDReferences().CreateID(id);

                this.marker = 0;
            }

            if (area is BlockArea)
            {
                area.end();
            }

            if (spaceBefore != 0)
            {
                area.addDisplaySpace(spaceBefore);
            }

            this.blockArea =
                new BlockArea(propMgr.GetFontState(area.getFontInfo()),
                              area.getAllocationWidth(), area.spaceLeft(), 0, 0,
                              0, align, alignLast, lineHeight);
            this.blockArea.setTableCellXOffset(area.getTableCellXOffset());
            this.blockArea.setGeneratedBy(this);
            this.areasGenerated++;
            if (this.areasGenerated == 1)
            {
                this.blockArea.isFirst(true);
            }
            this.blockArea.addLineagePair(this, this.areasGenerated);

            blockArea.setParent(area);
            blockArea.setPage(area.getPage());
            blockArea.start();

            blockArea.setAbsoluteHeight(area.getAbsoluteHeight());
            blockArea.setIDReferences(area.getIDReferences());

            int numChildren = this.children.Count;
            if (numChildren != 2)
            {
                throw new FonetException("list-item must have exactly two children");
            }
            ListItemLabel label = (ListItemLabel)children[0];
            ListItemBody body = (ListItemBody)children[1];

            Status status;

            if (this.marker == 0)
            {
                area.GetMyRefs().ConfigureID(id, area);

                status = label.Layout(blockArea);
                if (status.isIncomplete())
                {
                    return status;
                }
            }

            status = body.Layout(blockArea);
            if (status.isIncomplete())
            {
                blockArea.end();
                area.addChild(blockArea);
                area.increaseHeight(blockArea.GetHeight());
                this.marker = 1;
                return status;
            }

            blockArea.end();
            area.addChild(blockArea);
            area.increaseHeight(blockArea.GetHeight());

            if (spaceAfter != 0)
            {
                area.addDisplaySpace(spaceAfter);
            }

            if (area is BlockArea)
            {
                area.start();
            }
            this.blockArea.isLast(true);
            return new Status(Status.OK);
        }

        public override int GetContentWidth()
        {
            if (blockArea != null)
            {
                return blockArea.getContentWidth();
            }
            else
            {
                return 0;
            }
        }
    }
}