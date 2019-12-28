using System.Collections.Generic;

namespace TopDownStealth.Characters
{
    /// <summary>
    /// Local memory for <see cref="Character"/> with shared <see cref="CharacterBehaviour"/>.
    /// </summary>
    public class CharacterBrain
    {
        private Dictionary<string, object> _memory = null;

        public CharacterBrain()
        {
            _memory = new Dictionary<string, object>();
        }

        public T Remember<T>(string id)
        {
            return (T)_memory[id];
        }

        public void Remember(string id, object data)
        {
            _memory[id] = data;
        }

        public void Clear()
        {
            _memory.Clear();
        }

        public void Forget(string id)
        {
            _memory.Remove(id);
        }
    }
}
