using System;
using System.Collections.Generic;
using System.Web;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for ModuleEntry
    /// </summary>
    public class ModuleEntry
    {
        string _name, _type;
        int _stage, _version, _level;
        public ModuleEntry(string Name, string Type, int Stage, int Version, int Level)
        {
            _name = Name;
            _type = Type;
            _stage = Stage;
            _version = Version;
            _level = Level;
        }

        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3},{4}", _name, _type, _stage, _version, _level);
        }
    }
}