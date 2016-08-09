using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Text;

/// <summary>
/// Summary description for Logger
/// </summary>
public class Logger
{
    Dictionary<string,Stopwatch> timers;
    StringBuilder message;
    string _currentEvent= "";

	public Logger()
	{
        timers = new Dictionary<string, Stopwatch>();
        message = new StringBuilder();
	}

    private Stopwatch FindTimer(string id,bool CreateIfNotFound)
    {
        Stopwatch time = null;

        if (timers.ContainsKey(id))
            time = timers[id];
        else if (CreateIfNotFound)
        {
            time = new Stopwatch();
            timers[id] = time;
        }
        return time;
    }

    private void StartTimer(string id)
    {
        Stopwatch timer = FindTimer(id, true);
        timer.Reset();
        timer.Start();
    }

    private void StopTimer(string id)
    {
        Stopwatch timer = FindTimer(id, false);
        if (timer == null)
            return;

        if (timer.IsRunning)
            timer.Stop();
    }

    private long GetElapsedTime(string id)
    {
        Stopwatch timer = FindTimer(id, false);
        if (timer == null)
            return 0;
        else
            return timer.ElapsedMilliseconds;
    }

    private void StartTimer()
    {
        StartTimer("global");
    }

    private void StopTimer()
    {
        StopTimer("global");
    }

    private long GetElapsedTime()
    {
        return GetElapsedTime("global");
    }

    public string Flush()
    {
        return message.ToString();
    }

    public void EndEvent()
    {
        StopTimer();
        Log();
    }

    public void LogTimeBetweenEvents(string Message)
    {
        if (_currentEvent.Length > 0)  //already executing timer, then
        {
            StopTimer();
            Log();
        }
        _currentEvent = Message;
        StartTimer();
    }

    private void Log()
    {
        if (_currentEvent.Length > 0)
            message.AppendLine(String.Format("{0}: {2} ms - {1}", DateTime.Now, _currentEvent, GetElapsedTime()));

        _currentEvent = string.Empty;
    }


}