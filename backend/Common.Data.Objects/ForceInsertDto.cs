namespace Common.Data.Objects
{
    public abstract class ForceInsertDto
    {
        //TODO - add logic around this - if true then don't check if database record first
        //Used as a way for faster data processing in batches when you know its new data
        public bool ForceInsert { get; set; }
    }
}
