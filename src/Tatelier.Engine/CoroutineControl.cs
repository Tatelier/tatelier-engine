using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatelier.Engine
{
    public class CoroutineControl
    {
        LinkedList<Coroutine> coroutineList = new LinkedList<Coroutine>();

        public Coroutine StartCoroutine(IEnumerator enumerator)
        {
            var c = new Coroutine(enumerator);
            coroutineList.AddLast(c);
            return c;
        }

        public Coroutine StartCoroutine(Coroutine coroutine)
        {
            coroutineList.AddLast(coroutine);

            return coroutine;
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            coroutineList.Remove(coroutine);
        }

        public void Update()
        {
            var node = coroutineList.First;

            while (node != null)
            {
                var nval = node.Value;
                if (!nval.Enabled)
                {
                    node = node.Next;
                    continue;
                }

                bool hasNext = nval.MoveNext();

                if (hasNext)
                {
                    if (nval.Current is IEnumerator w)
                    {
                        var c = new Coroutine(w)
                        {
                            Parent = nval
                        };
                        coroutineList.AddAfter(node, c);

                        // 子供の処理が完了するまで無効化
                        nval.Enabled = false;
                    }
                    node = node.Next;
                }
                else
                {
                    if (nval.Parent != null)
                    {
                        // 子供の処理が完了したので有効化
                        nval.Parent.Enabled = true;
                    }

                    var next = node.Next;
                    coroutineList.Remove(node);
                    node = next;
                }
            }
        }
    }

    public class Coroutine
        : IEnumerator
    {
        public bool Enabled = true;

        bool finish = false;

        public bool Finish => finish;

        public Coroutine Parent;

        public object Current => enumerator.Current;

        protected IEnumerator enumerator;

        public virtual bool MoveNext()
        {
            finish = enumerator.MoveNext();

            return finish;
        }

        public virtual void Reset()
        {
            enumerator.Reset();
        }

        public Coroutine(IEnumerator enumerator)
        {
            this.enumerator = enumerator;
        }
    }

    //class Wait : Coroutine
    //{
    //    int startTime;
    //    int millisec;

    //    IEnumerator GetWaitEnumerator()
    //    {
    //        while (Supervision.NowMilliSec - startTime < millisec)
    //        {
    //            yield return null;
    //        }
    //    }

    //    public Wait(int millisec)
    //    {
    //        this.millisec = millisec;
    //        startTime = Supervision.NowMilliSec;
    //        enumerator = GetWaitEnumerator();
    //    }
    //}
}
