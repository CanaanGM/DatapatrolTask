using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listen.Data
{
    internal class Listener 
    {
        public bool Active { get; set; } = false;
        public string Name { get; set; } = string.Empty;
        public int Counter { get; set; } = 0;
        public int Target { get; set; } = 0;

        public event EventHandler<int> CounterChanged;

        public Listener()
        {
            var rand  = new Random();
            Name = NameGenerator.GenerateRandomMonsterName();
            Target = rand.Next(0, 11);
            Active = true;
        }

        public void Listen(int number)
        {
            if (Target == number)
            {

                Counter++;
                CounterChanged?.Invoke(this, Counter);
            }

        }

    }
}
