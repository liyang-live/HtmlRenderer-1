//BSD 2014,WinterCore
//ArthurHub

using System;
using System.Globalization;
using HtmlRenderer.Entities;
using HtmlRenderer.Parse;

namespace HtmlRenderer.Dom
{
    public static class CssFontSizeConst
    {
        public const int FONTSIZE_MEDIUM = 10;
        public const int FONTSIZE_XX_SMALL = 11;
        public const int FONTSIZE_X_SMALL = 12;
        public const int FONTSIZE_SMALL = 13;
        public const int FONTSIZE_LARGE = 14;
        public const int FONTSIZE_X_LARGE = 15;
        public const int FONTSIZE_XX_LARGE = 16;
        public const int FONTSIZE_SMALLER = 17;
        public const int FONTSIZE_LARGER = 18;
    }
    public static class CssBackgroundPositionConst
    {
        public const int LEFT = 1 << (10 - 1);
        public const int TOP = 1 << (11 - 1);
        public const int RIGHT = 1 << (12 - 1);
        public const int BOTTOM = 1 << (13 - 1);
        public const int CENTER = 1 << (14 - 1);
    }

    /// <summary>
    /// Represents and gets info about a CSS Length
    /// </summary>
    /// <remarks>
    /// http://www.w3.org/TR/CSS21/syndata.html#length-units
    /// </remarks>
    public struct CssLength
    {

        const int IS_AUTO = 1 << (11 - 1);
        const int IS_RELATIVE = 1 << (12 - 1);
        const int HAS_ERROR = 1 << (13 - 1);
        const int IS_ASSIGN = 1 << (14 - 1);
        const int NONE_VALUE = 1 << (15 - 1);
        const int NORMAL = 1 << (16 - 1);
        //-------------------------------------
        const int MEDIUM = 1 << (17 - 1);
        const int THICK = 1 << (18 - 1);
        const int THIN = 1 << (19 - 1);
        //-------------------------------------
        //when used as font size
        const int IS_FONT_SIZE_NAME = 1 << (20 - 1);
        //------------------------------------- 
        //when used as background position
        const int IS_BACKGROUND_POS = 1 << (21 - 1);
        //------------------------------------- 

        #region Fields
        private readonly float _number;
        readonly int _flags;



        #endregion

        public static readonly CssLength AutoLength = new CssLength(IS_ASSIGN | IS_AUTO);
        public static readonly CssLength NotAssign = new CssLength(0);
        public static readonly CssLength NormalWordOrLine = new CssLength(IS_ASSIGN | NORMAL);


        public static readonly CssLength Medium = new CssLength(IS_ASSIGN | MEDIUM);
        public static readonly CssLength Thick = new CssLength(IS_ASSIGN | THICK);
        public static readonly CssLength Thin = new CssLength(IS_ASSIGN | THIN);

        public static readonly CssLength ZeroNoUnit = CssLength.MakeZeroLengthNoUnit();
        public static readonly CssLength ZeroPx = CssLength.MakePixelLength(0);
        //-----------------------------------------------------------------------------------------

        public static readonly CssLength FontSizeMedium = new CssLength(IS_ASSIGN | IS_FONT_SIZE_NAME | CssFontSizeConst.FONTSIZE_MEDIUM);//default
        public static readonly CssLength FontSizeXXSmall = new CssLength(IS_ASSIGN | IS_FONT_SIZE_NAME | CssFontSizeConst.FONTSIZE_XX_SMALL);
        public static readonly CssLength FontSizeXSmall = new CssLength(IS_ASSIGN | IS_FONT_SIZE_NAME | CssFontSizeConst.FONTSIZE_X_SMALL);
        public static readonly CssLength FontSizeSmall = new CssLength(IS_ASSIGN | IS_FONT_SIZE_NAME | CssFontSizeConst.FONTSIZE_SMALL);
        public static readonly CssLength FontSizeLarge = new CssLength(IS_ASSIGN | IS_FONT_SIZE_NAME | CssFontSizeConst.FONTSIZE_LARGE);
        public static readonly CssLength FontSizeXLarge = new CssLength(IS_ASSIGN | IS_FONT_SIZE_NAME | CssFontSizeConst.FONTSIZE_X_LARGE);
        public static readonly CssLength FontSizeXXLarge = new CssLength(IS_ASSIGN | IS_FONT_SIZE_NAME | CssFontSizeConst.FONTSIZE_XX_LARGE);

        public static readonly CssLength FontSizeSmaller = new CssLength(IS_ASSIGN | IS_FONT_SIZE_NAME | CssFontSizeConst.FONTSIZE_SMALLER);
        public static readonly CssLength FontSizeLarger = new CssLength(IS_ASSIGN | IS_FONT_SIZE_NAME | CssFontSizeConst.FONTSIZE_LARGE);
        //-----------------------------------------------------------------------------------------
        public static readonly CssLength BackgroundPosLeft = new CssLength(IS_ASSIGN | IS_BACKGROUND_POS | CssBackgroundPositionConst.LEFT);
        public static readonly CssLength BackgroundPosTop = new CssLength(IS_ASSIGN | IS_BACKGROUND_POS | CssBackgroundPositionConst.TOP);
        public static readonly CssLength BackgroundPosRight = new CssLength(IS_ASSIGN | IS_BACKGROUND_POS | CssBackgroundPositionConst.RIGHT);
        public static readonly CssLength BackgroundPosBottom = new CssLength(IS_ASSIGN | IS_BACKGROUND_POS | CssBackgroundPositionConst.BOTTOM);
        public static readonly CssLength BackgroundPosCenter = new CssLength(IS_ASSIGN | IS_BACKGROUND_POS | CssBackgroundPositionConst.CENTER);
        //-----------------------------------------------------------------------------------------


        #region Ctor
        /// <summary>
        /// Creates a new CssLength from a length specified on a CSS style sheet or fragment
        /// </summary>
        /// <param name="lenValue">Length as specified in the Style Sheet or style fragment</param>
        public CssLength(string lenValue)
        {

            _number = 0f;
            this._flags = (int)CssUnit.None | IS_ASSIGN;

            switch (lenValue)
            {
                case null:
                case "":
                case "0":
                    {   //Return zero if no length specified, zero specified
                        return;
                    }
                case "auto":
                    {
                        this._flags |= IS_AUTO;
                        return;
                    }
                case "normal":
                    {
                        this._flags |= NORMAL;
                        return;
                    }
            }

            //then parse
            //If percentage, use ParseNumber
            if (lenValue.EndsWith("%"))
            {
                _number = float.Parse(lenValue.Substring(0, lenValue.Length - 1));
                this._flags |= (int)CssUnit.Percent;
                return;
            }

            //If no units, has error
            if (lenValue.Length < 3)
            {
                float.TryParse(lenValue, out _number);
                this._flags |= HAS_ERROR;
                //_hasError = true;
                return;
            }

            //Get units of the length
            //TODO: Units behave different in paper and in screen!
            CssUnit unit = GetCssUnit(lenValue.Substring(lenValue.Length - 2, 2));

            switch (unit)
            {
                case CssUnit.Ems:
                case CssUnit.Ex:
                case CssUnit.Pixels:
                    this._flags |= (int)unit | IS_RELATIVE;
                    break;
                case CssUnit.Unknown:
                    this._flags |= HAS_ERROR;
                    return;
                default:
                    this._flags |= (int)unit;
                    break;
            }

            string number = lenValue.Substring(0, lenValue.Length - 2);
            if (!float.TryParse(number, System.Globalization.NumberStyles.Number, NumberFormatInfo.InvariantInfo, out this._number))
            {
                this._flags |= HAS_ERROR;
            }
        }


        public CssLength(float num, CssUnit unit)
        {
            this._number = num;
            this._flags = (int)unit | IS_ASSIGN;
            switch (unit)
            {
                case CssUnit.Pixels:
                case CssUnit.Ems:
                case CssUnit.Ex:
                case CssUnit.None:
                    this._flags |= IS_RELATIVE;
                    break;
                case CssUnit.Unknown:
                    this._flags |= HAS_ERROR;
                    return;
                default:
                    break;
            }
        }
        private CssLength(int internalFlags)
        {
            this._number = 0;
            this._flags = internalFlags; // (int)CssUnit.Pixels | IS_ASSIGN; 
        }

        #endregion


        #region Props


        public static CssUnit GetCssUnit(string u)
        {
            switch (u)
            {
                case CssConstants.Em:
                    return CssUnit.Ems;
                case CssConstants.Ex:
                    return CssUnit.Ex;
                case CssConstants.Px:
                    return CssUnit.Pixels;
                case CssConstants.Mm:
                    return CssUnit.Milimeters;
                case CssConstants.Cm:
                    return CssUnit.Centimeters;
                case CssConstants.In:
                    return CssUnit.Inches;
                case CssConstants.Pt:
                    return CssUnit.Points;
                case CssConstants.Pc:
                    return CssUnit.Picas;
                default:
                    return CssUnit.Unknown;
            }
        }
        public static CssLength MakeBorderLength(string str)
        {
            switch (str)
            {
                case CssConstants.Medium:
                    return CssLength.Medium;
                case CssConstants.Thick:
                    return CssLength.Thick;
                case CssConstants.Thin:
                    return CssLength.Thin;
            }
            return new CssLength(str);
        }
        public static CssLength MakePixelLength(float pixel)
        {
            return new CssLength(pixel, CssUnit.Pixels);
        }
        public static CssLength MakeZeroLengthNoUnit()
        {
            return new CssLength(0, CssUnit.None);
        }
        public static CssLength MakeFontSizePtUnit(float pointUnit)
        {
            return new CssLength(pointUnit, CssUnit.Points);
        }
        /// <summary>
        /// Gets the number in the length
        /// </summary>
        public float Number
        {
            get { return _number; }
        }
        public bool IsMedium
        {
            get { return (this._flags & MEDIUM) != 0; }
        }
        public bool IsThick
        {
            get { return (this._flags & THICK) != 0; }
        }
        public bool IsThin
        {
            get { return (this._flags & THIN) != 0; }
        }
        /// <summary>
        /// Gets if the length has some parsing error
        /// </summary>
        public bool HasError
        {
            // get { return _hasError; }
            get { return (this._flags & HAS_ERROR) != 0; }
        }


        /// <summary>
        /// Gets if the length represents a precentage (not actually a length)
        /// </summary>
        public bool IsPercentage
        {

            get { return this.Unit == CssUnit.Percent; }
        }

        public bool IsAuto
        {
            get { return (this._flags & IS_AUTO) != 0; }
        }
        public bool IsEmpty
        {
            get { return (this._flags & IS_ASSIGN) == 0; }
        }
        public bool IsNormalWordSpacing
        {
            get
            {
                return (this._flags & NORMAL) != 0;
            }
        }
        public bool IsNormalLineHeight
        {
            get
            {
                return (this._flags & NORMAL) != 0;
            }
        }
        /// <summary>
        /// Gets if the length is specified in relative units
        /// </summary>
        public bool IsRelative
        {
            get { return (this._flags & IS_RELATIVE) != 0; }
            //get { return _isRelative; }
        }

        /// <summary>
        /// Gets the unit of the length
        /// </summary>
        public CssUnit Unit
        {
            get { return (CssUnit)(this._flags & 0xFF); }
        }

        //-------------------------------------------------
        public bool IsFontSizeName
        {
            get { return (this._flags & IS_FONT_SIZE_NAME) != 0; }
        }
        public int FontSizeName
        {
            get { return (int)(this._flags & 0xFF); }
        }
        //-------------------------------------------------
        public bool IsBackgroundPositionName
        {
            get { return (this._flags & IS_BACKGROUND_POS) != 0; }
        }
        public int BackgroundPositionName
        {
            get { return (int)(this._flags & 0xFF); }
        }
        #endregion

        #region Methods

        /// <summary>
        /// If length is in Ems, returns its value in points
        /// </summary>
        /// <param name="emSize">Em size factor to multiply</param>
        /// <returns>Points size of this em</returns>
        /// <exception cref="InvalidOperationException">If length has an error or isn't in ems</exception>
        public CssLength ConvertEmToPoints(float emSize)
        {
            if (HasError) throw new InvalidOperationException("Invalid length");
            if (Unit != CssUnit.Ems) throw new InvalidOperationException("Length is not in ems");

            return new CssLength(Number * emSize, CssUnit.Points);
            //return new CssLength(string.Format("{0}pt", Convert.ToSingle(Number * emSize).ToString("0.0", NumberFormatInfo.InvariantInfo)));
        }

        /// <summary>
        /// If length is in Ems, returns its value in pixels
        /// </summary>
        /// <param name="pixelFactor">Pixel size factor to multiply</param>
        /// <returns>Pixels size of this em</returns>
        /// <exception cref="InvalidOperationException">If length has an error or isn't in ems</exception>
        public CssLength ConvertEmToPixels(float pixelFactor)
        {
            if (HasError) throw new InvalidOperationException("Invalid length");
            if (Unit != CssUnit.Ems) throw new InvalidOperationException("Length is not in ems");

            return new CssLength(Number * pixelFactor, CssUnit.Pixels);
            //return new CssLength(string.Format("{0}px", Convert.ToSingle(Number * pixelFactor).ToString("0.0", NumberFormatInfo.InvariantInfo)));
        }

        /// <summary>
        /// Returns the length formatted ready for CSS interpreting.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (HasError)
            {
                return string.Empty;
            }
            else
            {
                string u = string.Empty;

                switch (Unit)
                {
                    case CssUnit.Percent:
                        return string.Format(NumberFormatInfo.InvariantInfo, "{0}%", Number);

                    case CssUnit.None:
                        break;
                    case CssUnit.Ems:
                        u = "em";
                        break;
                    case CssUnit.Pixels:
                        u = "px";
                        break;
                    case CssUnit.Ex:
                        u = "ex";
                        break;
                    case CssUnit.Inches:
                        u = "in";
                        break;
                    case CssUnit.Centimeters:
                        u = "cm";
                        break;
                    case CssUnit.Milimeters:
                        u = "mm";
                        break;
                    case CssUnit.Points:
                        u = "pt";
                        break;
                    case CssUnit.Picas:
                        u = "pc";
                        break;

                }

                return string.Format(NumberFormatInfo.InvariantInfo, "{0}{1}", Number, u);
            }
        }

        #endregion
    }
}