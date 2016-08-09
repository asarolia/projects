using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for DataSetCommand
    /// </summary>
    public class DataSetCommand : ICommand<Object>
    {
        class Instruction
        {
            public String TableName, ColumnName, Operation, OldValue, NewValue;
            public int StartPosition, Length;
            struct Result
            {
                public int RowNumber;
                public string OldValue, NewValue;
            }
            private List<Result> results;

            public Instruction(String table, string column, string operation, string oldValue, string newValue)
            {
                results = new List<Result>();
                TableName = table;
                ColumnName = column;
                Operation = operation;
                OldValue = oldValue;
                NewValue = newValue;

                StartPosition = 0;
                Length = 0;

                VerifyPostionAndLength();
            }

            public void VerifyPostionAndLength()
            {
                string col = ColumnName;
                string rest;

                if (ColumnName.Contains("("))
                    ColumnName = ColumnName.Substring(0, ColumnName.IndexOf('('));
                else
                    return;

                rest = col.Substring(col.IndexOf('(') + 1, col.IndexOf(')') - col.IndexOf('(') - 1);
                StartPosition = Int32.Parse(rest.Split(' ')[0]) - 1;
                Length = Int32.Parse(rest.Split(' ')[1]);

                if (StartPosition < 0 || Length < 1)
                    throw new Exception(String.Format("Position({0}) & Length({1}) are not valid.", StartPosition.ToString(), Length.ToString()));

            }

            public void AddResult(int index, string existingValue, string latestValue)
            {
                Result r = new Result();
                r.RowNumber = index;
                r.OldValue = existingValue;
                r.NewValue = latestValue;
                results.Add(r);
            }

            public void Do(DataSet dataset)
            {
                DataRowCollection Rows = dataset.Tables[TableName].Rows;

                if (Rows.Count == 0)
                    return;

                DataRow Row;
                for (int i = 0; i < Rows.Count; i++)
                {
                    Row = Rows[i];

                    AddResult(i, Row[ColumnName].ToString(), GetModifiedValue(Row[ColumnName].ToString()));
                    Row[ColumnName] = GetModifiedValue(Row[ColumnName].ToString());
                }
            }

            private string GetModifiedValue(string ExistingValue)
            {
                if (StartPosition == 0 & Length == 0)
                    return NewValue;
                string pre = "", post = "";

                if (StartPosition > 0)
                    pre = ExistingValue.Substring(0, StartPosition);

                post = ExistingValue.Substring(StartPosition + Length);

                return pre + NewValue + post;
            }
        }

        private DataSet dataset;
        private List<Instruction> InstructionList;

        public DataSetCommand()
        {
            InstructionList = new List<Instruction>();
        }
        public DataSetCommand(Object obj)
        {
            if (obj.GetType() != typeof(DataSet))
                throw new Exception("Could not create DataSetCommand for given input.");

            SetClientObject(obj as DataSet);

            InstructionList = new List<Instruction>();
        }

        private void SetClientObject(DataSet obj)
        {
            dataset = obj;
        }

        public override void AddInstruction(string Instruction)
        {
            //instruction format U:TABLE_NAME:COLUMN=NewValue,COLUMN=Newvalue
            String TableName, ColumnName, Operation, OldValue, NewValue;
            try
            {
                string[] instructions = Instruction.Split(':');
                Operation = instructions[0];
                TableName = instructions[1];
                if (!dataset.Tables.Contains(TableName))
                    //throw new InvalidOperationException("Table name " + TableName + " not exists in dataset.");
                    return;

                string[] columns = instructions[2].Split(',');

                foreach (string column in columns)
                {
                    string[] columnInstruction = column.Split('=');
                    ColumnName = columnInstruction[0];


                    NewValue = columnInstruction[1];

                    OldValue = "";
                    //if (dataset.Tables[TableName].Rows.Count > 0)
                    //    OldValue = dataset.Tables[TableName].Rows[0][ColumnName].ToString().Trim();

                    Instruction ins = new Instruction(TableName, ColumnName, Operation, OldValue, NewValue);

                    if (dataset.Tables[ins.TableName].Columns[ins.ColumnName] == null)
                        throw new InvalidOperationException("Column name " + ins.ColumnName + " not valid in table " + ins.TableName);

                    InstructionList.Add(ins);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Command instruction is invalid '" + Instruction + "'." + e.Message);
            }
        }

        public override void Do()
        {
            foreach (Instruction i in InstructionList)
            {
                try
                {
                    i.Do(dataset);
                }
                catch (Exception e)
                {
                    throw new Exception("Apply Changes failed for '" + dataset.Tables[0].TableName + "'." + e.Message);
                }
            }
        }

        public override void Undo()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Object GetClientObject()
        {
            return dataset;
        }

        public override string GetStatus()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}