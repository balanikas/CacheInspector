namespace CacheInspector
{
    public enum CommandType
    {
        Help,
        List,
        ListInventorySystem,
        Invalid,
        Clear
    }

    class Command
    {
        public CommandType Cmd { get; set; }
        public string Key { get; set; } = "";
        public bool ShowDetails { get; set; }
    }
}