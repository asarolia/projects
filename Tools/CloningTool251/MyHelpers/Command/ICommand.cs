using System;
using System.Data;
using System.Configuration;
using System.Web;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for ICommand
    /// </summary>
    abstract public class ICommand<T>
    {
        public ICommand()
        {
        }

        public ICommand(T obj)
        {
        }

        abstract public void AddInstruction(string Instruction);
        abstract public void Do();
        abstract public void Undo();
        abstract public T GetClientObject();
        abstract public string GetStatus();
    }
}