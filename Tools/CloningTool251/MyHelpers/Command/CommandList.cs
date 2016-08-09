using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for ExecuteCommands
    /// </summary>
    public class CommandList
    {
        List<ICommand<Object>> list = new List<ICommand<Object>>();

        public CommandList()
        {

        }
        public void Add(ICommand<Object> command)
        {
            list.Add(command);
        }

        public void Do()
        {
            foreach (ICommand<Object> command in list)
            {
                command.Do();
            }
        }

    }
}