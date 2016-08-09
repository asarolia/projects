using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucene.Net.Search;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using System.IO;

namespace EASE
{
    public class KMSearch
    {
        //a path to directory where Lucene will store index files
        private static String indexDirectory = "\\\\20.198.58.156\\Exceed-Run\\kmindex";
        // a path to directory which contains data files that need to be indexed
        //	private static String dataDirectory = "C:\\Users\\asarolia\\Downloads\\MF training";
       // private String dataDirectory = "\\\\20.198.58.156\\Exceed-Run\\kmsearch";
       // private static String search = "";
        private String file = "";
        private String path = "";
        public IndexSearcher indexSearcher;
        private HashSet<String> result = new HashSet<String>();

       


        Lucene.Net.Store.Directory directory = FSDirectory.Open(indexDirectory);


        public void CreateIndexSearcher()
        {
            indexSearcher = new IndexSearcher(directory);

        }

       

        public void titleSearch(string search2)
        {
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "filename", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
            Query query = null;
            try
            {
                query = parser.Parse(search2);
            }
            catch (ParseException e)
            {
                // TODO Auto-generated catch block
                throw new Exception("not able to parse the query for title");
            }

        //    processSearchResults(query);
        }

        public HashSet<String> contentSearch(string search2)
        {

            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "filecontent", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
            Query query = null;
            try
            {
                query = parser.Parse(search2);
            }
            catch (ParseException e)
            {
                // TODO Auto-generated catch block
                throw new Exception("not able to parse the query for content");
            }

            try
            {

                // Search for the query
                TopScoreDocCollector collector = TopScoreDocCollector.Create(1000, false);
                indexSearcher.Search(query, collector);
                //	     Hits collector = indexSearcher.search(query);

                ScoreDoc[] hits = collector.TopDocs().ScoreDocs;

                int hitCount = collector.TotalHits;  //.GetTotalHits();
                //      System.out.println(hitCount + " total matching documents");

                // Examine the Hits object to see if there were any matches


                // Iterate over the Documents in the Hits object
                for (int i = 0; i < hitCount; i++)
                {
                    ScoreDoc scoreDoc = hits[i];
                    int docId = scoreDoc.Doc;
                    //         float docScore = scoreDoc.score;
                    //      System.out.println("docId: " + docId + "\t" + "docScore: " + docScore);

                    Document doc = indexSearcher.Doc(docId);

                    file = doc.GetField("filename").ToString();
                    int t1 = file.IndexOf('<');
                    path = doc.GetField("filepath").ToString();
                    int t2 = path.IndexOf('<');
                    //           System.out.println("File name: "+file.substring(t1));
                    //           System.out.println("Path name: "+path.substring(t2));

                    result.Add(file.Substring(t1) + " " + path.Substring(t2));
                  //  result.Add(file + " ; " + path);
                }

                return result;
            }
            catch (IOException e)
            {
                throw new Exception(" Unable to read the index!");
            }


        }

       


        //void showResult()
        //{
        //    // TODO Auto-generated method stub
        //    //if(!result.IsEmpty()){
        //    ////	System.out.println("Match found:");

        //    //    for(Object o: result)
        //    //    {
        //    //        String [] s = o.toString().split("> ");

        //    //        System.out.println(s[0]+">");
        //    //        System.out.println(s[1]);
        //    //        System.out.println(" ");
        //    //    }
        //    //     result.clear();
        //    //} else
        //    //{
        //    //    System.out.println("No Match found!");
        //    //}


        //    //}

        //    if (result.Count > 1)
        //    {
               
        //        foreach(Object o in result)
        //        {
        //            String s = Convert.ToString(o);

        //        }
        //    }

        //}
       

    }
}