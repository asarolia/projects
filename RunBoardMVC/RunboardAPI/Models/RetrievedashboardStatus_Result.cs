using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RunboardAPI.Models
{
    public class RetrievedashboardStatus_Result
    {
        public int Delay { get; set; }

        public int MdmTotal { get; set; }
        public int MdmProcessed { get; set; }
        public int MdmUnprocessed { get; set; }
        public int MdmFailed { get; set; }
        public int MdmFTR { get; set; }
        public int MdmUTR { get; set; }
        public int MdmFTA { get; set; }
        public int MdmUTA { get; set; }

        public int BatchCNT { get; set; }
        public int BatchFTR { get; set; }
        public int BatchFTA { get; set; }

        public int HsbcProcessed { get; set; }
        public int HsbcSuccess { get; set; }
        public int HsbcFail { get; set; }
        public int HsbcFTR { get; set; }
        public int HsbcFTA { get; set; }

        public int BProcessed { get; set; }
        public int BSuccess { get; set; }
        public int BFail { get; set; }
        public int BFTR { get; set; }
        public int BFTA { get; set; }

        public int SProcessed { get; set; }
        public int SSuccess { get; set; }
        public int SFail { get; set; }
        public int SFTR { get; set; }
        public int SFTA { get; set; }

        public string Jobname { get; set; }
        public Nullable<int> End_Time { get; set; }

        public int CPL2Total { get; }
        public int CPL2Processed { get; set; }
        public int CPL2Unprocessed { get; set; }
        public int CPL2Failed { get; set; }
        public int CPL2FTR { get; set; }
        public int CPL2UTR { get; set; }
        public int CPL2FTA { get; set; }
        public int CPL2UTA { get; set; }


        public int CPLSTGOTotal { get; }
        public int CPLSTGOProcessed { get; set; }
        public int CPLSTGOUnprocessed { get; set; }
        public int CPLSTGOFailed { get; set; }
        public int CPLSTGOFTR { get; set; }
        public int CPLSTGOUTR { get; set; }
        public int CPLSTGOFTA { get; set; }
        public int CPLSTGOUTA { get; set; }


        public int CPLSTGTTotal { get; }
        public int CPLSTGTProcessed { get; set; }
        public int CPLSTGTUnprocessed { get; set; }
        public int CPLSTGTFailed { get; set; }
        public int CPLSTGTFTR { get; set; }
        public int CPLSTGTUTR { get; set; }
        public int CPLSTGTFTA { get; set; }
        public int CPLSTGTUTA { get; set; }

        public int TITotal { get; }
        public int TIProcessed { get; set; }
        public int TIUnprocessed { get; set; }
        public int TIFailed { get; set; }
        public int TIFTR { get; set; }
        public int TIUTR { get; set; }
        public int TIFTA { get; set; }
        public int TIUTA { get; set; }

        public int AnolTotal { get; }
        public int AnolProcessed { get; set; }
        public int AnolUnprocessed { get; set; }
        public int AnolFailed { get; set; }
        public int AnolFTR { get; set; }
        public int AnolUTR { get; set; }
        public int AnolFTA { get; set; }
        public int AnolUTA { get; set; }

        public int EmailTotal { get; }
        public int EmailProcessed { get; set; }
        public int EmailUnprocessed { get; set; }
        public int EmailFailed { get; set; }
        public int EmailGTFTR { get; set; }
        public int EmailGTUTR { get; set; }
        public int EmailGTFTA { get; set; }
        public int EmailGTUTA { get; set; }


        public int GfifTotal { get; }
        public int GfifProcessed { get; set; }
        public int GfifUnprocessed { get; set; }
        public int GfifFailed { get; set; }
        public int GfifFTR { get; set; }
        public int GfifUTR { get; set; }
        public int GfifFTA { get; set; }
        public int GfifUTA { get; set; }


        public int RenewalTotal { get; }
        public int RenewalProcessed { get; set; }
        public int RenewalUnprocessed { get; set; }
        public int RenewalFailed { get; set; }
        public int RenewalFTR { get; set; }
        public int RenewalUTR { get; set; }
        public int RenewalFTA { get; set; }
        public int RenewalUTA { get; set; }

        public int CardTotal { get; }
        public int CardProcessed { get; set; }
        public int CardUnprocessed { get; set; }
        public int CardFailed { get; set; }
        public int CardFTR { get; set; }
        public int CardUTR { get; set; }
        public int CardFTA { get; set; }
        public int CardUTA { get; set; }


        public int WamiTotal { get; }
        public int WamiProcessed { get; set; }
        public int WamiUnprocessed { get; set; }
        public int WamiFailed { get; set; }
        public int WamiFTR { get; set; }
        public int WamiUTR { get; set; }
        public int WamiFTA { get; set; }
        public int WamiUTA { get; set; }

        public int OracleTotal { get; }
        public int OracleProcessed { get; set; }
        public int OracleUnprocessed { get; set; }
        public int OracleFailed { get; set; }
        public int OracleFTR { get; set; }
        public int OracleUTR { get; set; }
        public int OracleFTA { get; set; }
        public int OracleUTA { get; set; }

        public int PitsTotal { get; }
        public int PitsProcessed { get; set; }
        public int PitsUnprocessed { get; set; }
        public int PitsFailed { get; set; }
        public int PitsFTR { get; set; }
        public int PitsUTR { get; set; }
        public int PitsFTA { get; set; }
        public int PitsUTA { get; set; }

        public int PrintTotal { get; }
        public int PrintProcessed { get; set; }
        public int PrintUnprocessed { get; set; }
        public int PrintFailed { get; set; }
        public int PrintFTR { get; set; }
        public int PrintUTR { get; set; }
        public int PrintFTA { get; set; }
        public int PrintUTA { get; set; }




    }
}