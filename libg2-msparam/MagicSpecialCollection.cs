using System;
using System.Collections;

namespace libg2_msparam
{
    class MagicSpecialCollection : ICollection
    {
        private MagicSpecialAction[] msArray;

        void ICollection.CopyTo(Array sourceArray, int index)
        {
            foreach(MagicSpecialAction msa in msArray)
            {
                sourceArray.SetValue(msa, index);
                index = index + 1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MagicSpecialEnumerator(msArray);
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return this;
            }
        }

        int ICollection.Count
        {
            get
            {
                return msArray.Length;
            }
        }

    }

    public class MagicSpecialEnumerator : IEnumerator
    {
        private MagicSpecialAction[] msArray;
        private int cursor;

        public MagicSpecialEnumerator(MagicSpecialAction[] msarr)
        {
            this.msArray = msarr;
            cursor = -1;
        }

        bool IEnumerator.MoveNext()
        {
            if (cursor < msArray.Length)
                cursor = cursor + 1;

            return (!(cursor == msArray.Length));
        }

        void IEnumerator.Reset()
        {
            cursor = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                if ((cursor < 0) || (cursor == msArray.Length))
                {
                    throw new InvalidOperationException();
                }

                return msArray[cursor];
            }
        }
    
    }
}
