using System;

namespace DevTool
{
    public class KeyValue
    {
        public String Name { get; set; }

        public int Value { get; set; }

        public KeyValue(String name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public KeyValue() { }
    }
}
