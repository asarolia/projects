using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MyHelpers
{
    public class ObjectPool
    {
        public static Dictionary<long, Queue> pool = new Dictionary<long, Queue>();

        static public void Push(long id, Object element)
        {
            if (pool.ContainsKey(id))
            {
                lock(pool[id].SyncRoot)
                    pool[id].Enqueue(element);
            }
            else
            {
                pool[id] = new Queue(1);
                pool[id].Enqueue(element);
            }
        }

        static public Object Pop(long id)
        {
            if (pool.ContainsKey(id))
            {
                lock (pool[id].SyncRoot)
                    return pool[id].Dequeue();
            }

            return null;
        }

        static public Object PopKeepOne(long id)
        {
            try
            {

                if (pool.ContainsKey(id))
                {
                    if (pool[id].Count == 1)
                    {
                        lock (pool[id].SyncRoot)
                            return pool[id].Peek();
                    }
                    else
                    {
                        lock (pool[id].SyncRoot)
                            return pool[id].Dequeue();
                    }
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Queue read failed with exception '" + e.Message +"'.");
            }
            return null;
        }

        //static public void Push(long id, Object element)
        //{
        //    if (pool.ContainsKey(id))
        //        pool[id].Enqueue(element);
        //    else
        //    {
        //        pool[id] = new Queue(1);
        //        pool[id].Enqueue(element);
        //    }
        //}

        //static public Object Pop(long id)
        //{
        //    if (pool.ContainsKey(id))
        //        return pool[id].Dequeue();

        //    return null;
        //}

        //static public Object PopKeepOne(long id)
        //{
        //    if (pool.ContainsKey(id))
        //    {
        //        if (pool[id].Count == 1)
        //            return pool[id].Peek();
        //        else
        //            return pool[id].Dequeue();
        //    }

        //    return null;
        //}



    }
}
