﻿namespace Common.Data.Objects.Words
{
    public class InsertWordContextDto 
    {
        public string ContextDesc { get; set; }
        public int SortOrder { get; set; }
        public List<InsertWordExample> Examples { get; set; }
    }
}
