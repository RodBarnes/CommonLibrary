using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class Setting
    {
        public Setting(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Settings : IList<Setting>
    {
        private readonly List<Setting> list = new List<Setting>();

        public Settings() { }

        public Settings(params string[] settings)
        {
            for (int i = 0; i < settings.Length; i++)
            {
                list.Add(new Setting(settings[i], settings[i + 1]));
                i++;
            }
        }

        public Setting this[string name]
        {
            get => list.FirstOrDefault(i => i.Name == name);
            set => list.Insert(list.IndexOf(list.FirstOrDefault(i => i.Name == name)), value);
        }

        #region Standard List Implementation

        public Setting this[int index] { get => list[index]; set => list[index] = value; }

        public int Count => list.Count;

        public bool IsReadOnly => ((IList<Setting>)list).IsReadOnly;

        public void Add(Setting item) => list.Add(item);

        public void Clear() => list.Clear();

        public bool Contains(Setting item) => list.Contains(item);

        public void CopyTo(Setting[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        public IEnumerator<Setting> GetEnumerator() => ((IList<Setting>)list).GetEnumerator();

        public int IndexOf(Setting item) => list.IndexOf(item);

        public void Insert(int index, Setting item) => list.Insert(index, item);

        public bool Remove(Setting item) => list.Remove(item);

        public void RemoveAt(int index) => list.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => ((IList<Setting>)list).GetEnumerator();
    }

    #endregion
}