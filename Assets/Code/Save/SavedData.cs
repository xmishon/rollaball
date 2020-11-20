using System;
using System.Collections.Generic;

namespace mzmeevskiy
{
    [Serializable]
    public sealed class SavedData
    {
        //public string Version = "1.0.0";
        public List<SavedDataItem> SavedDataItmes;
        public int Points;
    }
}
