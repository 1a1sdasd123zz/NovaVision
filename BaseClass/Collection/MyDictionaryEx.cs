using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NovaVision.BaseClass.Collection
{
    [Serializable]
    [XmlRoot("MyDictionaryEx")]
    public class MyDictionaryEx<T> : IChangedEvent, ICollectionEvents, IXmlSerializable
    {
        private List<string> _keys;

        private List<T> mValues;

        private int index = 1;

        public string Current_Key = "";

        public int Count => mValues.Count;

        public T this[string key]
        {
            get
            {
                var indexOf = _keys.IndexOf(key);
                if (indexOf < 0)
                {
                    throw new IndexOutOfRangeException(_keys.IndexOf(key) + ",Key=" + key);
                }
                Current_Key = key;
                return mValues[indexOf];
            }
            set
            {
                var indexOf = _keys.IndexOf(key);
                if (indexOf < 0)
                {
                    throw new IndexOutOfRangeException(_keys.IndexOf(key) + ",Key=" + key);
                }
                Current_Key = key;
                mValues[indexOf] = value;
            }
        }

        public T this[int i]
        {
            get
            {
                Current_Key = _keys[i];
                return mValues[i];
            }
            set
            {
                Current_Key = _keys[i];
                mValues[i] = value;
            }
        }

        public event ChangeEventHandler Changed;

        public event ChangeEventHandler Changing;

        public event EventHandler Clearing;

        public event EventHandler Cleared;

        public event CollectionInsertEventHandler InsertingItem;

        public event CollectionInsertEventHandler InsertedItem;

        public event CollectionRemoveEventHandler RemovingItem;

        public event CollectionRemoveEventHandler RemovedItem;

        public event CollectionReplaceEventHandler ReplacingItem;

        public event CollectionReplaceEventHandler ReplacedItem;

        public event CollectionMoveEventHandler MovingItem;

        public event CollectionMoveEventHandler MovedItem;

        public List<string> GetKeys()
        {
            var keys = new List<string>();
            foreach (var key in _keys)
            {
                keys.Add(key);
            }
            return keys;
        }

        public void CopyKeysTo(string[] array, int i)
        {
            _keys.CopyTo(array, i);
        }

        public List<T> GetValues()
        {
            var values = new List<T>();
            foreach (var value in mValues)
            {
                values.Add(value);
            }
            return values;
        }

        public MyDictionaryEx()
        {
            _keys = new List<string>();
            _keys.Capacity = 24;
            mValues = new List<T>();
            mValues.Capacity = 24;
        }

        public MyDictionaryEx(MyDictionaryEx<T> other)
        {
            _keys = other._keys;
            mValues = other.mValues;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool Add(string key, T value)
        {
            try
            {
                if (_keys.Contains(key))
                {
                    return false;
                }
                var keyIndex = _keys.Count;
                OnInsertingItem(keyIndex, value);
                _keys.Add(key);
                mValues.Add(value);
                OnInsertedItem(keyIndex, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryAdd(string key, T value)
        {
            if (ContainsKey(key))
            {
                return false;
            }

            OnInsertingItem(_keys.Count, value);
            _keys.Add(key);
            mValues.Add(value);
            OnInsertedItem(_keys.Count - 1, value);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool Add(T value)
        {
            try
            {
                var key = "Value" + index;
                while (_keys.Contains(key))
                {
                    key = "Value" + index;
                    index++;
                }
                var keyIndex = _keys.Count;
                OnInsertingItem(keyIndex, value);
                _keys.Add(key);
                mValues.Add(value);
                OnInsertedItem(keyIndex, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool Insert(int i, string key, T value)
        {
            if (i < 0 || i >= _keys.Count)
            {
                return false;
            }
            if (_keys.Contains(key))
            {
                return false;
            }
            OnInsertingItem(i, value);
            _keys.Insert(i, key);
            mValues.Insert(i, value);
            OnInsertedItem(i, value);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool Replace(string oldKey, string newKey)
        {
            if (!_keys.Contains(oldKey))
            {
                return false;
            }
            if (_keys.Contains(newKey))
            {
                return false;
            }
            var i = _keys.IndexOf(oldKey);
            OnReplacingItem(i, oldKey, newKey);
            OnChangingEvent(oldKey, newKey);
            _keys[i] = newKey;
            OnReplacedItem(i, oldKey, newKey);
            OnChangedEvent(oldKey, newKey);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool Remove(string key)
        {
            if (!_keys.Contains(key))
            {
                return false;
            }
            var i = _keys.IndexOf(key);
            var value = mValues[i];
            OnRemovingItem(i, value);
            _keys.RemoveAt(i);
            mValues.RemoveAt(i);
            OnRemovedItem(i, value);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool Remove(int i)
        {
            if (i < 0 || i >= _keys.Count)
            {
                return false;
            }
            var value = mValues[i];
            OnRemovingItem(i, value);
            _keys.RemoveAt(i);
            mValues.RemoveAt(i);
            OnRemovedItem(i, value);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual void Clear()
        {
            OnClearing();
            _keys.Clear();
            mValues.Clear();
            OnCleared();
        }

        public bool MoveUp(int i)
        {
            if (i < 0 || i >= _keys.Count)
            {
                return false;
            }
            if (i == 0)
            {
                return false;
            }
            Exchange(i, i - 1);
            return true;
        }

        public bool MoveDown(int i)
        {
            if (i < 0 || i >= _keys.Count)
            {
                return false;
            }
            if (i == _keys.Count - 1)
            {
                return false;
            }
            Exchange(i, i + 1);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private bool Exchange(int i, int j)
        {
            if (i < 0 || i >= _keys.Count || j < 0 || j >= _keys.Count)
            {
                return false;
            }
            if (i == j)
            {
                return true;
            }
            OnMovingItem(i, j);
            var key_I = _keys[i];
            var value_I = mValues[i];
            _keys[i] = _keys[j];
            mValues[i] = mValues[j];
            _keys[j] = key_I;
            mValues[j] = value_I;
            OnMovedItem(i, j);
            return true;
        }

        public int IndexOfKey(string key)
        {
            return _keys.IndexOf(key);
        }

        public string KeyofIndex(int i)
        {
            if (i < 0 && i >= _keys.Count)
            {
                throw new IndexOutOfRangeException(i.ToString());
            }
            return _keys[i];
        }

        public int IndexOfValue(T t)
        {
            return mValues.IndexOf(t);
        }

        public bool ContainsKey(string key)
        {
            return _keys.Contains(key);
        }

        public bool ContainsValue(T value)
        {
            return mValues.Contains(value);
        }


        public bool SwapSomeElements(int index1, int count1, int index2, int count2)
        {
            try
            {
                var tempKeyArray1 = new string[count1];
                var tempValueArray1 = new T[count1];
                Array.Copy(_keys.ToArray(), index1, tempKeyArray1, 0, count1);
                Array.Copy(mValues.ToArray(), index1, tempValueArray1, 0, count1);
                var tempKeyArray2 = new string[count2];
                var tempValueArray2 = new T[count2];
                Array.Copy(_keys.ToArray(), index2, tempKeyArray2, 0, count2);
                Array.Copy(mValues.ToArray(), index2, tempValueArray2, 0, count2);
                _keys.RemoveRange(index1, count1);
                _keys.RemoveRange(index2 - count1, count2);
                mValues.RemoveRange(index1, count1);
                mValues.RemoveRange(index2 - count1, count2);
                _keys.InsertRange(index1, tempKeyArray2);
                _keys.InsertRange(index2 + count2 - count1, tempKeyArray1);
                mValues.InsertRange(index1, tempValueArray2);
                mValues.InsertRange(index2 + count2 - count1, tempValueArray1);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected virtual void OnChangedEvent(string oldName, string newName)
        {
            if (this.Changed != null)
            {
                this.Changed(this, new ChangeEventArg(oldName, newName));
            }
        }

        protected virtual void OnChangingEvent(string oldName, string newName)
        {
            if (this.Changing != null)
            {
                this.Changing(this, new ChangeEventArg(oldName, newName));
            }
        }

        protected virtual void OnClearing()
        {
            if (this.Clearing != null)
            {
                this.Clearing(this, new EventArgs());
            }
        }

        protected virtual void OnCleared()
        {
            if (this.Cleared != null)
            {
                this.Cleared(this, new EventArgs());
            }
        }

        protected virtual void OnInsertingItem(int i, object value)
        {
            this.InsertingItem?.Invoke(this, new CollectionInsertEventArgs(i, value));
        }

        protected virtual void OnInsertedItem(int i, object value)
        {
            if (this.InsertedItem != null)
            {
                this.InsertedItem(this, new CollectionInsertEventArgs(i, value));
            }
        }

        protected virtual void OnRemovingItem(int i, object value)
        {
            if (this.RemovingItem != null)
            {
                this.RemovingItem(this, new CollectionRemoveEventArgs(i, value));
            }
        }

        protected virtual void OnRemovedItem(int n, object value)
        {
            if (this.RemovedItem != null)
            {
                this.RemovedItem(this, new CollectionRemoveEventArgs(n, value));
            }
        }

        protected virtual void OnReplacingItem(int n, object oldValue, object newValue)
        {
            if (this.ReplacingItem != null)
            {
                this.ReplacingItem(this, new CollectionReplaceEventArgs(n, oldValue, newValue));
            }
        }

        protected virtual void OnReplacedItem(int n, object oldValue, object newValue)
        {
            if (this.ReplacedItem != null)
            {
                this.ReplacedItem(this, new CollectionReplaceEventArgs(n, oldValue, newValue));
            }
        }

        protected virtual void OnMovingItem(int fromIndex, int toIndex)
        {
            if (this.MovingItem != null)
            {
                this.MovingItem(this, new CollectionMoveEventArgs(fromIndex, toIndex));
            }
        }

        protected virtual void OnMovedItem(int fromIndex, int toIndex)
        {
            if (this.MovedItem != null)
            {
                this.MovedItem(this, new CollectionMoveEventArgs(fromIndex, toIndex));
            }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                var keysSer = new XmlSerializer(typeof(string));
                var valuesSer = new XmlSerializer(typeof(T));
                reader.Read();
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    reader.ReadStartElement("Item");
                    reader.ReadStartElement("Key");
                    var key = (string)keysSer.Deserialize(reader);
                    reader.ReadEndElement();
                    reader.ReadStartElement("Value");
                    var value = (T)valuesSer.Deserialize(reader);
                    reader.ReadEndElement();
                    _keys.Add(key);
                    mValues.Add(value);
                    reader.ReadEndElement();
                    reader.MoveToContent();
                }
                reader.ReadEndElement();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            var count = 0;
            var keysSer = new XmlSerializer(typeof(string));
            var valuesSer = new XmlSerializer(typeof(T));
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            foreach (var item in mValues)
            {
                writer.WriteStartElement("Item");
                writer.WriteStartElement("Key");
                keysSer.Serialize(writer, _keys[count], ns);
                writer.WriteEndElement();
                writer.WriteStartElement("Value");
                valuesSer.Serialize(writer, item, ns);
                writer.WriteEndElement();
                writer.WriteEndElement();
                count++;
            }
        }
    }

}
