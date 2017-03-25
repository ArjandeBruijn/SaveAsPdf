﻿//Apache2, 2017, WinterDev
//Apache2, 2009, griffm, FO.NET
using System.Collections;

namespace Fonet.Render.Pdf
{
    public class CodePointMapping
    {
        private static Hashtable mappings = new Hashtable();

        private string name;
        private ushort[] latin1Map;
        private ushort[] characters;
        private ushort[] codepoints;

        private CodePointMapping(string name, int[] table)
        {
            this.name = name;
            int nonLatin1 = 0;
            latin1Map = new ushort[256];
            for (int i = 0; i < table.Length; i += 2)
            {
                if (table[i + 1] < 256)
                {
                    latin1Map[table[i + 1]] = (char)table[i];
                }
                else
                {
                    ++nonLatin1;
                }
            }
            characters = new ushort[nonLatin1];
            codepoints = new ushort[nonLatin1];
            int top = 0;
            for (int i = 0; i < table.Length; i += 2)
            {
                char c = (char)table[i + 1];
                if (c >= 256)
                {
                    ++top;
                    for (int j = top - 1; j >= 0; --j)
                    {
                        if (j > 0 && characters[j - 1] >= c)
                        {
                            characters[j] = characters[j - 1];
                            codepoints[j] = codepoints[j - 1];
                        }
                        else
                        {
                            characters[j] = c;
                            codepoints[j] = (char)table[i];
                            break;
                        }
                    }
                }
            }
        }

        public ushort MapCharacter(char c)
        {
            if (c < 256)
            {
                return latin1Map[c];
            }
            else
            {
                int bot = 0, top = characters.Length - 1;
                while (top >= bot)
                {
                    int mid = (bot + top) / 2;
                    ushort mc = characters[mid];

                    if (c == mc)
                    {
                        return codepoints[mid];
                    }
                    else if (c < mc)
                    {
                        top = mid - 1;
                    }
                    else
                    {
                        bot = mid + 1;
                    }
                }
                return (ushort)0;
            }
        }

        public string Name
        {
            get { return name; }
        }

        public static CodePointMapping GetMapping(string encoding)
        {
            CodePointMapping mapping = (CodePointMapping)mappings[encoding];
            if (mapping != null)
            {
                return mapping;

            }
            else if (encoding.Equals("StandardEncoding"))
            {
                mapping = new CodePointMapping("StandardEncoding", encStandardEncoding);
                mappings["StandardEncoding"] = mapping;
                return mapping;

            }
            else if (encoding.Equals("ISOLatin1Encoding"))
            {
                mapping = new CodePointMapping("ISOLatin1Encoding", encISOLatin1Encoding);
                mappings["ISOLatin1Encoding"] = mapping;
                return mapping;

            }
            else if (encoding.Equals("CEEncoding"))
            {
                mapping = new CodePointMapping("CEEncoding", encCEEncoding);
                mappings["CEEncoding"] = mapping;
                return mapping;

            }
            else if (encoding.Equals("MacRomanEncoding"))
            {
                mapping = new CodePointMapping("MacRomanEncoding", encMacRomanEncoding);
                mappings["MacRomanEncoding"] = mapping;
                return mapping;

            }
            else if (encoding.Equals("WinAnsiEncoding"))
            {
                mapping = new CodePointMapping("WinAnsiEncoding", encWinAnsiEncoding);
                mappings["WinAnsiEncoding"] = mapping;
                return mapping;

            }
            else if (encoding.Equals("PDFDocEncoding"))
            {
                mapping = new CodePointMapping("PDFDocEncoding", encPDFDocEncoding);
                mappings["PDFDocEncoding"] = mapping;
                return mapping;

            }
            else if (encoding.Equals("SymbolEncoding"))
            {
                mapping = new CodePointMapping("SymbolEncoding", encSymbolEncoding);
                mappings["SymbolEncoding"] = mapping;
                return mapping;

            }
            else if (encoding.Equals("ZapfDingbatsEncoding"))
            {
                mapping = new CodePointMapping("ZapfDingbatsEncoding", encZapfDingbatsEncoding);
                mappings["ZapfDingbatsEncoding"] = mapping;
                return mapping;

            }
            else
            {
                return null;
            }
        }


        private static readonly int[] encStandardEncoding
            = {
                0x20, 0x0020, // space
                0x20, 0x00A0, // space
                0x21, 0x0021, // exclam
                0x22, 0x0022, // quotedbl
                0x23, 0x0023, // numbersign
                0x24, 0x0024, // dollar
                0x25, 0x0025, // percent
                0x26, 0x0026, // ampersand
                0x27, 0x2019, // quoteright
                0x28, 0x0028, // parenleft
                0x29, 0x0029, // parenright
                0x2a, 0x002A, // asterisk
                0x2b, 0x002B, // plus
                0x2c, 0x002C, // comma
                0x2d, 0x002D, // hyphen
                0x2d, 0x00AD, // hyphen
                0x2e, 0x002E, // period
                0x2f, 0x002F, // slash
                0x30, 0x0030, // zero
                0x31, 0x0031, // one
                0x32, 0x0032, // two
                0x33, 0x0033, // three
                0x34, 0x0034, // four
                0x35, 0x0035, // five
                0x36, 0x0036, // six
                0x37, 0x0037, // seven
                0x38, 0x0038, // eight
                0x39, 0x0039, // nine
                0x3a, 0x003A, // colon
                0x3b, 0x003B, // semicolon
                0x3c, 0x003C, // less
                0x3d, 0x003D, // equal
                0x3e, 0x003E, // greater
                0x3f, 0x003F, // question
                0x40, 0x0040, // at
                0x41, 0x0041, // A
                0x42, 0x0042, // B
                0x43, 0x0043, // C
                0x44, 0x0044, // D
                0x45, 0x0045, // E
                0x46, 0x0046, // F
                0x47, 0x0047, // G
                0x48, 0x0048, // H
                0x49, 0x0049, // I
                0x4a, 0x004A, // J
                0x4b, 0x004B, // K
                0x4c, 0x004C, // L
                0x4d, 0x004D, // M
                0x4e, 0x004E, // N
                0x4f, 0x004F, // O
                0x50, 0x0050, // P
                0x51, 0x0051, // Q
                0x52, 0x0052, // R
                0x53, 0x0053, // S
                0x54, 0x0054, // T
                0x55, 0x0055, // U
                0x56, 0x0056, // V
                0x57, 0x0057, // W
                0x58, 0x0058, // X
                0x59, 0x0059, // Y
                0x5a, 0x005A, // Z
                0x5b, 0x005B, // bracketleft
                0x5c, 0x005C, // backslash
                0x5d, 0x005D, // bracketright
                0x5e, 0x005E, // asciicircum
                0x5f, 0x005F, // underscore
                0x60, 0x2018, // quoteleft
                0x61, 0x0061, // a
                0x62, 0x0062, // b
                0x63, 0x0063, // c
                0x64, 0x0064, // d
                0x65, 0x0065, // e
                0x66, 0x0066, // f
                0x67, 0x0067, // g
                0x68, 0x0068, // h
                0x69, 0x0069, // i
                0x6a, 0x006A, // j
                0x6b, 0x006B, // k
                0x6c, 0x006C, // l
                0x6d, 0x006D, // m
                0x6e, 0x006E, // n
                0x6f, 0x006F, // o
                0x70, 0x0070, // p
                0x71, 0x0071, // q
                0x72, 0x0072, // r
                0x73, 0x0073, // s
                0x74, 0x0074, // t
                0x75, 0x0075, // u
                0x76, 0x0076, // v
                0x77, 0x0077, // w
                0x78, 0x0078, // x
                0x79, 0x0079, // y
                0x7a, 0x007A, // z
                0x7b, 0x007B, // braceleft
                0x7c, 0x007C, // bar
                0x7d, 0x007D, // braceright
                0x7e, 0x007E, // asciitilde
                0xa1, 0x00A1, // exclamdown
                0xa2, 0x00A2, // cent
                0xa3, 0x00A3, // sterling
                0xa4, 0x2044, // fraction
                0xa4, 0x2215, // fraction
                0xa5, 0x00A5, // yen
                0xa6, 0x0192, // florin
                0xa7, 0x00A7, // section
                0xa8, 0x00A4, // currency
                0xa9, 0x0027, // quotesingle
                0xaa, 0x201C, // quotedblleft
                0xab, 0x00AB, // guillemotleft
                0xac, 0x2039, // guilsinglleft
                0xad, 0x203A, // guilsinglright
                0xae, 0xFB01, // fi
                0xaf, 0xFB02, // fl
                0xb1, 0x2013, // endash
                0xb2, 0x2020, // dagger
                0xb3, 0x2021, // daggerdbl
                0xb4, 0x00B7, // periodcentered
                0xb4, 0x2219, // periodcentered
                0xb6, 0x00B6, // paragraph
                0xb7, 0x2022, // bullet
                0xb8, 0x201A, // quotesinglbase
                0xb9, 0x201E, // quotedblbase
                0xba, 0x201D, // quotedblright
                0xbb, 0x00BB, // guillemotright
                0xbc, 0x2026, // ellipsis
                0xbd, 0x2030, // perthousand
                0xbf, 0x00BF, // questiondown
                0xc1, 0x0060, // grave
                0xc2, 0x00B4, // acute
                0xc3, 0x02C6, // circumflex
                0xc4, 0x02DC, // tilde
                0xc5, 0x00AF, // macron
                0xc5, 0x02C9, // macron
                0xc6, 0x02D8, // breve
                0xc7, 0x02D9, // dotaccent
                0xc8, 0x00A8, // dieresis
                0xca, 0x02DA, // ring
                0xcb, 0x00B8, // cedilla
                0xcd, 0x02DD, // hungarumlaut
                0xce, 0x02DB, // ogonek
                0xcf, 0x02C7, // caron
                0xd0, 0x2014, // emdash
                0xe1, 0x00C6, // AE
                0xe3, 0x00AA, // ordfeminine
                0xe8, 0x0141, // Lslash
                0xe9, 0x00D8, // Oslash
                0xea, 0x0152, // OE
                0xeb, 0x00BA, // ordmasculine
                0xf1, 0x00E6, // ae
                0xf5, 0x0131, // dotlessi
                0xf8, 0x0142, // lslash
                0xf9, 0x00F8, // oslash
                0xfa, 0x0153, // oe
                0xfb, 0x00DF, // germandbls
            };


        private static readonly int[] encISOLatin1Encoding
            = {
                0x20, 0x0020, // space
                0x20, 0x00A0, // space
                0x21, 0x0021, // exclam
                0x22, 0x0022, // quotedbl
                0x23, 0x0023, // numbersign
                0x24, 0x0024, // dollar
                0x25, 0x0025, // percent
                0x26, 0x0026, // ampersand
                0x27, 0x2019, // quoteright
                0x28, 0x0028, // parenleft
                0x29, 0x0029, // parenright
                0x2a, 0x002A, // asterisk
                0x2b, 0x002B, // plus
                0x2c, 0x002C, // comma
                0x2d, 0x2212, // minus
                0x2e, 0x002E, // period
                0x2f, 0x002F, // slash
                0x30, 0x0030, // zero
                0x31, 0x0031, // one
                0x32, 0x0032, // two
                0x33, 0x0033, // three
                0x34, 0x0034, // four
                0x35, 0x0035, // five
                0x36, 0x0036, // six
                0x37, 0x0037, // seven
                0x38, 0x0038, // eight
                0x39, 0x0039, // nine
                0x3a, 0x003A, // colon
                0x3b, 0x003B, // semicolon
                0x3c, 0x003C, // less
                0x3d, 0x003D, // equal
                0x3e, 0x003E, // greater
                0x3f, 0x003F, // question
                0x40, 0x0040, // at
                0x41, 0x0041, // A
                0x42, 0x0042, // B
                0x43, 0x0043, // C
                0x44, 0x0044, // D
                0x45, 0x0045, // E
                0x46, 0x0046, // F
                0x47, 0x0047, // G
                0x48, 0x0048, // H
                0x49, 0x0049, // I
                0x4a, 0x004A, // J
                0x4b, 0x004B, // K
                0x4c, 0x004C, // L
                0x4d, 0x004D, // M
                0x4e, 0x004E, // N
                0x4f, 0x004F, // O
                0x50, 0x0050, // P
                0x51, 0x0051, // Q
                0x52, 0x0052, // R
                0x53, 0x0053, // S
                0x54, 0x0054, // T
                0x55, 0x0055, // U
                0x56, 0x0056, // V
                0x57, 0x0057, // W
                0x58, 0x0058, // X
                0x59, 0x0059, // Y
                0x5a, 0x005A, // Z
                0x5b, 0x005B, // bracketleft
                0x5c, 0x005C, // backslash
                0x5d, 0x005D, // bracketright
                0x5e, 0x005E, // asciicircum
                0x5f, 0x005F, // underscore
                0x60, 0x2018, // quoteleft
                0x61, 0x0061, // a
                0x62, 0x0062, // b
                0x63, 0x0063, // c
                0x64, 0x0064, // d
                0x65, 0x0065, // e
                0x66, 0x0066, // f
                0x67, 0x0067, // g
                0x68, 0x0068, // h
                0x69, 0x0069, // i
                0x6a, 0x006A, // j
                0x6b, 0x006B, // k
                0x6c, 0x006C, // l
                0x6d, 0x006D, // m
                0x6e, 0x006E, // n
                0x6f, 0x006F, // o
                0x70, 0x0070, // p
                0x71, 0x0071, // q
                0x72, 0x0072, // r
                0x73, 0x0073, // s
                0x74, 0x0074, // t
                0x75, 0x0075, // u
                0x76, 0x0076, // v
                0x77, 0x0077, // w
                0x78, 0x0078, // x
                0x79, 0x0079, // y
                0x7a, 0x007A, // z
                0x7b, 0x007B, // braceleft
                0x7c, 0x007C, // bar
                0x7d, 0x007D, // braceright
                0x7e, 0x007E, // asciitilde
                0x90, 0x0131, // dotlessi
                0x91, 0x0060, // grave
                0x93, 0x02C6, // circumflex
                0x94, 0x02DC, // tilde
                0x96, 0x02D8, // breve
                0x97, 0x02D9, // dotaccent
                0x9a, 0x02DA, // ring
                0x9d, 0x02DD, // hungarumlaut
                0x9e, 0x02DB, // ogonek
                0x9f, 0x02C7, // caron
                0xa1, 0x00A1, // exclamdown
                0xa2, 0x00A2, // cent
                0xa3, 0x00A3, // sterling
                0xa4, 0x00A4, // currency
                0xa5, 0x00A5, // yen
                0xa6, 0x00A6, // brokenbar
                0xa7, 0x00A7, // section
                0xa8, 0x00A8, // dieresis
                0xa9, 0x00A9, // copyright
                0xaa, 0x00AA, // ordfeminine
                0xab, 0x00AB, // guillemotleft
                0xac, 0x00AC, // logicalnot
                0xad, 0x002D, // hyphen
                0xad, 0x00AD, // hyphen
                0xae, 0x00AE, // registered
                0xaf, 0x00AF, // macron
                0xaf, 0x02C9, // macron
                0xb0, 0x00B0, // degree
                0xb1, 0x00B1, // plusminus
                0xb2, 0x00B2, // twosuperior
                0xb3, 0x00B3, // threesuperior
                0xb4, 0x00B4, // acute
                0xb5, 0x00B5, // mu
                0xb5, 0x03BC, // mu
                0xb6, 0x00B6, // paragraph
                0xb7, 0x00B7, // periodcentered
                0xb7, 0x2219, // periodcentered
                0xb8, 0x00B8, // cedilla
                0xb9, 0x00B9, // onesuperior
                0xba, 0x00BA, // ordmasculine
                0xbb, 0x00BB, // guillemotright
                0xbc, 0x00BC, // onequarter
                0xbd, 0x00BD, // onehalf
                0xbe, 0x00BE, // threequarters
                0xbf, 0x00BF, // questiondown
                0xc0, 0x00C0, // Agrave
                0xc1, 0x00C1, // Aacute
                0xc2, 0x00C2, // Acircumflex
                0xc3, 0x00C3, // Atilde
                0xc4, 0x00C4, // Adieresis
                0xc5, 0x00C5, // Aring
                0xc6, 0x00C6, // AE
                0xc7, 0x00C7, // Ccedilla
                0xc8, 0x00C8, // Egrave
                0xc9, 0x00C9, // Eacute
                0xca, 0x00CA, // Ecircumflex
                0xcb, 0x00CB, // Edieresis
                0xcc, 0x00CC, // Igrave
                0xcd, 0x00CD, // Iacute
                0xce, 0x00CE, // Icircumflex
                0xcf, 0x00CF, // Idieresis
                0xd0, 0x00D0, // Eth
                0xd1, 0x00D1, // Ntilde
                0xd2, 0x00D2, // Ograve
                0xd3, 0x00D3, // Oacute
                0xd4, 0x00D4, // Ocircumflex
                0xd5, 0x00D5, // Otilde
                0xd6, 0x00D6, // Odieresis
                0xd7, 0x00D7, // multiply
                0xd8, 0x00D8, // Oslash
                0xd9, 0x00D9, // Ugrave
                0xda, 0x00DA, // Uacute
                0xdb, 0x00DB, // Ucircumflex
                0xdc, 0x00DC, // Udieresis
                0xdd, 0x00DD, // Yacute
                0xde, 0x00DE, // Thorn
                0xdf, 0x00DF, // germandbls
                0xe0, 0x00E0, // agrave
                0xe1, 0x00E1, // aacute
                0xe2, 0x00E2, // acircumflex
                0xe3, 0x00E3, // atilde
                0xe4, 0x00E4, // adieresis
                0xe5, 0x00E5, // aring
                0xe6, 0x00E6, // ae
                0xe7, 0x00E7, // ccedilla
                0xe8, 0x00E8, // egrave
                0xe9, 0x00E9, // eacute
                0xea, 0x00EA, // ecircumflex
                0xeb, 0x00EB, // edieresis
                0xec, 0x00EC, // igrave
                0xed, 0x00ED, // iacute
                0xee, 0x00EE, // icircumflex
                0xef, 0x00EF, // idieresis
                0xf0, 0x00F0, // eth
                0xf1, 0x00F1, // ntilde
                0xf2, 0x00F2, // ograve
                0xf3, 0x00F3, // oacute
                0xf4, 0x00F4, // ocircumflex
                0xf5, 0x00F5, // otilde
                0xf6, 0x00F6, // odieresis
                0xf7, 0x00F7, // divide
                0xf8, 0x00F8, // oslash
                0xf9, 0x00F9, // ugrave
                0xfa, 0x00FA, // uacute
                0xfb, 0x00FB, // ucircumflex
                0xfc, 0x00FC, // udieresis
                0xfd, 0x00FD, // yacute
                0xfe, 0x00FE, // thorn
                0xff, 0x00FF, // ydieresis
            };


        private static readonly int[] encCEEncoding
            = {
                0x20, 0x0020, // space
                0x20, 0x00A0, // space
                0x21, 0x0021, // exclam
                0x22, 0x0022, // quotedbl
                0x23, 0x0023, // numbersign
                0x24, 0x0024, // dollar
                0x25, 0x0025, // percent
                0x26, 0x0026, // ampersand
                0x27, 0x0027, // quotesingle
                0x28, 0x0028, // parenleft
                0x29, 0x0029, // parenright
                0x2a, 0x002A, // asterisk
                0x2b, 0x002B, // plus
                0x2c, 0x002C, // comma
                0x2d, 0x002D, // hyphen
                0x2d, 0x00AD, // hyphen
                0x2e, 0x002E, // period
                0x2f, 0x002F, // slash
                0x30, 0x0030, // zero
                0x31, 0x0031, // one
                0x32, 0x0032, // two
                0x33, 0x0033, // three
                0x34, 0x0034, // four
                0x35, 0x0035, // five
                0x36, 0x0036, // six
                0x37, 0x0037, // seven
                0x38, 0x0038, // eight
                0x39, 0x0039, // nine
                0x3a, 0x003A, // colon
                0x3b, 0x003B, // semicolon
                0x3c, 0x003C, // less
                0x3d, 0x003D, // equal
                0x3e, 0x003E, // greater
                0x3f, 0x003F, // question
                0x40, 0x0040, // at
                0x41, 0x0041, // A
                0x42, 0x0042, // B
                0x43, 0x0043, // C
                0x44, 0x0044, // D
                0x45, 0x0045, // E
                0x46, 0x0046, // F
                0x47, 0x0047, // G
                0x48, 0x0048, // H
                0x49, 0x0049, // I
                0x4a, 0x004A, // J
                0x4b, 0x004B, // K
                0x4c, 0x004C, // L
                0x4d, 0x004D, // M
                0x4e, 0x004E, // N
                0x4f, 0x004F, // O
                0x50, 0x0050, // P
                0x51, 0x0051, // Q
                0x52, 0x0052, // R
                0x53, 0x0053, // S
                0x54, 0x0054, // T
                0x55, 0x0055, // U
                0x56, 0x0056, // V
                0x57, 0x0057, // W
                0x58, 0x0058, // X
                0x59, 0x0059, // Y
                0x5a, 0x005A, // Z
                0x5b, 0x005B, // bracketleft
                0x5c, 0x005C, // backslash
                0x5d, 0x005D, // bracketright
                0x5e, 0x005E, // asciicircum
                0x5f, 0x005F, // underscore
                0x60, 0x0060, // grave
                0x61, 0x0061, // a
                0x62, 0x0062, // b
                0x63, 0x0063, // c
                0x64, 0x0064, // d
                0x65, 0x0065, // e
                0x66, 0x0066, // f
                0x67, 0x0067, // g
                0x68, 0x0068, // h
                0x69, 0x0069, // i
                0x6a, 0x006A, // j
                0x6b, 0x006B, // k
                0x6c, 0x006C, // l
                0x6d, 0x006D, // m
                0x6e, 0x006E, // n
                0x6f, 0x006F, // o
                0x70, 0x0070, // p
                0x71, 0x0071, // q
                0x72, 0x0072, // r
                0x73, 0x0073, // s
                0x74, 0x0074, // t
                0x75, 0x0075, // u
                0x76, 0x0076, // v
                0x77, 0x0077, // w
                0x78, 0x0078, // x
                0x79, 0x0079, // y
                0x7a, 0x007A, // z
                0x7b, 0x007B, // braceleft
                0x7c, 0x007C, // bar
                0x7d, 0x007D, // braceright
                0x7e, 0x007E, // asciitilde
                0x82, 0x201A, // quotesinglbase
                0x84, 0x201E, // quotedblbase
                0x85, 0x2026, // ellipsis
                0x86, 0x2020, // dagger
                0x87, 0x2021, // daggerdbl
                0x89, 0x2030, // perthousand
                0x8a, 0x0160, // Scaron
                0x8b, 0x2039, // guilsinglleft
                0x8c, 0x015A, // Sacute
                0x8d, 0x0164, // Tcaron
                0x8e, 0x017D, // Zcaron
                0x8f, 0x0179, // Zacute
                0x91, 0x2018, // quoteleft
                0x92, 0x2019, // quoteright
                0x93, 0x201C, // quotedblleft
                0x94, 0x201D, // quotedblright
                0x95, 0x2022, // bullet
                0x96, 0x2013, // endash
                0x97, 0x2014, // emdash
                0x99, 0x2122, // trademark
                0x9a, 0x0161, // scaron
                0x9b, 0x203A, // guilsinglright
                0x9c, 0x015B, // sacute
                0x9d, 0x0165, // tcaron
                0x9e, 0x017E, // zcaron
                0x9f, 0x017A, // zacute
                0xa1, 0x02C7, // caron
                0xa2, 0x02D8, // breve
                0xa3, 0x0141, // Lslash
                0xa4, 0x00A4, // currency
                0xa5, 0x0104, // Aogonek
                0xa6, 0x00A6, // brokenbar
                0xa7, 0x00A7, // section
                0xa8, 0x00A8, // dieresis
                0xa9, 0x00A9, // copyright
                0xaa, 0x0218, // Scommaaccent
                0xab, 0x00AB, // guillemotleft
                0xac, 0x00AC, // logicalnot
                0xae, 0x00AE, // registered
                0xaf, 0x017B, // Zdotaccent
                0xb0, 0x00B0, // degree
                0xb1, 0x00B1, // plusminus
                0xb2, 0x02DB, // ogonek
                0xb3, 0x0142, // lslash
                0xb4, 0x00B4, // acute
                0xb5, 0x00B5, // mu
                0xb5, 0x03BC, // mu
                0xb6, 0x00B6, // paragraph
                0xb7, 0x00B7, // periodcentered
                0xb7, 0x2219, // periodcentered
                0xb8, 0x00B8, // cedilla
                0xb9, 0x0105, // aogonek
                0xba, 0x0219, // scommaaccent
                0xbb, 0x00BB, // guillemotright
                0xbc, 0x013D, // Lcaron
                0xbd, 0x02DD, // hungarumlaut
                0xbe, 0x013E, // lcaron
                0xbf, 0x017C, // zdotaccent
                0xc0, 0x0154, // Racute
                0xc1, 0x00C1, // Aacute
                0xc2, 0x00C2, // Acircumflex
                0xc3, 0x0102, // Abreve
                0xc4, 0x00C4, // Adieresis
                0xc5, 0x0139, // Lacute
                0xc6, 0x0106, // Cacute
                0xc7, 0x00C7, // Ccedilla
                0xc8, 0x010C, // Ccaron
                0xc9, 0x00C9, // Eacute
                0xca, 0x0118, // Eogonek
                0xcb, 0x00CB, // Edieresis
                0xcc, 0x011A, // Ecaron
                0xcd, 0x00CD, // Iacute
                0xce, 0x00CE, // Icircumflex
                0xcf, 0x010E, // Dcaron
                0xd0, 0x0110, // Dcroat
                0xd1, 0x0143, // Nacute
                0xd2, 0x0147, // Ncaron
                0xd3, 0x00D3, // Oacute
                0xd4, 0x00D4, // Ocircumflex
                0xd5, 0x0150, // Ohungarumlaut
                0xd6, 0x00D6, // Odieresis
                0xd7, 0x00D7, // multiply
                0xd8, 0x0158, // Rcaron
                0xd9, 0x016E, // Uring
                0xda, 0x00DA, // Uacute
                0xdb, 0x0170, // Uhungarumlaut
                0xdc, 0x00DC, // Udieresis
                0xdd, 0x00DD, // Yacute
                0xde, 0x0162, // Tcommaaccent
                0xde, 0x021A, // Tcommaaccent
                0xdf, 0x00DF, // germandbls
                0xe0, 0x0155, // racute
                0xe1, 0x00E1, // aacute
                0xe2, 0x00E2, // acircumflex
                0xe3, 0x0103, // abreve
                0xe4, 0x00E4, // adieresis
                0xe5, 0x013A, // lacute
                0xe6, 0x0107, // cacute
                0xe7, 0x00E7, // ccedilla
                0xe8, 0x010D, // ccaron
                0xe9, 0x00E9, // eacute
                0xea, 0x0119, // eogonek
                0xeb, 0x00EB, // edieresis
                0xec, 0x011B, // ecaron
                0xed, 0x00ED, // iacute
                0xee, 0x00EE, // icircumflex
                0xef, 0x010F, // dcaron
                0xf0, 0x0111, // dcroat
                0xf1, 0x0144, // nacute
                0xf2, 0x0148, // ncaron
                0xf3, 0x00F3, // oacute
                0xf4, 0x00F4, // ocircumflex
                0xf5, 0x0151, // ohungarumlaut
                0xf6, 0x00F6, // odieresis
                0xf7, 0x00F7, // divide
                0xf8, 0x0159, // rcaron
                0xf9, 0x016F, // uring
                0xfa, 0x00FA, // uacute
                0xfb, 0x0171, // uhungarumlaut
                0xfc, 0x00FC, // udieresis
                0xfd, 0x00FD, // yacute
                0xfe, 0x0163, // tcommaaccent
                0xfe, 0x021B, // tcommaaccent
                0xff, 0x02D9, // dotaccent
            };


        private static readonly int[] encMacRomanEncoding
            = {
                0x20, 0x0020, // space
                0x20, 0x00A0, // space
                0x21, 0x0021, // exclam
                0x22, 0x0022, // quotedbl
                0x23, 0x0023, // numbersign
                0x24, 0x0024, // dollar
                0x25, 0x0025, // percent
                0x26, 0x0026, // ampersand
                0x27, 0x0027, // quotesingle
                0x28, 0x0028, // parenleft
                0x29, 0x0029, // parenright
                0x2a, 0x002A, // asterisk
                0x2b, 0x002B, // plus
                0x2c, 0x002C, // comma
                0x2d, 0x002D, // hyphen
                0x2d, 0x00AD, // hyphen
                0x2e, 0x002E, // period
                0x2f, 0x002F, // slash
                0x30, 0x0030, // zero
                0x31, 0x0031, // one
                0x32, 0x0032, // two
                0x33, 0x0033, // three
                0x34, 0x0034, // four
                0x35, 0x0035, // five
                0x36, 0x0036, // six
                0x37, 0x0037, // seven
                0x38, 0x0038, // eight
                0x39, 0x0039, // nine
                0x3a, 0x003A, // colon
                0x3b, 0x003B, // semicolon
                0x3c, 0x003C, // less
                0x3d, 0x003D, // equal
                0x3e, 0x003E, // greater
                0x3f, 0x003F, // question
                0x40, 0x0040, // at
                0x41, 0x0041, // A
                0x42, 0x0042, // B
                0x43, 0x0043, // C
                0x44, 0x0044, // D
                0x45, 0x0045, // E
                0x46, 0x0046, // F
                0x47, 0x0047, // G
                0x48, 0x0048, // H
                0x49, 0x0049, // I
                0x4a, 0x004A, // J
                0x4b, 0x004B, // K
                0x4c, 0x004C, // L
                0x4d, 0x004D, // M
                0x4e, 0x004E, // N
                0x4f, 0x004F, // O
                0x50, 0x0050, // P
                0x51, 0x0051, // Q
                0x52, 0x0052, // R
                0x53, 0x0053, // S
                0x54, 0x0054, // T
                0x55, 0x0055, // U
                0x56, 0x0056, // V
                0x57, 0x0057, // W
                0x58, 0x0058, // X
                0x59, 0x0059, // Y
                0x5a, 0x005A, // Z
                0x5b, 0x005B, // bracketleft
                0x5c, 0x005C, // backslash
                0x5d, 0x005D, // bracketright
                0x5e, 0x005E, // asciicircum
                0x5f, 0x005F, // underscore
                0x60, 0x0060, // grave
                0x61, 0x0061, // a
                0x62, 0x0062, // b
                0x63, 0x0063, // c
                0x64, 0x0064, // d
                0x65, 0x0065, // e
                0x66, 0x0066, // f
                0x67, 0x0067, // g
                0x68, 0x0068, // h
                0x69, 0x0069, // i
                0x6a, 0x006A, // j
                0x6b, 0x006B, // k
                0x6c, 0x006C, // l
                0x6d, 0x006D, // m
                0x6e, 0x006E, // n
                0x6f, 0x006F, // o
                0x70, 0x0070, // p
                0x71, 0x0071, // q
                0x72, 0x0072, // r
                0x73, 0x0073, // s
                0x74, 0x0074, // t
                0x75, 0x0075, // u
                0x76, 0x0076, // v
                0x77, 0x0077, // w
                0x78, 0x0078, // x
                0x79, 0x0079, // y
                0x7b, 0x007B, // braceleft
                0x7c, 0x007C, // bar
                0x7d, 0x007D, // braceright
                0x7e, 0x007E, // asciitilde
                0x80, 0x00C4, // Adieresis
                0x81, 0x00C5, // Aring
                0x82, 0x00C7, // Ccedilla
                0x83, 0x00C9, // Eacute
                0x84, 0x00D1, // Ntilde
                0x85, 0x00D6, // Odieresis
                0x86, 0x00DC, // Udieresis
                0x87, 0x00E1, // aacute
                0x88, 0x00E0, // agrave
                0x89, 0x00E2, // acircumflex
                0x8a, 0x00E4, // adieresis
                0x8b, 0x00E3, // atilde
                0x8c, 0x00E5, // aring
                0x8d, 0x00E7, // ccedilla
                0x8e, 0x00E9, // eacute
                0x8f, 0x00E8, // egrave
                0x90, 0x00EA, // ecircumflex
                0x91, 0x00EB, // edieresis
                0x92, 0x00ED, // iacute
                0x93, 0x00EC, // igrave
                0x94, 0x00EE, // icircumflex
                0x95, 0x00EF, // idieresis
                0x96, 0x00F1, // ntilde
                0x97, 0x00F3, // oacute
                0x98, 0x00F2, // ograve
                0x99, 0x00F4, // ocircumflex
                0x9a, 0x00F6, // odieresis
                0x9b, 0x00F5, // otilde
                0x9c, 0x00FA, // uacute
                0x9d, 0x00F9, // ugrave
                0x9e, 0x00FB, // ucircumflex
                0x9f, 0x00FC, // udieresis
                0xa0, 0x2020, // dagger
                0xa1, 0x00B0, // degree
                0xa2, 0x00A2, // cent
                0xa3, 0x00A3, // sterling
                0xa4, 0x00A7, // section
                0xa5, 0x2022, // bullet
                0xa6, 0x00B6, // paragraph
                0xa7, 0x00DF, // germandbls
                0xa8, 0x00AE, // registered
                0xa9, 0x00A9, // copyright
                0xaa, 0x2122, // trademark
                0xab, 0x00B4, // acute
                0xac, 0x00A8, // dieresis
                0xae, 0x00C6, // AE
                0xaf, 0x00D8, // Oslash
                0xb1, 0x00B1, // plusminus
                0xb5, 0x00B5, // mu
                0xb5, 0x03BC, // mu
                0xbb, 0x00AA, // ordfeminine
                0xbc, 0x00BA, // ordmasculine
                0xbe, 0x00E6, // ae
                0xbf, 0x00F8, // oslash
                0xc0, 0x00BF, // questiondown
                0xc1, 0x00A1, // exclamdown
                0xc2, 0x00AC, // logicalnot
                0xc4, 0x0192, // florin
                0xc7, 0x00AB, // guillemotleft
                0xc8, 0x00BB, // guillemotright
                0xc9, 0x2026, // ellipsis
                0xcb, 0x00C0, // Agrave
                0xcc, 0x00C3, // Atilde
                0xcd, 0x00D5, // Otilde
                0xce, 0x0152, // OE
                0xcf, 0x0153, // oe
                0xd0, 0x2013, // endash
                0xd1, 0x2014, // emdash
                0xd2, 0x201C, // quotedblleft
                0xd3, 0x201D, // quotedblright
                0xd4, 0x2018, // quoteleft
                0xd5, 0x2019, // quoteright
                0xd6, 0x00F7, // divide
                0xd9, 0x0178, // Ydieresis
                0xda, 0x2044, // fraction
                0xda, 0x2215, // fraction
                0xdb, 0x00A4, // currency
                0xdc, 0x2039, // guilsinglleft
                0xdd, 0x203A, // guilsinglright
                0xde, 0xFB01, // fi
                0xdf, 0xFB02, // fl
                0xe0, 0x2021, // daggerdbl
                0xe1, 0x00B7, // periodcentered
                0xe1, 0x2219, // periodcentered
                0xe2, 0x201A, // quotesinglbase
                0xe3, 0x201E, // quotedblbase
                0xe4, 0x2030, // perthousand
                0xe5, 0x00C2, // Acircumflex
                0xe6, 0x00CA, // Ecircumflex
                0xe7, 0x00C1, // Aacute
                0xe8, 0x00CB, // Edieresis
                0xe9, 0x00C8, // Egrave
                0xea, 0x00CD, // Iacute
                0xeb, 0x00CE, // Icircumflex
                0xec, 0x00CF, // Idieresis
                0xed, 0x00CC, // Igrave
                0xee, 0x00D3, // Oacute
                0xef, 0x00D4, // Ocircumflex
                0xf1, 0x00D2, // Ograve
                0xf2, 0x00DA, // Uacute
                0xf3, 0x00DB, // Ucircumflex
                0xf4, 0x00D9, // Ugrave
                0xf5, 0x0131, // dotlessi
                0xf6, 0x02C6, // circumflex
                0xf7, 0x02DC, // tilde
                0xf8, 0x00AF, // macron
                0xf8, 0x02C9, // macron
                0xf9, 0x02D8, // breve
                0xfa, 0x02D9, // dotaccent
                0xfb, 0x02DA, // ring
                0xfc, 0x00B8, // cedilla
                0xfd, 0x02DD, // hungarumlaut
                0xfe, 0x02DB, // ogonek
                0xff, 0x02C7, // caron
                0xd8, 0x00FF, // ydieresis
                0xb4, 0x00A5, // yen
                0x7a, 0x007A, // z
            };


        private static readonly int[] encWinAnsiEncoding
            = {
                0x20, 0x0020, // space
                0x20, 0x00A0, // space
                0x21, 0x0021, // exclam
                0x22, 0x0022, // quotedbl
                0x23, 0x0023, // numbersign
                0x24, 0x0024, // dollar
                0x25, 0x0025, // percent
                0x26, 0x0026, // ampersand
                0x27, 0x0027, // quotesingle
                0x28, 0x0028, // parenleft
                0x29, 0x0029, // parenright
                0x2a, 0x002A, // asterisk
                0x2b, 0x002B, // plus
                0x2c, 0x002C, // comma
                0x2d, 0x002D, // hyphen
                0x2d, 0x00AD, // hyphen
                0x2e, 0x002E, // period
                0x2f, 0x002F, // slash
                0x30, 0x0030, // zero
                0x31, 0x0031, // one
                0x32, 0x0032, // two
                0x33, 0x0033, // three
                0x34, 0x0034, // four
                0x35, 0x0035, // five
                0x36, 0x0036, // six
                0x37, 0x0037, // seven
                0x38, 0x0038, // eight
                0x39, 0x0039, // nine
                0x3a, 0x003A, // colon
                0x3b, 0x003B, // semicolon
                0x3c, 0x003C, // less
                0x3d, 0x003D, // equal
                0x3e, 0x003E, // greater
                0x3f, 0x003F, // question
                0x40, 0x0040, // at
                0x41, 0x0041, // A
                0x42, 0x0042, // B
                0x43, 0x0043, // C
                0x44, 0x0044, // D
                0x45, 0x0045, // E
                0x46, 0x0046, // F
                0x47, 0x0047, // G
                0x48, 0x0048, // H
                0x49, 0x0049, // I
                0x4a, 0x004A, // J
                0x4b, 0x004B, // K
                0x4c, 0x004C, // L
                0x4d, 0x004D, // M
                0x4e, 0x004E, // N
                0x4f, 0x004F, // O
                0x50, 0x0050, // P
                0x51, 0x0051, // Q
                0x52, 0x0052, // R
                0x53, 0x0053, // S
                0x54, 0x0054, // T
                0x55, 0x0055, // U
                0x56, 0x0056, // V
                0x57, 0x0057, // W
                0x58, 0x0058, // X
                0x59, 0x0059, // Y
                0x5a, 0x005A, // Z
                0x5b, 0x005B, // bracketleft
                0x5c, 0x005C, // backslash
                0x5d, 0x005D, // bracketright
                0x5e, 0x005E, // asciicircum
                0x5f, 0x005F, // underscore
                0x60, 0x0060, // grave
                0x61, 0x0061, // a
                0x62, 0x0062, // b
                0x63, 0x0063, // c
                0x64, 0x0064, // d
                0x65, 0x0065, // e
                0x66, 0x0066, // f
                0x67, 0x0067, // g
                0x68, 0x0068, // h
                0x69, 0x0069, // i
                0x6a, 0x006A, // j
                0x6b, 0x006B, // k
                0x6c, 0x006C, // l
                0x6d, 0x006D, // m
                0x6e, 0x006E, // n
                0x6f, 0x006F, // o
                0x70, 0x0070, // p
                0x71, 0x0071, // q
                0x72, 0x0072, // r
                0x73, 0x0073, // s
                0x74, 0x0074, // t
                0x75, 0x0075, // u
                0x76, 0x0076, // v
                0x77, 0x0077, // w
                0x78, 0x0078, // x
                0x79, 0x0079, // y
                0x7a, 0x007A, // z
                0x7b, 0x007B, // braceleft
                0x7c, 0x007C, // bar
                0x7d, 0x007D, // braceright
                0x7e, 0x007E, // asciitilde
                0x80, 0x20AC, // Euro
                0x82, 0x201A, // quotesinglbase
                0x83, 0x0192, // florin
                0x84, 0x201E, // quotedblbase
                0x85, 0x2026, // ellipsis
                0x86, 0x2020, // dagger
                0x87, 0x2021, // daggerdbl
                0x88, 0x02C6, // circumflex
                0x89, 0x2030, // perthousand
                0x8a, 0x0160, // Scaron
                0x8b, 0x2039, // guilsinglleft
                0x8c, 0x0152, // OE
                0x8e, 0x017D, // Zcaron
                0x91, 0x2018, // quoteleft
                0x92, 0x2019, // quoteright
                0x93, 0x201C, // quotedblleft
                0x94, 0x201D, // quotedblright
                0x95, 0x2022, // bullet
                0x96, 0x2013, // endash
                0x97, 0x2014, // emdash
                0x98, 0x02DC, // tilde
                0x99, 0x2122, // trademark
                0x9a, 0x0161, // scaron
                0x9b, 0x203A, // guilsinglright
                0x9c, 0x0153, // oe
                0x9e, 0x017E, // zcaron
                0x9f, 0x0178, // Ydieresis
                0xa1, 0x00A1, // exclamdown
                0xa2, 0x00A2, // cent
                0xa3, 0x00A3, // sterling
                0xa4, 0x00A4, // currency
                0xa5, 0x00A5, // yen
                0xa6, 0x00A6, // brokenbar
                0xa7, 0x00A7, // section
                0xa8, 0x00A8, // dieresis
                0xa9, 0x00A9, // copyright
                0xaa, 0x00AA, // ordfeminine
                0xab, 0x00AB, // guillemotleft
                0xac, 0x00AC, // logicalnot
                0xae, 0x00AE, // registered
                0xaf, 0x00AF, // macron
                0xaf, 0x02C9, // macron
                0xb0, 0x00B0, // degree
                0xb1, 0x00B1, // plusminus
                0xb2, 0x00B2, // twosuperior
                0xb3, 0x00B3, // threesuperior
                0xb4, 0x00B4, // acute
                0xb5, 0x00B5, // mu
                0xb5, 0x03BC, // mu
                0xb6, 0x00B6, // paragraph
                0xb7, 0x00B7, // periodcentered
                0xb7, 0x2219, // periodcentered
                0xb8, 0x00B8, // cedilla
                0xb9, 0x00B9, // onesuperior
                0xba, 0x00BA, // ordmasculine
                0xbb, 0x00BB, // guillemotright
                0xbc, 0x00BC, // onequarter
                0xbd, 0x00BD, // onehalf
                0xbe, 0x00BE, // threequarters
                0xbf, 0x00BF, // questiondown
                0xc0, 0x00C0, // Agrave
                0xc1, 0x00C1, // Aacute
                0xc2, 0x00C2, // Acircumflex
                0xc3, 0x00C3, // Atilde
                0xc4, 0x00C4, // Adieresis
                0xc5, 0x00C5, // Aring
                0xc6, 0x00C6, // AE
                0xc7, 0x00C7, // Ccedilla
                0xc8, 0x00C8, // Egrave
                0xc9, 0x00C9, // Eacute
                0xca, 0x00CA, // Ecircumflex
                0xcb, 0x00CB, // Edieresis
                0xcc, 0x00CC, // Igrave
                0xcd, 0x00CD, // Iacute
                0xce, 0x00CE, // Icircumflex
                0xcf, 0x00CF, // Idieresis
                0xd0, 0x00D0, // Eth
                0xd1, 0x00D1, // Ntilde
                0xd2, 0x00D2, // Ograve
                0xd3, 0x00D3, // Oacute
                0xd4, 0x00D4, // Ocircumflex
                0xd5, 0x00D5, // Otilde
                0xd6, 0x00D6, // Odieresis
                0xd7, 0x00D7, // multiply
                0xd8, 0x00D8, // Oslash
                0xd9, 0x00D9, // Ugrave
                0xda, 0x00DA, // Uacute
                0xdb, 0x00DB, // Ucircumflex
                0xdc, 0x00DC, // Udieresis
                0xdd, 0x00DD, // Yacute
                0xde, 0x00DE, // Thorn
                0xdf, 0x00DF, // germandbls
                0xe0, 0x00E0, // agrave
                0xe1, 0x00E1, // aacute
                0xe2, 0x00E2, // acircumflex
                0xe3, 0x00E3, // atilde
                0xe4, 0x00E4, // adieresis
                0xe5, 0x00E5, // aring
                0xe6, 0x00E6, // ae
                0xe7, 0x00E7, // ccedilla
                0xe8, 0x00E8, // egrave
                0xe9, 0x00E9, // eacute
                0xea, 0x00EA, // ecircumflex
                0xeb, 0x00EB, // edieresis
                0xec, 0x00EC, // igrave
                0xed, 0x00ED, // iacute
                0xee, 0x00EE, // icircumflex
                0xef, 0x00EF, // idieresis
                0xf0, 0x00F0, // eth
                0xf1, 0x00F1, // ntilde
                0xf2, 0x00F2, // ograve
                0xf3, 0x00F3, // oacute
                0xf4, 0x00F4, // ocircumflex
                0xf5, 0x00F5, // otilde
                0xf6, 0x00F6, // odieresis
                0xf7, 0x00F7, // divide
                0xf8, 0x00F8, // oslash
                0xf9, 0x00F9, // ugrave
                0xfa, 0x00FA, // uacute
                0xfb, 0x00FB, // ucircumflex
                0xfc, 0x00FC, // udieresis
                0xfd, 0x00FD, // yacute
                0xfe, 0x00FE, // thorn
                0xff, 0x00FF, // ydieresis
            };


        private static readonly int[] encPDFDocEncoding
            = {
                0x18, 0x02D8, // breve
                0x19, 0x02C7, // caron
                0x1a, 0x02C6, // circumflex
                0x1b, 0x02D9, // dotaccent
                0x1c, 0x02DD, // hungarumlaut
                0x1d, 0x02DB, // ogonek
                0x1e, 0x02DA, // ring
                0x1f, 0x02DC, // tilde
                0x20, 0x0020, // space
                0x20, 0x00A0, // space
                0x21, 0x0021, // exclam
                0x22, 0x0022, // quotedbl
                0x23, 0x0023, // numbersign
                0x24, 0x0024, // dollar
                0x25, 0x0025, // percent
                0x26, 0x0026, // ampersand
                0x27, 0x0027, // quotesingle
                0x28, 0x0028, // parenleft
                0x29, 0x0029, // parenright
                0x2a, 0x002A, // asterisk
                0x2b, 0x002B, // plus
                0x2c, 0x002C, // comma
                0x2d, 0x002D, // hyphen
                0x2d, 0x00AD, // hyphen
                0x2e, 0x002E, // period
                0x2f, 0x002F, // slash
                0x30, 0x0030, // zero
                0x31, 0x0031, // one
                0x32, 0x0032, // two
                0x33, 0x0033, // three
                0x34, 0x0034, // four
                0x35, 0x0035, // five
                0x36, 0x0036, // six
                0x37, 0x0037, // seven
                0x38, 0x0038, // eight
                0x39, 0x0039, // nine
                0x3a, 0x003A, // colon
                0x3b, 0x003B, // semicolon
                0x3c, 0x003C, // less
                0x3d, 0x003D, // equal
                0x3e, 0x003E, // greater
                0x3f, 0x003F, // question
                0x40, 0x0040, // at
                0x41, 0x0041, // A
                0x42, 0x0042, // B
                0x43, 0x0043, // C
                0x44, 0x0044, // D
                0x45, 0x0045, // E
                0x46, 0x0046, // F
                0x47, 0x0047, // G
                0x48, 0x0048, // H
                0x49, 0x0049, // I
                0x4a, 0x004A, // J
                0x4b, 0x004B, // K
                0x4c, 0x004C, // L
                0x4d, 0x004D, // M
                0x4e, 0x004E, // N
                0x4f, 0x004F, // O
                0x50, 0x0050, // P
                0x51, 0x0051, // Q
                0x52, 0x0052, // R
                0x53, 0x0053, // S
                0x54, 0x0054, // T
                0x55, 0x0055, // U
                0x56, 0x0056, // V
                0x57, 0x0057, // W
                0x58, 0x0058, // X
                0x59, 0x0059, // Y
                0x5a, 0x005A, // Z
                0x5b, 0x005B, // bracketleft
                0x5c, 0x005C, // backslash
                0x5d, 0x005D, // bracketright
                0x5e, 0x005E, // asciicircum
                0x5f, 0x005F, // underscore
                0x60, 0x0060, // grave
                0x61, 0x0061, // a
                0x62, 0x0062, // b
                0x63, 0x0063, // c
                0x64, 0x0064, // d
                0x65, 0x0065, // e
                0x66, 0x0066, // f
                0x67, 0x0067, // g
                0x68, 0x0068, // h
                0x69, 0x0069, // i
                0x6a, 0x006A, // j
                0x6b, 0x006B, // k
                0x6c, 0x006C, // l
                0x6d, 0x006D, // m
                0x6e, 0x006E, // n
                0x6f, 0x006F, // o
                0x70, 0x0070, // p
                0x71, 0x0071, // q
                0x72, 0x0072, // r
                0x73, 0x0073, // s
                0x74, 0x0074, // t
                0x75, 0x0075, // u
                0x76, 0x0076, // v
                0x77, 0x0077, // w
                0x78, 0x0078, // x
                0x79, 0x0079, // y
                0x7a, 0x007A, // z
                0x7b, 0x007B, // braceleft
                0x7c, 0x007C, // bar
                0x7d, 0x007D, // braceright
                0x7e, 0x007E, // asciitilde
                0x80, 0x2022, // bullet
                0x81, 0x2020, // dagger
                0x82, 0x2021, // daggerdbl
                0x83, 0x2026, // ellipsis
                0x84, 0x2014, // emdash
                0x85, 0x2013, // endash
                0x86, 0x0192, // florin
                0x87, 0x2044, // fraction
                0x87, 0x2215, // fraction
                0x88, 0x2039, // guilsinglleft
                0x89, 0x203A, // guilsinglright
                0x8a, 0x2212, // minus
                0x8b, 0x2030, // perthousand
                0x8c, 0x201E, // quotedblbase
                0x8d, 0x201C, // quotedblleft
                0x8e, 0x201D, // quotedblright
                0x8f, 0x2018, // quoteleft
                0x90, 0x2019, // quoteright
                0x91, 0x201A, // quotesinglbase
                0x92, 0x2122, // trademark
                0x93, 0xFB01, // fi
                0x94, 0xFB02, // fl
                0x95, 0x0141, // Lslash
                0x96, 0x0152, // OE
                0x97, 0x0160, // Scaron
                0x98, 0x0178, // Ydieresis
                0x99, 0x017D, // Zcaron
                0x9a, 0x0131, // dotlessi
                0x9b, 0x0142, // lslash
                0x9c, 0x0153, // oe
                0x9d, 0x0161, // scaron
                0x9e, 0x017E, // zcaron
                0xa0, 0x20AC, // Euro
                0xa1, 0x00A1, // exclamdown
                0xa2, 0x00A2, // cent
                0xa3, 0x00A3, // sterling
                0xa4, 0x00A4, // currency
                0xa5, 0x00A5, // yen
                0xa6, 0x00A6, // brokenbar
                0xa7, 0x00A7, // section
                0xa8, 0x00A8, // dieresis
                0xa9, 0x00A9, // copyright
                0xaa, 0x00AA, // ordfeminine
                0xab, 0x00AB, // guillemotleft
                0xac, 0x00AC, // logicalnot
                0xae, 0x00AE, // registered
                0xaf, 0x00AF, // macron
                0xaf, 0x02C9, // macron
                0xb0, 0x00B0, // degree
                0xb1, 0x00B1, // plusminus
                0xb2, 0x00B2, // twosuperior
                0xb3, 0x00B3, // threesuperior
                0xb4, 0x00B4, // acute
                0xb5, 0x00B5, // mu
                0xb5, 0x03BC, // mu
                0xb6, 0x00B6, // paragraph
                0xb7, 0x00B7, // periodcentered
                0xb7, 0x2219, // periodcentered
                0xb8, 0x00B8, // cedilla
                0xb9, 0x00B9, // onesuperior
                0xba, 0x00BA, // ordmasculine
                0xbb, 0x00BB, // guillemotright
                0xbc, 0x00BC, // onequarter
                0xbd, 0x00BD, // onehalf
                0xbe, 0x00BE, // threequarters
                0xbf, 0x00BF, // questiondown
                0xc0, 0x00C0, // Agrave
                0xc1, 0x00C1, // Aacute
                0xc2, 0x00C2, // Acircumflex
                0xc3, 0x00C3, // Atilde
                0xc4, 0x00C4, // Adieresis
                0xc5, 0x00C5, // Aring
                0xc6, 0x00C6, // AE
                0xc7, 0x00C7, // Ccedilla
                0xc8, 0x00C8, // Egrave
                0xc9, 0x00C9, // Eacute
                0xca, 0x00CA, // Ecircumflex
                0xcb, 0x00CB, // Edieresis
                0xcc, 0x00CC, // Igrave
                0xcd, 0x00CD, // Iacute
                0xce, 0x00CE, // Icircumflex
                0xcf, 0x00CF, // Idieresis
                0xd0, 0x00D0, // Eth
                0xd1, 0x00D1, // Ntilde
                0xd2, 0x00D2, // Ograve
                0xd3, 0x00D3, // Oacute
                0xd4, 0x00D4, // Ocircumflex
                0xd5, 0x00D5, // Otilde
                0xd6, 0x00D6, // Odieresis
                0xd7, 0x00D7, // multiply
                0xd8, 0x00D8, // Oslash
                0xd9, 0x00D9, // Ugrave
                0xda, 0x00DA, // Uacute
                0xdb, 0x00DB, // Ucircumflex
                0xdc, 0x00DC, // Udieresis
                0xdd, 0x00DD, // Yacute
                0xde, 0x00DE, // Thorn
                0xdf, 0x00DF, // germandbls
                0xe0, 0x00E0, // agrave
                0xe1, 0x00E1, // aacute
                0xe2, 0x00E2, // acircumflex
                0xe3, 0x00E3, // atilde
                0xe4, 0x00E4, // adieresis
                0xe5, 0x00E5, // aring
                0xe6, 0x00E6, // ae
                0xe7, 0x00E7, // ccedilla
                0xe8, 0x00E8, // egrave
                0xe9, 0x00E9, // eacute
                0xea, 0x00EA, // ecircumflex
                0xeb, 0x00EB, // edieresis
                0xec, 0x00EC, // igrave
                0xed, 0x00ED, // iacute
                0xee, 0x00EE, // icircumflex
                0xef, 0x00EF, // idieresis
                0xf0, 0x00F0, // eth
                0xf1, 0x00F1, // ntilde
                0xf2, 0x00F2, // ograve
                0xf3, 0x00F3, // oacute
                0xf4, 0x00F4, // ocircumflex
                0xf5, 0x00F5, // otilde
                0xf6, 0x00F6, // odieresis
                0xf7, 0x00F7, // divide
                0xf8, 0x00F8, // oslash
                0xf9, 0x00F9, // ugrave
                0xfa, 0x00FA, // uacute
                0xfb, 0x00FB, // ucircumflex
                0xfc, 0x00FC, // udieresis
                0xfd, 0x00FD, // yacute
                0xfe, 0x00FE, // thorn
                0xff, 0x00FF, // ydieresis
            };


        private static readonly int[] encSymbolEncoding
            = {
                0x20, 0x0020, // space
                0x20, 0x00A0, // space
                0x21, 0x0021, // exclam
                0x22, 0x2200, // universal
                0x23, 0x0023, // numbersign
                0x24, 0x2203, // existential
                0x25, 0x0025, // percent
                0x26, 0x0026, // ampersand
                0x27, 0x220B, // suchthat
                0x28, 0x0028, // parenleft
                0x29, 0x0029, // parenright
                0x2a, 0x2217, // asteriskmath
                0x2b, 0x002B, // plus
                0x2c, 0x002C, // comma
                0x2d, 0x2212, // minus
                0x2e, 0x002E, // period
                0x2f, 0x002F, // slash
                0x30, 0x0030, // zero
                0x31, 0x0031, // one
                0x32, 0x0032, // two
                0x33, 0x0033, // three
                0x34, 0x0034, // four
                0x35, 0x0035, // five
                0x36, 0x0036, // six
                0x37, 0x0037, // seven
                0x38, 0x0038, // eight
                0x39, 0x0039, // nine
                0x3a, 0x003A, // colon
                0x3b, 0x003B, // semicolon
                0x3c, 0x003C, // less
                0x3d, 0x003D, // equal
                0x3e, 0x003E, // greater
                0x3f, 0x003F, // question
                0x40, 0x2245, // congruent
                0x41, 0x0391, // Alpha
                0x42, 0x0392, // Beta
                0x43, 0x03A7, // Chi
                0x44, 0x2206, // Delta
                0x44, 0x0394, // Delta
                0x45, 0x0395, // Epsilon
                0x46, 0x03A6, // Phi
                0x47, 0x0393, // Gamma
                0x48, 0x0397, // Eta
                0x49, 0x0399, // Iota
                0x4a, 0x03D1, // theta1
                0x4b, 0x039A, // Kappa
                0x4c, 0x039B, // Lambda
                0x4d, 0x039C, // Mu
                0x4e, 0x039D, // Nu
                0x4f, 0x039F, // Omicron
                0x50, 0x03A0, // Pi
                0x51, 0x0398, // Theta
                0x52, 0x03A1, // Rho
                0x53, 0x03A3, // Sigma
                0x54, 0x03A4, // Tau
                0x55, 0x03A5, // Upsilon
                0x56, 0x03C2, // sigma1
                0x57, 0x2126, // Omega
                0x57, 0x03A9, // Omega
                0x58, 0x039E, // Xi
                0x59, 0x03A8, // Psi
                0x5a, 0x0396, // Zeta
                0x5b, 0x005B, // bracketleft
                0x5c, 0x2234, // therefore
                0x5d, 0x005D, // bracketright
                0x5e, 0x22A5, // perpendicular
                0x5f, 0x005F, // underscore
                0x60, 0xF8E5, // radicalex
                0x61, 0x03B1, // alpha
                0x62, 0x03B2, // beta
                0x63, 0x03C7, // chi
                0x64, 0x03B4, // delta
                0x65, 0x03B5, // epsilon
                0x66, 0x03C6, // phi
                0x67, 0x03B3, // gamma
                0x68, 0x03B7, // eta
                0x69, 0x03B9, // iota
                0x6a, 0x03D5, // phi1
                0x6b, 0x03BA, // kappa
                0x6c, 0x03BB, // lambda
                0x6d, 0x00B5, // mu
                0x6d, 0x03BC, // mu
                0x6e, 0x03BD, // nu
                0x6f, 0x03BF, // omicron
                0x70, 0x03C0, // pi
                0x71, 0x03B8, // theta
                0x72, 0x03C1, // rho
                0x73, 0x03C3, // sigma
                0x74, 0x03C4, // tau
                0x75, 0x03C5, // upsilon
                0x76, 0x03D6, // omega1
                0x77, 0x03C9, // omega
                0x78, 0x03BE, // xi
                0x79, 0x03C8, // psi
                0x7a, 0x03B6, // zeta
                0x7b, 0x007B, // braceleft
                0x7c, 0x007C, // bar
                0x7d, 0x007D, // braceright
                0x7e, 0x223C, // similar
                0xa0, 0x20AC, // Euro
                0xa1, 0x03D2, // Upsilon1
                0xa2, 0x2032, // minute
                0xa3, 0x2264, // lessequal
                0xa4, 0x2044, // fraction
                0xa4, 0x2215, // fraction
                0xa5, 0x221E, // infinity
                0xa6, 0x0192, // florin
                0xa7, 0x2663, // club
                0xa8, 0x2666, // diamond
                0xa9, 0x2665, // heart
                0xaa, 0x2660, // spade
                0xab, 0x2194, // arrowboth
                0xac, 0x2190, // arrowleft
                0xad, 0x2191, // arrowup
                0xae, 0x2192, // arrowright
                0xaf, 0x2193, // arrowdown
                0xb0, 0x00B0, // degree
                0xb1, 0x00B1, // plusminus
                0xb2, 0x2033, // second
                0xb3, 0x2265, // greaterequal
                0xb4, 0x00D7, // multiply
                0xb5, 0x221D, // proportional
                0xb6, 0x2202, // partialdiff
                0xb7, 0x2022, // bullet
                0xb8, 0x00F7, // divide
                0xb9, 0x2260, // notequal
                0xba, 0x2261, // equivalence
                0xbb, 0x2248, // approxequal
                0xbc, 0x2026, // ellipsis
                0xbd, 0xF8E6, // arrowvertex
                0xbe, 0xF8E7, // arrowhorizex
                0xbf, 0x21B5, // carriagereturn
                0xc0, 0x2135, // aleph
                0xc1, 0x2111, // Ifraktur
                0xc2, 0x211C, // Rfraktur
                0xc3, 0x2118, // weierstrass
                0xc4, 0x2297, // circlemultiply
                0xc5, 0x2295, // circleplus
                0xc6, 0x2205, // emptyset
                0xc7, 0x2229, // intersection
                0xc8, 0x222A, // union
                0xc9, 0x2283, // propersuperset
                0xca, 0x2287, // reflexsuperset
                0xcb, 0x2284, // notsubset
                0xcc, 0x2282, // propersubset
                0xcd, 0x2286, // reflexsubset
                0xce, 0x2208, // element
                0xcf, 0x2209, // notelement
                0xd0, 0x2220, // angle
                0xd1, 0x2207, // gradient
                0xd2, 0xF6DA, // registerserif
                0xd3, 0xF6D9, // copyrightserif
                0xd4, 0xF6DB, // trademarkserif
                0xd5, 0x220F, // product
                0xd6, 0x221A, // radical
                0xd7, 0x22C5, // dotmath
                0xd8, 0x00AC, // logicalnot
                0xd9, 0x2227, // logicaland
                0xda, 0x2228, // logicalor
                0xdb, 0x21D4, // arrowdblboth
                0xdc, 0x21D0, // arrowdblleft
                0xdd, 0x21D1, // arrowdblup
                0xde, 0x21D2, // arrowdblright
                0xdf, 0x21D3, // arrowdbldown
                0xe0, 0x25CA, // lozenge
                0xe1, 0x2329, // angleleft
                0xe2, 0xF8E8, // registersans
                0xe3, 0xF8E9, // copyrightsans
                0xe4, 0xF8EA, // trademarksans
                0xe5, 0x2211, // summation
                0xe6, 0xF8EB, // parenlefttp
                0xe7, 0xF8EC, // parenleftex
                0xe8, 0xF8ED, // parenleftbt
                0xe9, 0xF8EE, // bracketlefttp
                0xea, 0xF8EF, // bracketleftex
                0xeb, 0xF8F0, // bracketleftbt
                0xec, 0xF8F1, // bracelefttp
                0xed, 0xF8F2, // braceleftmid
                0xee, 0xF8F3, // braceleftbt
                0xef, 0xF8F4, // braceex
                0xf1, 0x232A, // angleright
                0xf2, 0x222B, // integral
                0xf3, 0x2320, // integraltp
                0xf4, 0xF8F5, // integralex
                0xf5, 0x2321, // integralbt
                0xf6, 0xF8F6, // parenrighttp
                0xf7, 0xF8F7, // parenrightex
                0xf8, 0xF8F8, // parenrightbt
                0xf9, 0xF8F9, // bracketrighttp
                0xfa, 0xF8FA, // bracketrightex
                0xfb, 0xF8FB, // bracketrightbt
                0xfc, 0xF8FC, // bracerighttp
                0xfd, 0xF8FD, // bracerightmid
                0xfe, 0xF8FE, // bracerightbt
            };


        private static readonly int[] encZapfDingbatsEncoding
            = {
                0x20, 0x0020, // space
                0x20, 0x00A0, // space
                0x21, 0x2701, // a1
                0x22, 0x2702, // a2
                0x23, 0x2703, // a202
                0x24, 0x2704, // a3
                0x25, 0x260E, // a4
                0x26, 0x2706, // a5
                0x27, 0x2707, // a119
                0x28, 0x2708, // a118
                0x29, 0x2709, // a117
                0x2A, 0x261B, // a11
                0x2B, 0x261E, // a12
                0x2C, 0x270C, // a13
                0x2D, 0x270D, // a14
                0x2E, 0x270E, // a15
                0x2F, 0x270F, // a16
                0x30, 0x2710, // a105
                0x31, 0x2711, // a17
                0x32, 0x2712, // a18
                0x33, 0x2713, // a19
                0x34, 0x2714, // a20
                0x35, 0x2715, // a21
                0x36, 0x2716, // a22
                0x37, 0x2717, // a23
                0x38, 0x2718, // a24
                0x39, 0x2719, // a25
                0x3A, 0x271A, // a26
                0x3B, 0x271B, // a27
                0x3C, 0x271C, // a28
                0x3D, 0x271D, // a6
                0x3E, 0x271E, // a7
                0x3F, 0x271F, // a8
                0x40, 0x2720, // a9
                0x41, 0x2721, // a10
                0x42, 0x2722, // a29
                0x43, 0x2723, // a30
                0x44, 0x2724, // a31
                0x45, 0x2725, // a32
                0x46, 0x2726, // a33
                0x47, 0x2727, // a34
                0x48, 0x2605, // a35
                0x49, 0x2729, // a36
                0x4A, 0x272A, // a37
                0x4B, 0x272B, // a38
                0x4C, 0x272C, // a39
                0x4D, 0x272D, // a40
                0x4E, 0x272E, // a41
                0x4F, 0x272F, // a42
                0x50, 0x2730, // a43
                0x51, 0x2731, // a44
                0x52, 0x2732, // a45
                0x53, 0x2733, // a46
                0x54, 0x2734, // a47
                0x55, 0x2735, // a48
                0x56, 0x2736, // a49
                0x57, 0x2737, // a50
                0x58, 0x2738, // a51
                0x59, 0x2739, // a52
                0x5A, 0x273A, // a53
                0x5B, 0x273B, // a54
                0x5C, 0x273C, // a55
                0x5D, 0x273D, // a56
                0x5E, 0x273E, // a57
                0x5F, 0x273F, // a58
                0x60, 0x2740, // a59
                0x61, 0x2741, // a60
                0x62, 0x2742, // a61
                0x63, 0x2743, // a62
                0x64, 0x2744, // a63
                0x65, 0x2745, // a64
                0x66, 0x2746, // a65
                0x67, 0x2747, // a66
                0x68, 0x2748, // a67
                0x69, 0x2749, // a68
                0x6A, 0x274A, // a69
                0x6B, 0x274B, // a70
                0x6C, 0x25CF, // a71
                0x6D, 0x274D, // a72
                0x6E, 0x25A0, // a73
                0x6F, 0x274F, // a74
                0x70, 0x2750, // a203
                0x71, 0x2751, // a75
                0x72, 0x2752, // a204
                0x73, 0x25B2, // a76
                0x74, 0x25BC, // a77
                0x75, 0x25C6, // a78
                0x76, 0x2756, // a79
                0x77, 0x25D7, // a81
                0x78, 0x2758, // a82
                0x79, 0x2759, // a83
                0x7A, 0x275A, // a84
                0x7B, 0x275B, // a97
                0x7C, 0x275C, // a98
                0x7D, 0x275D, // a99
                0x7E, 0x275E, // a100
                0x80, 0xF8D7, // a89
                0x81, 0xF8D8, // a90
                0x82, 0xF8D9, // a93
                0x83, 0xF8DA, // a94
                0x84, 0xF8DB, // a91
                0x85, 0xF8DC, // a92
                0x86, 0xF8DD, // a205
                0x87, 0xF8DE, // a85
                0x88, 0xF8DF, // a206
                0x89, 0xF8E0, // a86
                0x8A, 0xF8E1, // a87
                0x8B, 0xF8E2, // a88
                0x8C, 0xF8E3, // a95
                0x8D, 0xF8E4, // a96
                0xA1, 0x2761, // a101
                0xA2, 0x2762, // a102
                0xA3, 0x2763, // a103
                0xA4, 0x2764, // a104
                0xA5, 0x2765, // a106
                0xA6, 0x2766, // a107
                0xA7, 0x2767, // a108
                0xA8, 0x2663, // a112
                0xA9, 0x2666, // a111
                0xAA, 0x2665, // a110
                0xAB, 0x2660, // a109
                0xAC, 0x2460, // a120
                0xAD, 0x2461, // a121
                0xAE, 0x2462, // a122
                0xAF, 0x2463, // a123
                0xB0, 0x2464, // a124
                0xB1, 0x2465, // a125
                0xB2, 0x2466, // a126
                0xB3, 0x2467, // a127
                0xB4, 0x2468, // a128
                0xB5, 0x2469, // a129
                0xB6, 0x2776, // a130
                0xB7, 0x2777, // a131
                0xB8, 0x2778, // a132
                0xB9, 0x2779, // a133
                0xBA, 0x277A, // a134
                0xBB, 0x277B, // a135
                0xBC, 0x277C, // a136
                0xBD, 0x277D, // a137
                0xBE, 0x277E, // a138
                0xBF, 0x277F, // a139
                0xC0, 0x2780, // a140
                0xC1, 0x2781, // a141
                0xC2, 0x2782, // a142
                0xC3, 0x2783, // a143
                0xC4, 0x2784, // a144
                0xC5, 0x2785, // a145
                0xC6, 0x2786, // a146
                0xC7, 0x2787, // a147
                0xC8, 0x2788, // a148
                0xC9, 0x2789, // a149
                0xCA, 0x278A, // a150
                0xCB, 0x278B, // a151
                0xCC, 0x278C, // a152
                0xCD, 0x278D, // a153
                0xCE, 0x278E, // a154
                0xCF, 0x278F, // a155
                0xD0, 0x2790, // a156
                0xD1, 0x2791, // a157
                0xD2, 0x2792, // a158
                0xD3, 0x2793, // a159
                0xD4, 0x2794, // a160
                0xD5, 0x2192, // a161
                0xD6, 0x2194, // a163
                0xD7, 0x2195, // a164
                0xD8, 0x2798, // a196
                0xD9, 0x2799, // a165
                0xDA, 0x279A, // a192
                0xDB, 0x279B, // a166
                0xDC, 0x279C, // a167
                0xDD, 0x279D, // a168
                0xDE, 0x279E, // a169
                0xDF, 0x279F, // a170
                0xE0, 0x27A0, // a171
                0xE1, 0x27A1, // a172
                0xE2, 0x27A2, // a173
                0xE3, 0x27A3, // a162
                0xE4, 0x27A4, // a174
                0xE5, 0x27A5, // a175
                0xE6, 0x27A6, // a176
                0xE7, 0x27A7, // a177
                0xE8, 0x27A8, // a178
                0xE9, 0x27A9, // a179
                0xEA, 0x27AA, // a193
                0xEB, 0x27AB, // a180
                0xEC, 0x27AC, // a199
                0xED, 0x27AD, // a181
                0xEE, 0x27AE, // a200
                0xEF, 0x27AF, // a182
                0xF1, 0x27B1, // a201
                0xF2, 0x27B2, // a183
                0xF3, 0x27B3, // a184
                0xF4, 0x27B4, // a197
                0xF5, 0x27B5, // a185
                0xF6, 0x27B6, // a194
                0xF7, 0x27B7, // a198
                0xF8, 0x27B8, // a186
                0xF9, 0x27B9, // a195
                0xFA, 0x27BA, // a187
                0xFB, 0x27BB, // a188
                0xFC, 0x27BC, // a189
                0xFD, 0x27BD, // a190
                0xFE, 0x27BE, // a191
            };


    }
}