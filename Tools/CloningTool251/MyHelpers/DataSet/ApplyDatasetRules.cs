using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using MyHelpers.Properties;

namespace MyHelpers
{
    class ApplyDatasetRules
    {
        DataSet TargetDataSet;

        public ApplyDatasetRules(DataSet TargetDataSet)
        {
            this.TargetDataSet = TargetDataSet;
        }

        #region public static functions
        static public void ApplyChangesFromFile(DataSet Data, StreamReader RuleFile, MyParameters param)
        {
            ApplyDatasetRules changes = new ApplyDatasetRules(Data);
            string[] rules = LoadRuleFile(RuleFile, param.ToDictionary());

            changes.ApplyChange(rules);
        }

        static public void ChangeScheme(DataSet Data, MyParameters param)
        {
            ApplyChangesFromFile(Data, new EmbeddedResource().GetStreamReader(Resource.SchemeRule), param);
        }

        static public void ChangeProduct(DataSet Data, MyParameters param)
        {
            ApplyChangesFromFile(Data, new EmbeddedResource().GetStreamReader(Resource.ProductRule), param);
        }

        static public void ChangeMasterCompanyNumber(DataSet Data, MyParameters param)
        {
            ApplyChangesFromFile(Data, new EmbeddedResource().GetStreamReader(Resource.MasterCompanyRule), param);
        }

        static public void ChangeEffectiveDate(DataSet Data, MyParameters param)
        {
            ApplyChangesFromFile(Data, new EmbeddedResource().GetStreamReader(Resource.EffectiveDateRule), param);
        }

        static public void ChangeExpirationDate(DataSet Data, MyParameters param)
        {
            ApplyChangesFromFile(Data, new EmbeddedResource().GetStreamReader(Resource.ExpirationDateRule), param);
        }


        #endregion


        #region private function

        private void ApplyChange(string[] rules)
        {
            CommandList list = new CommandList();

            foreach (string rule in rules)
            {
                string datasetName = rule.Split(':')[1];

                if (TargetDataSet == null)
                {
                    continue;
                    //throw new Exception(CurrentRule + ": Table '" + datasetName + "' from rule file not found in download data.");
                }

                DataSetCommand command = new DataSetCommand(TargetDataSet);

                command.AddInstruction(rule);
                list.Add(command as ICommand<Object>);
            }

            list.Do();
        }
        #endregion

        #region private static functions
        private static string[] LoadRuleFile(StreamReader reader, Dictionary<string, string> ParameterList)
        {
            List<string> rules = new List<string>();
            //StreamReader reader = CacheFileReader.ReadFile(session.GetConfiguration().RulesFolder() + fileName);
            //CurrentRule = Path.GetFileNameWithoutExtension(fileName);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Trim();

                if (line.Length == 0) continue;

                if (!line.Substring(0,1).Equals("X"))
                    rules.Add(ReplaceParameterString(line, ParameterList));
            }
            //reader.Close();

            return rules.ToArray();
        }

        private static string ReplaceParameterString(string input,Dictionary<string,string> ParameterList)
        {
            foreach (string key in ParameterList.Keys)
            {
                input = input.Replace(key, ParameterList[key]);
            }
            return input;
        }

        #endregion private static functions
    }
}
