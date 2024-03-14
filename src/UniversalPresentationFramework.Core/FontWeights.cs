﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.UI
{
    /// <summary>
    /// FontWeights contains predefined font weight structures for common font weights.
    /// </summary>
    public static class FontWeights
    {
        /// <summary>
        /// Predefined font weight : Thin.
        /// </summary>
        public static FontWeight Thin { get { return new FontWeight(100); } }

        /// <summary>
        /// Predefined font weight : Extra-light.
        /// </summary>
        public static FontWeight ExtraLight { get { return new FontWeight(200); } }

        /// <summary>
        /// Predefined font weight : Ultra-light.
        /// </summary>
        public static FontWeight UltraLight { get { return new FontWeight(200); } }

        /// <summary>
        /// Predefined font weight : Light.
        /// </summary>
        public static FontWeight Light { get { return new FontWeight(300); } }

        /// <summary>
        /// Predefined font weight : Normal.
        /// </summary>
        public static FontWeight Normal { get { return new FontWeight(400); } }

        /// <summary>
        /// Predefined font weight : Regular.
        /// </summary>
        public static FontWeight Regular { get { return new FontWeight(400); } }

        /// <summary>
        /// Predefined font weight : Medium.
        /// </summary>
        public static FontWeight Medium { get { return new FontWeight(500); } }

        /// <summary>
        /// Predefined font weight : Demi-bold.
        /// </summary>
        public static FontWeight DemiBold { get { return new FontWeight(600); } }

        /// <summary>
        /// Predefined font weight : Semi-bold.
        /// </summary>
        public static FontWeight SemiBold { get { return new FontWeight(600); } }

        /// <summary>
        /// Predefined font weight : Bold.
        /// </summary>
        public static FontWeight Bold { get { return new FontWeight(700); } }

        /// <summary>
        /// Predefined font weight : Extra-bold.
        /// </summary>
        public static FontWeight ExtraBold { get { return new FontWeight(800); } }

        /// <summary>
        /// Predefined font weight : Ultra-bold.
        /// </summary>
        public static FontWeight UltraBold { get { return new FontWeight(800); } }

        /// <summary>
        /// Predefined font weight : Black.
        /// </summary>
        public static FontWeight Black { get { return new FontWeight(900); } }

        /// <summary>
        /// Predefined font weight : Heavy.
        /// </summary>
        public static FontWeight Heavy { get { return new FontWeight(900); } }

        /// <summary>
        /// Predefined font weight : ExtraBlack.
        /// </summary>
        public static FontWeight ExtraBlack { get { return new FontWeight(950); } }

        /// <summary>
        /// Predefined font weight : UltraBlack.
        /// </summary>
        public static FontWeight UltraBlack { get { return new FontWeight(950); } }

        internal static bool FontWeightStringToKnownWeight(string s, IFormatProvider? provider, ref FontWeight fontWeight)
        {
            switch (s.Length)
            {
                case 4:
                    if (s.Equals("Bold", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = Bold;
                        return true;
                    }
                    if (s.Equals("Thin", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = Thin;
                        return true;
                    }
                    break;

                case 5:
                    if (s.Equals("Black", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = Black;
                        return true;
                    }
                    if (s.Equals("Light", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = Light;
                        return true;
                    }
                    if (s.Equals("Heavy", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = Heavy;
                        return true;
                    }
                    break;

                case 6:
                    if (s.Equals("Normal", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = Normal;
                        return true;
                    }
                    if (s.Equals("Medium", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = Medium;
                        return true;
                    }
                    break;

                case 7:
                    if (s.Equals("Regular", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = Regular;
                        return true;
                    }
                    break;

                case 8:
                    if (s.Equals("SemiBold", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = SemiBold;
                        return true;
                    }
                    if (s.Equals("DemiBold", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = DemiBold;
                        return true;
                    }
                    break;

                case 9:
                    if (s.Equals("ExtraBold", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = ExtraBold;
                        return true;
                    }
                    if (s.Equals("UltraBold", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = UltraBold;
                        return true;
                    }
                    break;

                case 10:
                    if (s.Equals("ExtraLight", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = ExtraLight;
                        return true;
                    }
                    if (s.Equals("UltraLight", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = UltraLight;
                        return true;
                    }
                    if (s.Equals("ExtraBlack", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = ExtraBlack;
                        return true;
                    }
                    if (s.Equals("UltraBlack", StringComparison.OrdinalIgnoreCase))
                    {
                        fontWeight = UltraBlack;
                        return true;
                    }
                    break;
            }
            int weightValue;
            if (int.TryParse(s, NumberStyles.Integer, provider, out weightValue))
            {
                fontWeight = FontWeight.FromOpenTypeWeight(weightValue);
                return true;
            }
            return false;
        }

        internal static bool FontWeightToString(int weight, out string? convertedValue)
        {
            switch (weight)
            {
                case 100:
                    convertedValue = "Thin";
                    return true;
                case 200:
                    convertedValue = "ExtraLight";
                    return true;
                case 300:
                    convertedValue = "Light";
                    return true;
                case 400:
                    convertedValue = "Normal";
                    return true;
                case 500:
                    convertedValue = "Medium";
                    return true;
                case 600:
                    convertedValue = "SemiBold";
                    return true;
                case 700:
                    convertedValue = "Bold";
                    return true;
                case 800:
                    convertedValue = "ExtraBold";
                    return true;
                case 900:
                    convertedValue = "Black";
                    return true;
                case 950:
                    convertedValue = "ExtraBlack";
                    return true;
            }
            convertedValue = null;
            return false;
        }
    }
}
