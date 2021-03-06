﻿//Apache2, 2017, WinterDev
//Apache2, 2009, griffm, FO.NET
using System;
using System.Text;

namespace Fonet.Pdf
{
    public class PdfVersion
    {

        //TODO: implement version 1.7
        //www.adobe.com/content/dam/Adobe/en/devnet/acrobat/pdfs/PDF32000_2008.pdf
        //-----------------

        public static readonly PdfVersion V14 = new PdfVersion(1, 4);
        public static readonly PdfVersion V13 = new PdfVersion(1, 3);
        public static readonly PdfVersion V12 = new PdfVersion(1, 2);
        public static readonly PdfVersion V11 = new PdfVersion(1, 1);
        public static readonly PdfVersion V10 = new PdfVersion(1, 0);

        private byte major;

        private byte minor;

        private byte[] header;

        private PdfVersion(byte major, byte minor)
        {
            this.major = major;
            this.minor = minor;
        }

        internal byte[] Header
        {
            get
            {
                if (header == null)
                {
                    header = Encoding.ASCII.GetBytes(
                        String.Format("%PDF-{0}.{1}", major, minor));
                }
                return header;
            }
        }

        public byte Major
        {
            get { return major; }
        }

        public byte Minor
        {
            get { return minor; }
        }
    }
}