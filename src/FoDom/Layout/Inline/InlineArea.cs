﻿//Apache2, 2017, WinterDev
//Apache2, 2009, griffm, FO.NET
namespace Fonet.Layout.Inline
{
    using Fonet.Fo.Properties;
    internal abstract class InlineArea : Area
    {
        private int yOffset = 0;
        private int xOffset = 0;
        protected int height = 0;
        private VerticalAlign verticalAlign = 0;

        private float red, green, blue;
        protected bool underlined = false;
        protected bool overlined = false;
        protected bool lineThrough = false;

        public InlineArea(
            FontState fontState, int width,
            float red,
            float green,
            float blue)
            : base(fontState)
        {
            this.contentRectangleWidth = width;
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public float getBlue()
        {
            return this.blue;
        }

        public float getGreen()
        {
            return this.green;
        }

        public float getRed()
        {
            return this.red;
        }

        internal PdfColor GetColor()
        {
            //What about alpha?
            return new PdfColor(this.red, this.green, this.blue);
        }
        public override void SetHeight(int height)
        {
            this.height = height;
        }

        public override int GetHeight()
        {
            return this.height;
        }

        public virtual void setVerticalAlign(VerticalAlign align)
        {
            this.verticalAlign = align;
        }

        public virtual VerticalAlign getVerticalAlign()
        {
            return this.verticalAlign;
        }

        public void setYOffset(int yOffset)
        {
            this.yOffset = yOffset;
        }

        public int getYOffset()
        {
            return this.yOffset;
        }

        public void setXOffset(int xOffset)
        {
            this.xOffset = xOffset;
        }

        public virtual int getXOffset()
        {
            return this.xOffset;
        }


        public void setUnderlined(bool ul)
        {
            this.underlined = ul;
        }

        public bool getUnderlined()
        {
            return this.underlined;
        }

        public void setOverlined(bool ol)
        {
            this.overlined = ol;
        }

        public bool getOverlined()
        {
            return this.overlined;
        }

        public void setLineThrough(bool lt)
        {
            this.lineThrough = lt;
        }

        public bool getLineThrough()
        {
            return this.lineThrough;
        }

    }
}