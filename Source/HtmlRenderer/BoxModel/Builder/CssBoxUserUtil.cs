﻿//BSD, 2014 WinterCore
using System;
using System.Drawing;
using System.Collections.Generic;

using HtmlRenderer.Entities;
using HtmlRenderer.Handlers;
using HtmlRenderer.Utils;

namespace HtmlRenderer.Dom
{

    public static class CssBoxUserUtilExtension
    {


        static readonly ValueMap<CssDisplay> _cssDisplayMap = new ValueMap<CssDisplay>();
        static readonly ValueMap<CssDirection> _cssDirectionMap = new ValueMap<CssDirection>();
        static readonly ValueMap<CssPosition> _cssPositionMap = new ValueMap<CssPosition>();
        static readonly ValueMap<CssWordBreak> _cssWordBreakMap = new ValueMap<CssWordBreak>();
        static readonly ValueMap<CssTextDecoration> _cssTextDecorationMap = new ValueMap<CssTextDecoration>();
        static readonly ValueMap<CssOverflow> _cssOverFlowMap = new ValueMap<CssOverflow>();
        static readonly ValueMap<CssTextAlign> _cssTextAlignMap = new ValueMap<CssTextAlign>();
        static readonly ValueMap<CssVerticalAlign> _cssVerticalAlignMap = new ValueMap<CssVerticalAlign>();
        static readonly ValueMap<CssVisibility> _cssVisibilityMap = new ValueMap<CssVisibility>();
        static readonly ValueMap<CssWhiteSpace> _cssWhitespaceMap = new ValueMap<CssWhiteSpace>();
        static readonly ValueMap<CssBorderCollapse> _cssCollapseBorderMap = new ValueMap<CssBorderCollapse>();
        static readonly ValueMap<CssBorderStyle> _cssBorderStyleMap = new ValueMap<CssBorderStyle>();
        static readonly ValueMap<CssEmptyCell> _cssEmptyCellMap = new ValueMap<CssEmptyCell>();
        static readonly ValueMap<CssFloat> _cssFloatMap = new ValueMap<CssFloat>();
        static readonly ValueMap<CssFontStyle> _cssFontStyleMap = new ValueMap<CssFontStyle>();
        static readonly ValueMap<CssFontVariant> _cssFontVariantMap = new ValueMap<CssFontVariant>();
        static readonly ValueMap<CssFontWeight> _cssFontWeightMap = new ValueMap<CssFontWeight>();
        static readonly ValueMap<CssListStylePosition> _cssListStylePositionMap = new ValueMap<CssListStylePosition>();
        static readonly ValueMap<CssListStyleType> _cssListStyleTypeMap = new ValueMap<CssListStyleType>();

        static readonly ValueMap<CssNamedBorderWidth> _cssNamedBorderWidthMap = new ValueMap<CssNamedBorderWidth>();
        static readonly ValueMap<CssBackgroundRepeat> _cssBackgroundRepeatMap = new ValueMap<CssBackgroundRepeat>();



        static readonly ValueMap<HtmlRenderer.WebDom.WellknownCssPropertyName> _wellKnownCssPropNameMap = new ValueMap<WebDom.WellknownCssPropertyName>();
        static readonly ValueMap<WellknownHtmlTagName> _wellknownHtmlTagNameMap = new ValueMap<WellknownHtmlTagName>();



        static CssBoxUserUtilExtension()
        {

        }

        //-----------------------
        public static bool IsBorderStyle(string value)
        {
            return _cssBorderStyleMap.GetValueFromString(value, CssBorderStyle.Unknown) != CssBorderStyle.Unknown;
        }
        //thin,medium,thick border width
        public static bool IsNamedBorderWidth(string value)
        {
            return _cssNamedBorderWidthMap.GetValueFromString(value, CssNamedBorderWidth.Unknown) != CssNamedBorderWidth.Unknown;
        }
        public static bool IsFontVariant(string value)
        {
            return _cssFontVariantMap.GetValueFromString(value, CssFontVariant.Unknown) != CssFontVariant.Unknown;
        }
        public static bool IsFontStyle(string value)
        {
            return _cssFontStyleMap.GetValueFromString(value, CssFontStyle.Unknown) != CssFontStyle.Unknown;
        }
        public static bool IsFontWeight(string value)
        {
            return _cssFontWeightMap.GetValueFromString(value, CssFontWeight.Unknown) != CssFontWeight.Unknown;
        }

        //-----------------------

        public static CssBorderCollapse GetBorderCollapse(WebDom.CssCodeValueExpression value)
        {
            return (CssBorderCollapse)EvaluateIntPropertyValueFromString(
               _cssCollapseBorderMap,
               WebDom.CssValueEvaluatedAs.BorderCollapse,
               CssBorderCollapse.Separate,
               value);
        }

        public static string ToCssStringValue(this CssDisplay value)
        {
            return _cssDisplayMap.GetStringFromValue(value);
        }
        //-----------------------
        static int EvaluateIntPropertyValueFromString<T>(ValueMap<T> map,
            WebDom.CssValueEvaluatedAs
            evalAs,
            T defaultValue,
            WebDom.CssCodeValueExpression value)
            where T : struct
        {
            if (value.EvaluatedAs != evalAs)
            {
                T knownValue = map.GetValueFromString(value.GetTranslatedStringValue(), defaultValue);
                int result = Convert.ToInt32(knownValue);
                value.SetIntValue(result, evalAs);
                return (int)result;
            }
            else
            {
                return value.AsIntValue();
            }
        }
        //-----------------------
        public static CssDisplay GetDisplayType(WebDom.CssCodeValueExpression value)
        {
            return (CssDisplay)EvaluateIntPropertyValueFromString(
                _cssDisplayMap,
                WebDom.CssValueEvaluatedAs.Display,
                CssDisplay.Inline,
                value);
        }
        public static CssBackgroundRepeat GetBackgroundRepeat(WebDom.CssCodeValueExpression value)
        {
            return (CssBackgroundRepeat)EvaluateIntPropertyValueFromString(
                _cssBackgroundRepeatMap,
                WebDom.CssValueEvaluatedAs.BackgroundRepeat,
                CssBackgroundRepeat.Repeat,
                value);
        }
        //----------------------
        public static string ToCssStringValue(this CssDirection value)
        {
            return _cssDirectionMap.GetStringFromValue(value);
        }
        public static CssDirection GetCssDirection(WebDom.CssCodeValueExpression value)
        {
            return (CssDirection)EvaluateIntPropertyValueFromString(
             _cssDirectionMap,
             WebDom.CssValueEvaluatedAs.Direction,
             CssDirection.Ltl,
             value);
        }
        //----------------------
        public static CssPosition GetCssPosition(WebDom.CssCodeValueExpression value)
        {
            return (CssPosition)EvaluateIntPropertyValueFromString(
             _cssPositionMap,
             WebDom.CssValueEvaluatedAs.Position,
             CssPosition.Static,
             value);

        }
        public static CssWordBreak GetWordBreak(WebDom.CssCodeValueExpression value)
        {

            return (CssWordBreak)EvaluateIntPropertyValueFromString(
             _cssWordBreakMap,
             WebDom.CssValueEvaluatedAs.WordBreak,
             CssWordBreak.Normal,
             value);
        }
        public static CssTextDecoration GetTextDecoration(WebDom.CssCodeValueExpression value)
        {
            return (CssTextDecoration)EvaluateIntPropertyValueFromString(
                _cssTextDecorationMap,
                WebDom.CssValueEvaluatedAs.TextDecoration,
                CssTextDecoration.NotAssign,
                value);

        }
        public static CssOverflow GetOverflow(WebDom.CssCodeValueExpression value)
        {
            return (CssOverflow)EvaluateIntPropertyValueFromString(
               _cssOverFlowMap,
               WebDom.CssValueEvaluatedAs.Overflow,
               CssOverflow.Visible,
               value);
        }
        public static CssTextAlign GetTextAlign(WebDom.CssCodeValueExpression value)
        {
            return (CssTextAlign)EvaluateIntPropertyValueFromString(
                _cssTextAlignMap,
                WebDom.CssValueEvaluatedAs.TextAlign,
                CssTextAlign.NotAssign,
                value);
        }

        public static CssVerticalAlign GetVerticalAlign(WebDom.CssCodeValueExpression value)
        {
            return (CssVerticalAlign)EvaluateIntPropertyValueFromString(
            _cssVerticalAlignMap,
            WebDom.CssValueEvaluatedAs.VerticalAlign,
            CssVerticalAlign.Baseline,
            value);
        }


        public static CssVisibility GetVisibility(WebDom.CssCodeValueExpression value)
        {
            return (CssVisibility)EvaluateIntPropertyValueFromString(
            _cssVisibilityMap,
            WebDom.CssValueEvaluatedAs.Visibility,
            CssVisibility.Visible,
            value);
        }
        public static CssWhiteSpace GetWhitespace(WebDom.CssCodeValueExpression value)
        {
            return (CssWhiteSpace)EvaluateIntPropertyValueFromString(
            _cssWhitespaceMap,
            WebDom.CssValueEvaluatedAs.WhiteSpace,
            CssWhiteSpace.Normal,
            value);
        }
        public static CssBorderStyle GetBorderStyle(WebDom.CssCodeValueExpression value)
        {
            return (CssBorderStyle)EvaluateIntPropertyValueFromString(
                   _cssBorderStyleMap,
                   WebDom.CssValueEvaluatedAs.BorderStyle,
                   CssBorderStyle.None,
                   value);
        }
        public static void SetBorderSpacing(this CssBoxBase box, WebDom.CssCodeValueExpression value)
        {
            WebDom.CssCodePrimitiveExpression primValue = value as WebDom.CssCodePrimitiveExpression;
            if (primValue == null)
            {
                //2 values?
                //box.BorderSpacingHorizontal = new CssLength(r[0].Value);
                //box.BorderSpacingVertical = new CssLength(r[1].Value);
                throw new NotSupportedException();
            }
            else
            {
                //primitive value 
                box.BorderSpacingHorizontal = box.BorderSpacingVertical = primValue.AsLength();
            }


            //System.Text.RegularExpressions.MatchCollection r =
            // HtmlRenderer.Parse.RegexParserUtils.Match(HtmlRenderer.Parse.RegexParserUtils.CssLength, value.GetTranslatedStringValue());
            //switch (r.Count)
            //{
            //    case 1:
            //        {
            //            box.BorderSpacingHorizontal = box.BorderSpacingVertical = new CssLength(r[0].Value);
            //        } break;
            //    case 2:
            //        {
            //            box.BorderSpacingHorizontal = new CssLength(r[0].Value);
            //            box.BorderSpacingVertical = new CssLength(r[1].Value);
            //        } break;
            //}
        }
        public static string GetCornerRadius(this CssBox box)
        {
            System.Text.StringBuilder stbuilder = new System.Text.StringBuilder();
            stbuilder.Append(box.CornerNERadius);
            stbuilder.Append(' ');
            stbuilder.Append(box.CornerNWRadius);
            stbuilder.Append(' ');
            stbuilder.Append(box.CornerSERadius);
            stbuilder.Append(' ');
            stbuilder.Append(box.CornerSWRadius);
            return stbuilder.ToString();
        }
        public static void SetCornerRadius(this CssBoxBase box, WebDom.CssCodeValueExpression value)
        {
            WebDom.CssCodePrimitiveExpression prim = value as WebDom.CssCodePrimitiveExpression;
            if (prim == null)
            {
                //combinator values?
                throw new NotSupportedException();
                return;
            }
            box.CornerNERadius = box.CornerNWRadius =
             box.CornerSERadius = box.CornerSWRadius = prim.AsLength();


            ////parse corner radius 
            //System.Text.RegularExpressions.MatchCollection r =
            //    HtmlRenderer.Parse.RegexParserUtils.Match(HtmlRenderer.Parse.RegexParserUtils.CssLength, value);
            //switch (r.Count)
            //{
            //    case 1:
            //        box.CornerNERadius = box.CornerNWRadius =
            //            box.CornerSERadius = box.CornerSWRadius = new CssLength(r[0].Value);
            //        break;
            //    case 2:
            //        box.CornerNERadius = box.CornerNWRadius = new CssLength(r[0].Value);
            //        box.CornerSERadius = box.CornerSWRadius = new CssLength(r[1].Value);
            //        break;
            //    case 3:
            //        box.CornerNERadius = new CssLength(r[0].Value);
            //        box.CornerNWRadius = new CssLength(r[1].Value);
            //        box.CornerSERadius = new CssLength(r[2].Value);
            //        break;
            //    case 4:
            //        box.CornerNERadius = new CssLength(r[0].Value);
            //        box.CornerNWRadius = new CssLength(r[1].Value);
            //        box.CornerSERadius = new CssLength(r[2].Value);
            //        box.CornerSWRadius = new CssLength(r[3].Value);
            //        break;
            //}
        }
        public static CssEmptyCell GetEmptyCell(WebDom.CssCodeValueExpression value)
        {
            //return _cssEmptyCellMap.GetValueFromString(value, CssEmptyCell.Show);

            return (CssEmptyCell)EvaluateIntPropertyValueFromString(
               _cssEmptyCellMap,
               WebDom.CssValueEvaluatedAs.EmptyCell,
               CssEmptyCell.Show, value);

        }

        public static string ToCssStringValue(this CssEmptyCell value)
        {
            return _cssEmptyCellMap.GetStringFromValue(value);
        }
        public static string ToCssStringValue(this CssTextAlign value)
        {
            return _cssTextAlignMap.GetStringFromValue(value);
        }
        public static string ToCssStringValue(this CssTextDecoration value)
        {
            return _cssTextDecorationMap.GetStringFromValue(value);
        }
        public static string ToCssStringValue(this CssWordBreak value)
        {
            return _cssWordBreakMap.GetStringFromValue(value);
        }
        public static string ToCssStringValue(this CssWhiteSpace value)
        {
            return _cssWhitespaceMap.GetStringFromValue(value);

        }
        public static string ToCssStringValue(this CssVisibility value)
        {
            return _cssVisibilityMap.GetStringFromValue(value);

        }
        public static string ToCssStringValue(this CssVerticalAlign value)
        {
            return _cssVerticalAlignMap.GetStringFromValue(value);
        }
        public static string ToCssStringValue(this CssPosition value)
        {
            return _cssPositionMap.GetStringFromValue(value);
        }
        public static CssLength SetLineHeight(this CssBoxBase box, CssLength len)
        {
            return CssLength.MakePixelLength(HtmlRenderer.Parse.CssValueParser.ParseLength(len, box.SizeHeight, box, CssUnit.Ems));

        }
        public static HtmlRenderer.WebDom.WellknownCssPropertyName GetWellKnownPropName(string propertyName)
        {
            return _wellKnownCssPropNameMap.GetValueFromString(propertyName, WebDom.WellknownCssPropertyName.Unknown);
        }
        public static CssFloat GetFloat(WebDom.CssCodeValueExpression value)
        {
            return (CssFloat)EvaluateIntPropertyValueFromString(
                _cssFloatMap,
                WebDom.CssValueEvaluatedAs.Float,
                CssFloat.None,
                value);
        }

        public static string ToCssStringValue(this CssFloat cssFloat)
        {
            return _cssFloatMap.GetStringFromValue(cssFloat);
        }
        public static string ToCssStringValue(this CssBorderStyle borderStyle)
        {
            return _cssBorderStyleMap.GetStringFromValue(borderStyle);
        }
        public static string ToCssStringValue(this CssBorderCollapse borderCollapse)
        {
            return _cssCollapseBorderMap.GetStringFromValue(borderCollapse);
        }
        public static string ToCssStringValue(this CssBackgroundRepeat backgrounRepeat)
        {
            return _cssBackgroundRepeatMap.GetStringFromValue(backgrounRepeat);
        }
        public static string ToHexColor(this Color color)
        {
            return string.Concat("#", color.R.ToString("X"), color.G.ToString("X"), color.B.ToString("X"));
        }
        public static string ToCssStringValue(this CssFontStyle fontstyle)
        {
            return _cssFontStyleMap.GetStringFromValue(fontstyle);
        }
        public static CssFontStyle GetFontStyle(WebDom.CssCodeValueExpression value)
        {
            return (CssFontStyle)EvaluateIntPropertyValueFromString(
              _cssFontStyleMap,
              WebDom.CssValueEvaluatedAs.FontStyle,
              CssFontStyle.Normal,
              value);

        }
        public static string ToCssStringValue(this CssFontVariant fontVariant)
        {
            return _cssFontVariantMap.GetStringFromValue(fontVariant);
        }
        public static CssFontVariant GetFontVariant(WebDom.CssCodeValueExpression value)
        {
            return (CssFontVariant)EvaluateIntPropertyValueFromString(
              _cssFontVariantMap,
              WebDom.CssValueEvaluatedAs.FontVariant,
              CssFontVariant.Normal,
              value);
        }
        public static string ToFontSizeString(this CssLength length)
        {
            if (length.IsFontSizeName)
            {
                switch (length.FontSizeName)
                {
                    case CssFontSizeConst.FONTSIZE_MEDIUM:
                        return CssConstants.Medium;
                    case CssFontSizeConst.FONTSIZE_SMALL:
                        return CssConstants.Small;
                    case CssFontSizeConst.FONTSIZE_X_SMALL:
                        return CssConstants.XSmall;
                    case CssFontSizeConst.FONTSIZE_XX_SMALL:
                        return CssConstants.XXSmall;
                    case CssFontSizeConst.FONTSIZE_LARGE:
                        return CssConstants.Large;
                    case CssFontSizeConst.FONTSIZE_X_LARGE:
                        return CssConstants.XLarge;
                    case CssFontSizeConst.FONTSIZE_XX_LARGE:
                        return CssConstants.XXLarge;
                    case CssFontSizeConst.FONTSIZE_LARGER:
                        return CssConstants.Larger;
                    case CssFontSizeConst.FONTSIZE_SMALLER:
                        return CssConstants.Smaller;
                    default:
                        return "";
                }
            }
            else
            {
                return length.ToString();
            }
        }

        internal static void SetFontSize(this CssBoxBase box, CssBoxBase parentBox, WebDom.CssCodeValueExpression value)
        {
            //number + value
            WebDom.CssCodePrimitiveExpression primValue = value as WebDom.CssCodePrimitiveExpression;
            if (primValue == null)
            {
                return;
            }
            switch (primValue.Hint)
            {
                case WebDom.CssValueHint.Number:
                    {
                        //has unit or not
                        //?
                        //or percent ? 
                        CssLength len = primValue.AsLength();
                        if (len.HasError)
                        {
                            len = CssLength.FontSizeMedium;
                        }
                        else if (len.Unit == CssUnit.Ems && (parentBox != null))
                        {
                            len = len.ConvertEmToPoints(parentBox.ActualFont.SizeInPoints);
                        }
                        else
                        {

                        }
                        box.FontSize = len;

                    } break;
                case WebDom.CssValueHint.Iden:
                    {
                        switch (primValue.GetTranslatedStringValue())
                        {
                            default:
                                {
                                    throw new NotSupportedException();
                                }
                            case CssConstants.Medium:
                                box.FontSize = CssLength.FontSizeMedium;
                                break;
                            case CssConstants.Small:
                                box.FontSize = CssLength.FontSizeSmall;
                                break;
                            case CssConstants.XSmall:
                                box.FontSize = CssLength.FontSizeXSmall;
                                break;
                            case CssConstants.XXSmall:
                                box.FontSize = CssLength.FontSizeXXSmall;
                                break;
                            case CssConstants.Large:
                                box.FontSize = CssLength.FontSizeLarge;
                                break;
                            case CssConstants.XLarge:
                                box.FontSize = CssLength.FontSizeLarge;
                                break;
                            case CssConstants.XXLarge:
                                box.FontSize = CssLength.FontSizeLarger;
                                break;
                            case CssConstants.Smaller:
                                box.FontSize = CssLength.FontSizeSmaller;
                                break;
                            case CssConstants.Larger:
                                box.FontSize = CssLength.FontSizeLarger;
                                break;
                        }
                    } break;
            }
        }


        public static CssFontWeight GetFontWeight(WebDom.CssCodeValueExpression value)
        {
            return (CssFontWeight)EvaluateIntPropertyValueFromString(
                _cssFontWeightMap,
                WebDom.CssValueEvaluatedAs.Float,
                CssFontWeight.NotAssign,
                value);
        }

        public static string ToCssStringValue(this CssFontWeight weight)
        {
            return _cssFontWeightMap.GetStringFromValue(weight);
        }

        public static string ToCssStringValue(this CssListStylePosition listStylePosition)
        {
            return _cssListStylePositionMap.GetStringFromValue(listStylePosition);
        }
        public static CssListStylePosition GetListStylePosition(WebDom.CssCodeValueExpression value)
        {
            return (CssListStylePosition)EvaluateIntPropertyValueFromString(
               _cssListStylePositionMap,
               WebDom.CssValueEvaluatedAs.ListStylePosition,
               CssListStylePosition.Outside,
               value);

        }

        public static string ToCssStringValue(this CssListStyleType listStyleType)
        {
            return _cssListStyleTypeMap.GetStringFromValue(listStyleType);
        }
        public static CssListStyleType GetListStyleType(WebDom.CssCodeValueExpression value)
        {
            return (CssListStyleType)EvaluateIntPropertyValueFromString(
            _cssListStyleTypeMap,
            WebDom.CssValueEvaluatedAs.ListStyleType,
            CssListStyleType.Disc,
            value);
        }


        public static WellknownHtmlTagName EvaluateTagName(string name)
        {
            return _wellknownHtmlTagNameMap.GetValueFromString(name, WellknownHtmlTagName.Unknown);
        }
        internal static void SetBackgroundPosition(this CssBoxBase box, WebDom.CssCodeValueExpression value)
        {
            //TODO: implement background position from combination value
            throw new NotSupportedException();
        }
    }


}