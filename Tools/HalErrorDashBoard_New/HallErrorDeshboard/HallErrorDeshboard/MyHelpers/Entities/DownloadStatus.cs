using System;
using System.Collections.Generic;
using System.Text;

namespace MyHelpers
{
    public class DownloadStatus
    {
        public enum DownloadStatusStages
        {
            PreProcessing,
            Processing,
            PostProcessing
        }

        public DownloadStatusStages Stage { get; set; }
        public bool ErrorFlag { get; set; }
        public int PercentageComplete { get; set; }
        public string Description { get; set; }
        public string SecondaryDescription { get; set; }
        public string ErrorMessage { get; set; }

        public DownloadStatus()
        {
            ErrorFlag = false;
            ErrorMessage = "";

            PercentageComplete = 0;
            Description = "";
            SecondaryDescription = "";
        }

        public DownloadStatus(DownloadStatusStages stage, int CompletionPercentage, string Description, string SecondaryDescription) : this()
        {
            this.Stage = stage;
            this.PercentageComplete = CompletionPercentage;
            this.Description = Description;
            this.SecondaryDescription = SecondaryDescription;
        }

        public void SetError(string Message)
        {
            ErrorFlag = true;
            ErrorMessage = Message;
        }

        public bool IsLastMessage()
        {
            if (ErrorFlag == true)
                return true;

            if (Stage == DownloadStatusStages.PostProcessing && PercentageComplete == 100)
                return true;

            return false;
        }

    }
}
