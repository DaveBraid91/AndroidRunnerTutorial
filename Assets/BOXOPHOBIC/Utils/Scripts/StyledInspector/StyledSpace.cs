﻿using UnityEngine;

namespace BOXOPHOBIC.Utils.Scripts.StyledInspector
{
    public class StyledSpace : PropertyAttribute
    {
        public int space;

        public StyledSpace(int space)
        {
            this.space = space;
        }
    }
}

