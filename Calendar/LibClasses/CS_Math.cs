﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendare.LibClasses
{
    public static class CS_Math
    {
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if(value.CompareTo(min) < 0) return min;
            if(value.CompareTo(max) > 0) return max;

            return value;
        }



    }
}
