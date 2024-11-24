namespace RepairDepot.ViewModel
{
    public static class Mediator
    {
        private static IDictionary<string, List<Func<object, Task>>> pl_dict =
           new Dictionary<string, List<Func<object, Task>>>();

        public static void Subscribe(string token, Func<object, Task> callback)
        {
            if (!pl_dict.ContainsKey(token))
            {
                var list = new List<Func<object, Task>>();
                list.Add(callback);
                pl_dict.Add(token, list);
            }
            else
            {
                bool found = false;
                foreach (var item in pl_dict[token])
                    if (item.Method.ToString() == callback.Method.ToString())
                        found = true;
                if (!found)
                    pl_dict[token].Add(callback);
            }
        }

        public static void Unsubscribe(string token, Func<object, Task> callback)
        {
            if (pl_dict.ContainsKey(token))
                pl_dict[token].Remove(callback);
        }

        public async static Task Notify(string token, object args = null)
        {
            if (pl_dict.ContainsKey(token))
                foreach (var callback in pl_dict[token])
                    await callback(args);
        }
    }
}
