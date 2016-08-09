using System;
using System.Collections.Generic;
using System.Web;
using System.Xml.Serialization;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using MyHelpers.Properties;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for RelataionalDatabaseConfig
    /// </summary>
    public class RelataionalDatabaseConfig
    {
        private string file = "", _previousToken = "", _lastLine = "";
        private int State = 1, _previousIndex;  //1- initial
        private Dictionary<string, string> param = new Dictionary<string, string>();
        private List<string> _previousKeys = new List<string>();
        private StreamReader _reader;

        private string ApplyParam(string str)
        {
            string ret = str;
            foreach (string key in param.Keys)
            {
                ret = ret.Replace("%" + key.ToUpper().Trim() + "%", param[key]);
            }
            return ret;
        }

        private string PrepareQuery(string Table, string Where)
        {
            return ApplyParam(String.Format("select * from %{0}%.{1} {2}", Resource.LiteralRegion, Table, (Where.Length > 0) ? String.Format("where {0}", Where) : String.Empty));
        }

        private string ReadNextValue(DataSet ds, string ForeignRelation)
        {
            try
            {
                if (_previousToken.Length > 0 && ForeignRelation == null)
                {
                    if (++_previousIndex < _previousKeys.Count)
                        return _previousKeys[_previousIndex];
                    else
                    {
                        _previousToken = "";
                        return null;
                    }
                }
                else
                {
                    string table, column, temp;
                    parseForeignRelation(ForeignRelation, out temp, out table, out column);

                    _previousKeys = DataSetHelper.GetColumnValues(ds, table, column, null);

                    if (_previousKeys.Count == 0) return null;

                    _previousIndex = 0;
                    _previousToken = ForeignRelation;
                    return _previousKeys[_previousIndex];
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("RelationalDatabaseConfig: ReadNextValue - " + e.Message);
            }
        }

        private void parseForeignRelation(string Line, out string CurrentColumn, out string Table, out string Column)
        {
            try
            {
                CurrentColumn = Table = Column = "";

                if (Line.Length == 0) return;

                CurrentColumn = Regex.Split(Line, @"->")[0];
                Line = Regex.Split(Line, @"->")[1];

                Table = Line.Split('.')[0];
                Column = Line.Split('.')[1];
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("RelationalDatabaseConfig: parseForeignRelation on '" + Line + "' failed - " + e.Message);
            }

        }

        private void parseLine(string Line, out string Table, out string Where, out string ForeignRelation)
        {
            try
            {
                Table = Where = ForeignRelation = "";

                if (Line.Length == 0) return;

                string[] words = Line.Split(new char[] { '\t', ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                Table = words[0];

                if (words[1].Contains("->"))
                    ForeignRelation = words[1];
                else
                    Where = words[1];
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("RelationalDatabaseConfig: parseLine on '" + Line + "' failed - " + e.Message);
            }
        }

        private string ReadNextLine()
        {
            try
            {
                try
                {
                    if (_reader == null)
                    {
                        if (file == null)
                            _reader = new EmbeddedResource().GetStreamReader(Resource.PolicyRelationFile);
                        else
                            _reader = File.OpenText(file);                    
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("RelationalDatabaseConfig: ReadNextLine - " + e.Message);
                }

                if (_reader.EndOfStream)
                {
                    _reader.Close();
                    return null;
                }

                string line = _reader.ReadLine();

                if (line.Trim().Length > 0)
                    return line.Trim();
                else
                    return ReadNextLine();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("RelationalDatabaseConfig: ReadNextLine - " + e.Message);
            }
        }
        /// <summary>
        /// Pick relation file from stream
        /// </summary>
        public RelataionalDatabaseConfig()
        {
            file = null;
        }

        public RelataionalDatabaseConfig(string FileName)
        {
            file = FileName;
        }

        public void SetParameter(Dictionary<string, string> p)
        {
            param = p;
        }

        public bool ReadNext(DataSet currentData, out string TableName, out string Query)
        {
            TableName = "";
            Query = "";


            string mtable, mcolumn, mforeign, mwhere, mvalue, temp1, temp2;


            switch (State)
            {
                case 1: //initial state

                    try
                    {
                        _lastLine = ReadNextLine();
                        if (_lastLine == null) return false;

                        parseLine(_lastLine, out mtable, out mwhere, out mforeign);
                        State = 2;

                        TableName = mtable;
                        Query = PrepareQuery(mtable, mwhere);
                    }
                    catch (Exception e)
                    {
                        throw new InvalidOperationException("RelationalDatabaseConfig: ReadNext 1 State=" + State.ToString() + " - " + e.Message);
                    }
                    return true;

                case 2: //where results are supplied

                    try
                    {
                        _lastLine = ReadNextLine();
                        if (_lastLine == null) return false;

                        parseLine(_lastLine, out mtable, out mwhere, out mforeign);

                        //prepare where
                        if (mforeign.Length > 0)
                        {
                            mvalue = ReadNextValue(currentData, mforeign);
                            if (mvalue == null)  //if foreign parent don't have any values, fetch next entry from configuration
                                return ReadNext(currentData, out TableName, out Query);

                            parseForeignRelation(mforeign, out mcolumn, out temp1, out temp2);

                            mwhere = String.Format("{0}='{1}'", mcolumn, mvalue);

                        }

                        TableName = mtable;
                        Query = PrepareQuery(mtable, mwhere);

                        State = 3;
                    }
                    catch (Exception e)
                    {
                        throw new InvalidOperationException("RelationalDatabaseConfig: ReadNext 2 State=" + State.ToString() + " - " + e.Message);
                    }

                    return true;

                case 3: //check parent results to find more than 1 row

                    try
                    {
                        mvalue = ReadNextValue(currentData, null);

                        if (mvalue == null)
                        {
                            State = 2;
                            return ReadNext(currentData, out TableName, out Query);
                        }

                        parseLine(_lastLine, out mtable, out mwhere, out mforeign);
                        parseForeignRelation(mforeign, out mcolumn, out temp1, out temp2);
                        mwhere = String.Format("{0}='{1}'", mcolumn, mvalue);
                        TableName = mtable;
                        Query = PrepareQuery(mtable, mwhere);
                    }
                    catch (Exception e)
                    {
                        throw new InvalidOperationException("RelationalDatabaseConfig: ReadNext 3 State=" + State.ToString() + " - " + e.Message);
                    }

                    return true;
            }
            return false;
        }
    }
}